using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.RequestDTO.RequestDeliveryDTO
{

    public class clsRequestDeliveryDTO
    {
        public int? DisctanceMeters { get; set; }
        public int? SpeedAverageKM { get; set; }
        public int? RoadTimeMinutes { get; set; }
        public decimal? Price { get; set; }
    }
    
}
