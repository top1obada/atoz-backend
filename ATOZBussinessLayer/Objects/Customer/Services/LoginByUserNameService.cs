using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.Login;
using ATOZDataLayer.Connection.Customer;
using ATOZDTO.ObjectsDTOs.LoginInfoDTO;
using ATOZDTO.ObjectsDTOs.RetrivingLoggedInDTO;
using MyServices.PasswordServices;
namespace ATOZBussinessLayer.Objects.Customer.Services
{
    public class clsCustomerLoginByUserNameService : ILoginService<clsNativeLoginInfoDTO,clsRetrivingLoggedInDTO>
    {
        public Exception Exception;
        public clsRetrivingLoggedInDTO Login(clsNativeLoginInfoDTO loginInfo)
        {
            var Result = clsCustomerData.ExecuteCustomerLoginByUserName(loginInfo, ref Exception);

            if (Result == null) return null;

            var IsValid = clsPasswordEncrypt.VerifyPassword(loginInfo.Password,
                Result.RetrivingSecurityDTO.HashedPassword, Result.RetrivingSecurityDTO.Salt);

            if (!IsValid)
            {

                Exception = new Exception("UserName/Password Is Wronge");
                return null;

            }

            return Result.RetrivingLoggedInDTO;




        }

    }
}
