using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.RequestDTO
{
    public class clsCustomerRequestDTO
    {

        public int ? RequestID { get; set; }

        public DateTime? RequestDate { get; set; }

        public enRequestStatus? RequestStatus { get; set; }

        public DateTime? CompletedRequestDate { get; set; }

        public int ? StoreID { get; set; }

        public string ? StoreName { get; set; }

        public string ? StoreTypeName { get; set; }




    }
}
