using ATOZAPIs.RefreshToken;
using ATOZBussinessLayer.Objects.Session.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATOZAPIs.Controllers.Session
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {

        [HttpPut("DropSession")]
        public ActionResult<bool> DropSession([FromHeader(Name = "RefreshToken")] string RefreshToken)
        {
            if (RefreshToken == null) return BadRequest("The RefreshToken Is Not Sent");

            var HashedRefreshToken = clsRefreshTokenHelper.HashToken(RefreshToken);

            var service = new clsDropSessionService();

            var Result = service.Implement(HashedRefreshToken);

            if(Result)return Ok(Result);

            return StatusCode(500, service.Exception.Message);

        }



    }
}
