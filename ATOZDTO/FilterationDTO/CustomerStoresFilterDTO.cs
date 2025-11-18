using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.FilterationDTO
{
    public class clsCustomerStoresFilterDTO : clsPageFilterDTO
    {

        public int? CustomerID { get; set; }

        public int? DistanceInMeters { get; set; }

        public string? StoreTypeName { get; set; }


    }
}
