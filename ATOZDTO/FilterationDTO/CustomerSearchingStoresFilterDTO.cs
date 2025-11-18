using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.FilterationDTO
{
    public class clsCustomerSearchingStoresFilterDTO : clsPageFilterDTO
    {

        public int? CustomerID { get; set; }

        public string? SearchingText { get; set; }



    }
}
