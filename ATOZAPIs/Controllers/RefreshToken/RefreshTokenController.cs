using ATOZAPIs.JWT;
using ATOZAPIs.RefreshToken;
using ATOZBussinessLayer.Objects.RefreshToken.Services;
using ATOZBussinessLayer.Objects.Session;
using ATOZDTO.ObjectsDTOs.TokenDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyServices.PasswordServices;

namespace ATOZAPIs.Controllers.RefreshToken
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {

        IConfiguration _Configuration;

        public RefreshTokenController(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public ActionResult<string> GetCustomerByRefreshToken([FromHeader(Name = "RefreshToken")] string RefreshToken)
        {

            if (RefreshToken == null) return BadRequest("The RefreshToken Is Not Sent");

            var HashedRefreshToken = clsRefreshTokenHelper.HashToken(RefreshToken);

            var Service = new clsGetCustomerByRefreshTokenService();

            var Result = Service.Get(HashedRefreshToken);

            if(Result != null)
            {
                return Ok(clsJWTHelper.GenerateJwtToken(Result, clsJWTHelper.GetToken(_Configuration)));
            }

            return StatusCode(500, Service.Exception.Message);

        } 

    }
}
