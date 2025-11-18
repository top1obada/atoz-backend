using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.StoreDTO
{
    public class clsCustomerStoreSuggestionDTO
    {

        public int ? StoreID { get; set; }

        public string ? StoreName { get; set; }

        public string ? StoreTypeName { get; set; }

        public float ? DistanceInMeters { get; set; }

        public string? StoreImage { get; set; }

        public string ? HexCode { get; set; }

        public int ? Shade { get; set; }


    }
}
