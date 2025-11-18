using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.StoreTypeDTO
{
    public class clsStoreTypeWithColorDTO
    {
        public string? StoreTypeName { get; set; }
        public string? HexCode { get; set; }
        public int? Shade { get; set; }
        public string? CodePoint { get; set; } // Changed from int? to string?
    }
}