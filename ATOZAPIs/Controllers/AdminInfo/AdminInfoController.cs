using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ATOZBussinessLayer.Objects.AdminInfo.Services;

namespace ATOZAPIs.Controllers.AdminInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminInfoController : ControllerBase
    {
        [HttpGet("GetAdminPhoneNumber/{AdminID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<string> GetAdminPhoneNumber(int AdminID)
        {
            if (AdminID <= 0)
            {
                return BadRequest("Invalid Admin ID");
            }

            var service = new clsGetAdminPhoneNumberService();
            var phoneNumber = service.Get(AdminID);

            if (service.Exception != null)
            {
                return StatusCode(500, service.Exception.Message);
            }

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return NotFound("Admin phone number not found");
            }

            return Ok(phoneNumber);
        }
    }
}