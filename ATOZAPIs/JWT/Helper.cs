using ATOZDTO.ObjectsDTOs.RetrivingLoggedInDTO;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ATOZAPIs.JWT
{
    public static class clsJWTHelper
    {

        public static string GenerateJwtToken(
            clsRetrivingLoggedInDTO RetrivingLoggedINDTO,
            clsJWT jwtSettings)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key)
            );

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            var claims = new List<Claim>
            {
                new Claim("FirstName", RetrivingLoggedINDTO.FirstName.ToString()),

                new Claim("PersonID", RetrivingLoggedINDTO.PersonID.ToString()),
                new Claim("UserID", RetrivingLoggedINDTO.UserID.ToString()),

                new Claim("JoiningDate",((DateTime)(RetrivingLoggedINDTO.JoiningDate)).ToString("yyyy/MM/dd")),

                new Claim(ClaimTypes.Role,RetrivingLoggedINDTO.Role.ToString()),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("AddressConfirmed",RetrivingLoggedINDTO.IsAddressInfoConifrmed != null
                ?RetrivingLoggedINDTO.IsAddressInfoConifrmed ==true?"Yes" : "No" : "No"),
                new Claim("VerifyPhoneNumberMode",RetrivingLoggedINDTO.VerifyPhoneNumberMode.ToString())

            };

            if (RetrivingLoggedINDTO.BranchID != null)
            {
                claims.Add(new Claim("BranchID", RetrivingLoggedINDTO.BranchID.ToString()));
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes ?? 60),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }


        public static clsJWT GetToken(IConfiguration configuration)
        {
            return new clsJWT
            {
                Issuer = configuration["JwtSettings:Issuer"],
                Audience = configuration["JwtSettings:Audience"],
                DurationInMinutes = configuration.GetValue<int>("JwtSettings:DurationInMinutes"),
                Key = configuration["JwtSettings:Key"]
            };
        }

    }
}
