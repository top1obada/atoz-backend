using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.LocationDTO.CustomerLocationDTO
{
    public class clsCustomerLocationDTO
    {

        public int? CustomerID { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? Address { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }
}
