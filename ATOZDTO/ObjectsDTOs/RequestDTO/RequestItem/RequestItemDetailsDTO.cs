using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.RequestDTO.RequestItem
{
    public class clsRequestItemDetailsDTO
    {
        public string? ImagePath { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public int? Count { get; set; }
        public string? ItemName { get; set; }
    }
}
