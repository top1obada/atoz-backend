using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestItem;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestTotalPayment;




namespace ATOZDTO.ObjectsDTOs.RequestDTO
{
    public class clsCompletedRequestDTO
    {

        public clsRequestDTO? RequestDTO { get; set; }

        public List<clsRequestItemDTO>? RequestItems { get; set; }

        public clsRequestTotalPaymentDTO? RequestTotalPaymentDTO { get; set; }


    }
}
