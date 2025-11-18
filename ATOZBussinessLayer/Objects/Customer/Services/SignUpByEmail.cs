using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.SignUp;
using ATOZDataLayer.Connection.Customer;
using ATOZDTO.ObjectsDTOs.CustomerDTO;
using ATOZDTO.ObjectsDTOs.RetrivingLoggedInDTO;

namespace ATOZBussinessLayer.Objects.Customer.Services
{
    public class clsCustomerSignUpByEmailService : ISignUpService<clsCustomerSignUpByEmailDTO,clsRetrivingLoggedInDTO>
    {

        public Exception Exception;
        public clsRetrivingLoggedInDTO SignUp(clsCustomerSignUpByEmailDTO CustomerSignUpByEmail)
        {

            var Result = clsCustomerData.ExecuteCustomerSignUpByEmail
                (CustomerSignUpByEmail, ref Exception);

            if (Result == null) return null;

            return new clsRetrivingLoggedInDTO()
            {
                PersonID = Result.PersonID,
                UserID = Result.UserID,
                BranchID = Result.CustomerID,
                JoiningDate = DateTime.Now,
                FirstName = CustomerSignUpByEmail.Person.ContactInformation.Email,
                Role = ATOZDTO.ObjectsDTOs.UserDTO.enUserRole.eCustomer
            };

        }


    }
}
