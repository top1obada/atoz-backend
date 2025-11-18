using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.GetAll;
using ATOZDataLayer.Connection.CustomerBalance;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.CustomerBalanceDTO;

namespace ATOZBussinessLayer.Objects.CustomerBalance.Services
{
    public class clsGetCustomerBalancePaymentsHistoryAndCustomerBalanceService 
     
    {

        public Exception Exception;

        public clsCustomerBalancePaymentsHistoryAndCustomerBalanceDTO Get(clsCustomerBalancePaymentsHistoryFilterDTO CustomerBalancePaymentsHistoryFilterDTO)
        {
            return clsCustomerBalanceData.GetCustomerBalancePaymentsHistoryAndBalance(CustomerBalancePaymentsHistoryFilterDTO, ref Exception);
        }

    }
}
