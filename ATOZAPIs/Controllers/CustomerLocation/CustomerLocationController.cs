using ATOZBussinessLayer.Objects.CustomerLocation;
using ATOZBussinessLayer.Objects.CustomerLocation.Services;
using ATOZDTO.ObjectsDTOs.LocationDTO.CustomerLocationDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ATOZAPIs.Controllers.CustomerLocation
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerLocationController : ControllerBase
    {

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public ActionResult<bool> UpdateCustomerLocation(clsCustomerLocationDTO CustomerLocationDTO)
        {

            if(CustomerLocationDTO == null || CustomerLocationDTO.CustomerID == null)
            {
                return BadRequest("The Informations Is Not Completed");
            }

            var service = new clsSaveCustomerLocationService();

            var CustomerLocation = clsCustomerLocation.Find((int)CustomerLocationDTO.CustomerID, ref service.Exception);

            if(CustomerLocation == null)
            {
                return NotFound($"The CustomerID Is Not Found");
            }

            CustomerLocation.CustomerLocationDTO = CustomerLocationDTO;

            var Result = service.Save(CustomerLocation);

            if (Result)
            {
                return Ok(true);
            }

            return StatusCode(500, service.Exception.Message);

        }

        [HttpGet("{CustomerID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public ActionResult<clsCustomerLocationDTO> GetCustomerLocation(int CustomerID)
        {

            Exception ex = null;

            var CustomerLocation = clsCustomerLocation.Find(CustomerID, ref ex);

            if(CustomerLocation == null)
            {
                if(ex == null)
                {
                    return NotFound("This Customer ID Not Has Location Or Customer Not Found");
                }

                return StatusCode(500, ex.Message);
            }

            return Ok(CustomerLocation.CustomerLocationDTO);

        }

    }
}
