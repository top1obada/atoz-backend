using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.Login;
using ATOZDataLayer.Connection.Customer;
using ATOZDTO.ObjectsDTOs.CustomerDTO;
using ATOZDTO.ObjectsDTOs.RetrivingLoggedInDTO;

namespace ATOZBussinessLayer.Objects.Customer.Services
{
    public class clsCustomerLoginByEmailService : ILoginService<string,clsRetrivingLoggedInDTO>
    {
        public Exception Exception;

        public clsRetrivingLoggedInDTO Login(string Email)
        {

            var Result = clsCustomerData.ExecuteCustomerLoginByEmail(Email, ref Exception);

            if (Result == null) return null;

            return Result;
        }

    }
}
