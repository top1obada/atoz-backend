using System.Security.Claims;
using ATOZBussinessLayer.Objects.RandomCode.Services;
using ATOZDTO.ObjectsDTOs.RandomCodeDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATOZAPIs.Controllers.RandomCode
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomCodeController : ControllerBase
    {

        [HttpGet("PhoneNumberRandomCode/{PhoneNumber}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]

        public ActionResult<string> GetPhoneNumberRandomCode(string PhoneNumber)
        {

            var PersonID = User.FindFirstValue("PersonID");
            
            if(PersonID == null)
            {
                return BadRequest("There Is no Person ID");
            }

            var PhoneNumberRandomCodeDTO = new clsPhoneNumberRandomCodeDTO() { PersonID = Convert.ToInt32(PersonID), PhoneNumber = PhoneNumber };

            var Service = new clsPhoneNumberRandomCodeService();

            var Result = Service.Get(PhoneNumberRandomCodeDTO);

            if(Result == null)
            {
                return StatusCode(500, Service.Exception.Message);
            }

            return Ok(Result);  

        }

    }
}
