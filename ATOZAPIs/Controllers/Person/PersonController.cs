using System.Text.Json;
using ATOZBussinessLayer.Objects.Person.Services;
using ATOZDTO.ObjectsDTOs.PersonDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATOZAPIs.Controllers.Person
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet("GetPersonInfo/{personID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<clsPersonInfoDTO> GetPersonInfo(int personID)
        {
            if (personID <= 0)
            {
                return BadRequest("Invalid Person ID");
            }

            var service = new clsGetPersonInfoService();
            var personInfo = service.Get(personID);

            if (service.Exception != null)
            {
                return StatusCode(500, service.Exception.Message);
            }

            if (personInfo == null)
            {
                return NotFound("Person not found");
            }

            return Ok(personInfo);
        }
    }
}