using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.SignUp;
using ATOZDataLayer.Connection.Customer;
using ATOZDTO.ObjectsDTOs.CustomerDTO;
using ATOZDTO.ObjectsDTOs.RetrivingLoggedInDTO;
using MyServices.PasswordServices;
namespace ATOZBussinessLayer.Objects.Customer.Services
{
    public class clsCustomerSignUpByUserNameService : ISignUpService<clsSignUpCustomerByUserNameDTO,
        clsRetrivingLoggedInDTO>
    {

        public Exception Exception;

        public clsRetrivingLoggedInDTO SignUp(clsSignUpCustomerByUserNameDTO SignUpCustomerByUserNameDTO)
        {

            var PasswordHashResult = clsPasswordEncrypt.HashPassword(SignUpCustomerByUserNameDTO.NativeLoginInfoDTO.Password);

            var SecurityRestrivingDTO = new clsLoginRetrivingSecurity()
            { HashedPassword = PasswordHashResult.Hash, Salt = PasswordHashResult.Salt };

            var Result = clsCustomerData.ExecuteCustomerSignUpByUserName(SignUpCustomerByUserNameDTO,
                SecurityRestrivingDTO, ref Exception);

            if (Result == null) return null;

            return new clsRetrivingLoggedInDTO()
            {
                PersonID = Result.PersonID,
                UserID = Result.UserID,
                BranchID = Result.CustomerID,
                FirstName = SignUpCustomerByUserNameDTO.Person.FirstName,
                JoiningDate = DateTime.Now,
                Role = ATOZDTO.ObjectsDTOs.UserDTO.enUserRole.eCustomer
            };


        }

    }
}
