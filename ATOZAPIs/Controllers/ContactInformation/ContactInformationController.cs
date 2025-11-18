using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ATOZBussinessLayer.Objects.ContactInformation.Services;
using ATOZDTO.ObjectsDTOs.ContactInformationDTO;

namespace ATOZAPIs.Controllers.ContactInformation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationController : ControllerBase
    {
        [HttpGet("GetPersonContactInformation/{personID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<clsContactInformationDTO> GetPersonContactInformation(int personID)
        {
            if (personID <= 0)
            {
                return BadRequest("Invalid Person ID");
            }

            var service = new clsGetPersonContactInformationService();
            var contactInfo = service.Get(personID);

            if (service.Exception != null)
            {
                return StatusCode(500, service.Exception.Message);
            }

            if (contactInfo == null)
            {
                return NotFound("Person contact information not found");
            }

            return Ok(contactInfo);
        }
    }
}