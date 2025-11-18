using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.CustomerBalanceDTO
{
    public class clsCustomerBalancePaymentsHistoryAndCustomerBalanceDTO
    {

        public float ? CustomerBalance { get; set; }

        public List<clsCustomerBalancePaymentHistoryDTO> ?CustomerBalancePaymentHistoryDTOs { get; set; }

    }
}
