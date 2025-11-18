using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ATOZDTO.ObjectsDTOs.RequestDTO;
using ATOZBussinessLayer.Objects.Request.Services;
using System.Text.Json;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestItem;

namespace ATOZAPIs.Controllers.Request
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        [HttpPost("InsertCompletedRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<int> InsertCompletedRequest([FromBody] clsCompletedRequestDTO completedRequestDTO)
        {
            if (completedRequestDTO == null)
            {
                return BadRequest("Request data cannot be null");
            }

            var requestService = new ATOZBussinessLayer.Objects.Request.Services.clsInsertCompletedRequestService();
            bool isSuccess = requestService.Save(completedRequestDTO);

            if (isSuccess)
            {
                return Ok(completedRequestDTO.RequestDTO.RequestID);
            }
            else
            {
                return StatusCode(500, requestService.Exception?.Message);
            }
        }


        [HttpPost("GetCustomerRequests/{filterJson}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<clsCustomerRequestDTO>> GetCustomerRequests(string FilterJson)
        {
            clsCustomerRequestsFilterDTO filter;
            try
            {
                filter = JsonSerializer.Deserialize<clsCustomerRequestsFilterDTO>(FilterJson);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (filter == null)
            {
                return BadRequest("Filter data cannot be null");
            }

            if (filter.CustomerID == null || filter.CustomerID <= 0)
            {
                return BadRequest("Valid Customer ID is required");
            }

            if (filter.PageNumber == null || filter.PageNumber <= 0)
            {
                return BadRequest("Valid PageNumber is required");
            }

            if (filter.PageSize == null || filter.PageSize <= 0)
            {
                return BadRequest("Valid PageSize is required");
            }

            var requestService = new clsGetCustomerRequestsService();
            var requests = requestService.GetAll(filter);

            if (requestService.Exception != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, requestService.Exception.Message);
            }

            if (requests == null || requests.Count == 0)
            {
                return Ok(new List<clsCustomerRequestDTO>());
            }

            return Ok(requests);
        }


        [HttpGet("GetRequestStatus/{RequestID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]

        public ActionResult<enRequestStatus> GetRequestStatus(int RequestID)
        {

            var Service = new clsGetRequestStatusService();

            var Result = Service.Get(RequestID);

            if (Request == null)
            {
                if (Service.Exception == null)
                {
                    return NotFound("Request ID Not Found");
                }

                return StatusCode(500, Service.Exception.Message);
            }

            return Ok(Result);
        }

        [HttpPost("GetRequestContent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<clsRequestItemDetailsDTO>> GetRequestContent([FromBody] clsRequestContentFilterDTO filter)
        {
            if (filter == null)
            {
                return BadRequest("Filter data cannot be null");
            }

            if (filter.RequestID == null || filter.RequestID <= 0)
            {
                return BadRequest("Valid Request ID is required");
            }

            if (filter.PageNumber == null || filter.PageNumber <= 0)
            {
                return BadRequest("Valid PageNumber is required");
            }

            if (filter.PageSize == null || filter.PageSize <= 0)
            {
                return BadRequest("Valid PageSize is required");
            }

            var requestService = new clsGetRequestContentService();
            var requestItems = requestService.GetAll(filter);

            if (requestService.Exception != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, requestService.Exception.Message);
            }

            if (requestItems == null || requestItems.Count == 0)
            {
                return Ok(new List<clsRequestItemDetailsDTO>());
            }

            return Ok(requestItems);
        }

        [HttpPut("CancelRequest/{requestID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CancelRequest(int requestID)
        {
            if (requestID <= 0)
            {
                return BadRequest("Valid Request ID is required");
            }

            var cancelService = new clsCancelRequestService();

            bool isCancelled = cancelService.Implement(requestID);

            if (isCancelled)
            {
                return Ok(true);
            }
            else
            {
                if (cancelService.Exception != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, cancelService.Exception.Message);
                }
                else
                {
                    return NotFound($"Request with ID {requestID} not found or could not be cancelled");
                }
            }
        }
    }

   
    
}