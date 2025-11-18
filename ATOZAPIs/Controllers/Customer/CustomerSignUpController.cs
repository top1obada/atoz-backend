
using ATOZAPIs.JWT;
using ATOZAPIs.RefreshToken;
using ATOZBussinessLayer.Objects.Customer.Services;
using ATOZBussinessLayer.Objects.CustomerLocation;
using ATOZBussinessLayer.Objects.CustomerLocation.Services;
using ATOZBussinessLayer.Objects.Session;
using ATOZBussinessLayer.Objects.Session.Services;
using ATOZDTO.ObjectsDTOs.ContactInformationDTO;
using ATOZDTO.ObjectsDTOs.CustomerDTO;
using ATOZDTO.ObjectsDTOs.LoginInfoDTO;
using ATOZDTO.ObjectsDTOs.PersonDTO;
using ATOZDTO.ObjectsDTOs.RetrivingLoggedInDTO;
using ATOZDTO.ObjectsDTOs.SessionDTO;
using ATOZDTO.ObjectsDTOs.TokenDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace ATOZAPIs.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccessController :  ControllerBase
    {

        IConfiguration _Configuration;

        public CustomerAccessController(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }


        [HttpPost("GoEmail/{DeviceName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<clsTokensDTO>> GoEmail(string DeviceName)
        {


            var IDToken = Request.Headers["GoogleIDToken"].ToString();

            if (string.IsNullOrWhiteSpace(IDToken))
            {
                return BadRequest("There Is No ID Token");
            }

            var PayLoad = await clsGoogleHelper.ValidateIdTokenAsync(IDToken,
                new string[] { "234947276336-sqnn7qnd5ha6h5etqu9ff51f3pq153qm.apps.googleusercontent.com" });


            if(PayLoad == null)
            {
                return BadRequest("Email Is Not Correct");
            }

            var LoginService = new clsCustomerLoginByEmailService();

            clsRetrivingLoggedInDTO Result = LoginService.Login(PayLoad.Email);


            if(Result == null)
            {

                var AccessToken = Request.Headers["GoogleAccessToken"].ToString();

                if (string.IsNullOrWhiteSpace(AccessToken))
                {
                    return BadRequest("There Is No Access Token");
                }

                var ExtraInfo = await clsGoogleHelper.GetExtraProfileDataAsync(AccessToken);


                var SignUpByEmailDTO = clsGoogleHelper.GetCustomerSignUpByEmailInfo(PayLoad, ExtraInfo);

                var SignUpservice = new clsCustomerSignUpByEmailService();

                Result = SignUpservice.SignUp(SignUpByEmailDTO);

                if(Result == null)
                {
                    return StatusCode(500, SignUpservice.Exception.Message);
                }


            }


            string NativeRefreshToken = clsRefreshTokenHelper.GenerateRefreshToken();

            string HashedRefreshToken = clsRefreshTokenHelper.HashToken(NativeRefreshToken);

            var Session = new clsSession()
            {
                CreatedDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(_Configuration.GetValue<int>("RefreshToken:DurationInDays")),
                DeviceName = DeviceName,
                RevokedDate = null,
                UserID = Result.UserID,
                HashedRefreshToken = HashedRefreshToken
            };

            var SaveSessionservice = new clsSaveSessionService();

            var SaveSessionResult = SaveSessionservice.Save(Session);

            if (!SaveSessionResult)
            {
                return StatusCode(500, SaveSessionservice.Exception.Message);
            }

            return Ok(new clsTokensDTO()
            {
                JwtToken = clsJWTHelper.GenerateJwtToken(Result, clsJWTHelper.GetToken(_Configuration)),
                RefreshToken = NativeRefreshToken,
            });


        }



        [HttpPost("SignUpByUserName/{DeviceName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public ActionResult<clsTokensDTO> SignUpByUserName
            (clsPersonWithPointDTO PersonWithPointDTO,[FromHeader(Name = "LoginData")] string NativeLoginInfo, string DeviceName)
        {

            if (NativeLoginInfo == null)
            {
                return BadRequest("There Is No Login Data");
            }


            clsNativeLoginInfoDTO NativeLoginInfoDTO = null;


            try
            {
                NativeLoginInfoDTO = JsonSerializer.Deserialize<clsNativeLoginInfoDTO>(NativeLoginInfo);
            }

            catch
            {
                return BadRequest("Invalid JSON format in header");
            }


            clsSignUpCustomerByUserNameDTO signUpCustomerByUserNameDTO = new clsSignUpCustomerByUserNameDTO()
            {
                Person = PersonWithPointDTO.PersonDTO,
                NativeLoginInfoDTO = NativeLoginInfoDTO
            };

            var SignUpService = new clsCustomerSignUpByUserNameService();

            var Result = SignUpService.SignUp(signUpCustomerByUserNameDTO);

            if (Result == null)
            {
                return StatusCode(500, SignUpService.Exception.Message);
            }


            string NativeRefreshToken = clsRefreshTokenHelper.GenerateRefreshToken();

            string HashedRefreshToken = clsRefreshTokenHelper.HashToken(NativeRefreshToken);

            var Session = new clsSession()
            {
                CreatedDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(_Configuration.GetValue<int>("RefreshToken:DurationInDays")),
                DeviceName = DeviceName,
                RevokedDate = null,
                UserID = Result.UserID,
                HashedRefreshToken = HashedRefreshToken
            };

            var SaveSessionservice = new clsSaveSessionService();

            var SaveSessionResult = SaveSessionservice.Save(Session);

            if (!SaveSessionResult)
            {
                return StatusCode(500, SaveSessionservice.Exception.Message);
            }



            var CustomerLocationSaveService = new clsSaveCustomerLocationService();

            var CustomerLocation = new clsCustomerLocation()
            {
                CustomerID = Result.BranchID,
                Address = null,
                Latitude = PersonWithPointDTO.UserPointDTO.Latitude,
                Longitude = PersonWithPointDTO.UserPointDTO.Longitude,
                UpdatedDate = DateTime.Now
            };

            if (!CustomerLocationSaveService.Save(CustomerLocation))
            {
                return StatusCode(500, CustomerLocationSaveService.Exception.Message);
            }

            return Ok(new clsTokensDTO()
            {
                JwtToken = clsJWTHelper.GenerateJwtToken(Result, clsJWTHelper.GetToken(_Configuration)),
                RefreshToken = NativeRefreshToken,
            });

        }



        [HttpGet("LoginByUserName/{DeviceName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]

        public ActionResult<clsTokensDTO>
            LoginByUserName([FromHeader(Name = "LoginData")] string NativeLoginInfo, string DeviceName)
        {

            if(NativeLoginInfo == null)
            {
                return BadRequest("There Is No Login Data");
            }

            clsNativeLoginInfoDTO NativeLoginInfoDTO = null;


            try
            {
                NativeLoginInfoDTO = JsonSerializer.Deserialize<clsNativeLoginInfoDTO>(NativeLoginInfo);
            }

            catch
            {
                return BadRequest("Invalid JSON format in header");
            }

            if (NativeLoginInfoDTO == null || string.IsNullOrEmpty(NativeLoginInfoDTO.UserName) || string.IsNullOrEmpty(NativeLoginInfoDTO.Password))
            {
                return BadRequest("Invalid User Data");
            }

            var LoginService = new clsCustomerLoginByUserNameService();

            var Result = LoginService.Login(NativeLoginInfoDTO);

            if(Result == null)

            {
                if (LoginService.Exception != null)
                {
                    return StatusCode(500, LoginService.Exception.Message);
                }
                else
                {
                    return Unauthorized("UserName/Password Is Wronge");
                }
            }

            string NativeRefreshToken = clsRefreshTokenHelper.GenerateRefreshToken();

            string HashedRefreshToken = clsRefreshTokenHelper.HashToken(NativeRefreshToken);

            var Session = new clsSession()
            {
                CreatedDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(_Configuration.GetValue<int>("RefreshToken:DurationInDays")),
                DeviceName = DeviceName,
                RevokedDate = null,
                UserID = Result.UserID,
                HashedRefreshToken = HashedRefreshToken
            };


            var SessionService = new clsSaveSessionService();

            if (SessionService.Save(Session))
            {
                return Ok(new clsTokensDTO()
                {
                    JwtToken = clsJWTHelper.GenerateJwtToken(Result, clsJWTHelper.GetToken(_Configuration)),
                    RefreshToken = NativeRefreshToken
                });
            }

            return StatusCode(500, SessionService.Exception.Message);

        }




    }
}



