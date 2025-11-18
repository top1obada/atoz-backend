using System;

namespace ATOZDTO.ObjectsDTOs.ItemDTO
{
    public class clsStoreSubCategoryItemItemDTO
    {
        public int? StoreItemID { get; set; }

        public int ? SubCategoryItemID { get; set; }
        
        public float? Price { get; set; }
        public int? Count { get; set; }
        public string? Notes { get; set; }
        public string? ImagePath { get; set; }
        public string? SubCategoryItemTypeName { get; set; }
        public float? PriceAfterDiscount { get; set; }
        public DateTime? DiscountEndDate { get; set; }
    }
}