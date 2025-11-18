using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Connection.Customer;
using ATOZDTO.ObjectsDTOs.RetrivingLoggedInDTO;

namespace ATOZBussinessLayer.Objects.RefreshToken.Services
{
    public class clsGetCustomerByRefreshTokenService 
    {

        public Exception Exception;
        public clsRetrivingLoggedInDTO Get(string HashedRefreshToken)
        {
            return clsRefreshTokenData.GetCustomerByRefreshToken(HashedRefreshToken, ref Exception);
        }

    }
}
