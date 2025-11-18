using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.RequestDTO.RequestItem
{
    public class clsRequestItemDTO
    {
        public int? RequestItemID { get; set; }
        public int? RequestID { get; set; }
        public int? StoreItemID { get; set; }
        public int? Count { get; set; }
      
    }
}
