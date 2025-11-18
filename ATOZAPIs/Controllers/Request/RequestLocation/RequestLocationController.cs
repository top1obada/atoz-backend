using ATOZBussinessLayer.Objects.Request.RequestLocation.Services;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestLocationDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestLocationDTO.ATOZDTO.ObjectsDTOs.RequestDTO.RequestLocationDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATOZAPIs.Controllers.Request.RequestLocation
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestLocationController : ControllerBase
    {

        [HttpGet("{RequestID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public ActionResult<clsRequestLocationDTO> GetRequestLocation(int RequestID)
        {

            if (RequestID < 1)
                return BadRequest("Request ID Is Not Suitable");

            var Service = new clsGetRequestLocationService();

            var Result = Service.Find(RequestID);

            if(Result == null)
            {
                if(Service.Exception == null)
                {
                    return NotFound("The Request ID Is Not Found");
                }

                return StatusCode(500, Service.Exception.Message);
            }

            return Ok(Result);
        }

    }
}
