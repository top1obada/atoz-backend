using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.RequestDTO
{

    public enum enRequestStatus { ePending = 1,eCancled = 2,eRejected = 3,eOnRoad = 4,eCompleted = 5}

    public class clsRequestDTO
    {
        public int? RequestID { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? RequestCustomerID { get; set; }
        public int? StoreID { get; set; }
        public enRequestStatus? RequestStatus { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
