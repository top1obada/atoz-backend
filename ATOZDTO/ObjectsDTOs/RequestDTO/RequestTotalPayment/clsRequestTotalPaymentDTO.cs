using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.RequestDTO.RequestTotalPayment
{
    public class clsRequestTotalPaymentDTO
    {
        public int? RequestID { get; set; }
        public double? TotalDue { get; set; }
        public double? Discount { get; set; }
        public double? BalanceValueUsed { get; set; }
    }
}
