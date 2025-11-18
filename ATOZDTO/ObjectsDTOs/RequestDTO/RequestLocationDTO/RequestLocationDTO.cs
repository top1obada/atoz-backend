using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.RequestDTO.RequestLocationDTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace ATOZDTO.ObjectsDTOs.RequestDTO.RequestLocationDTO
    {
        public class clsRequestLocationDTO
        {
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
            public DateTime? updated { get; set; }
            public ushort? CurrentSpeed_KM { get; set; }
            public ushort? RestDistance_meters { get; set; }
            public ushort? TimeToArrive_Minutes { get; set; }
            public string? RequestPlace { get; set; }
        }
    }
}
