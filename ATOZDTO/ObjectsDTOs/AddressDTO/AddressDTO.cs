using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.AddressDTO
{
    public class clsAddressDTO
    {

        public int ? AddressID { get; set; }

        public int ? PersonID { get; set; }

        public string? City { get; set; }

        public string ? AreaName { get; set; }

        public string ? StreetNameOrNumber { get; set; }

        public string? BuildingName { get; set; }

        public string? ImportantNotes { get; set; }

    }
}
