using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Connection.CustomerBalance;

namespace ATOZBussinessLayer.Objects.CustomerBalance.Services
{
    public class clsGetCustomerBalanceService
    {
        public Exception Exception;
        public float Get(int CustomerID)
        {

            return clsCustomerBalanceData.GetCustomerBalance(CustomerID, ref Exception);

        }

    }
}
