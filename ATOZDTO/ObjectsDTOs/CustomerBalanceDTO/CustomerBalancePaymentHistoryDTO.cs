using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDTO.ObjectsDTOs.RequestDTO;

namespace ATOZDTO.ObjectsDTOs.CustomerBalanceDTO
{
    public class clsCustomerBalancePaymentHistoryDTO
    {
        public int RequestID { get; set; }
        public DateTime? RequestDate { get; set; }
        public enRequestStatus? RequestStatus { get; set; }
        public float? PriceAfterDiscount { get; set; }
        public float? BalanceValueUsed { get; set; }
    }
}
