using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.GetAll;
using ATOZDataLayer.Connection.SubCategoryItem;
using ATOZDTO.ObjectsDTOs.SubCategoryItemDTO;
using ATOZDTO.FilterationDTO;


namespace ATOZBussinessLayer.Objects.SubCategoryItem.Services
{
    public class clsGetStoreSubCategoryItemsService : IGetAll<clsSubCategoryItemDTO, clsStoreSubCategorySubCategoryItemsFilterDTO>
    {
        public Exception Exception;

        public List<clsSubCategoryItemDTO> GetAll(clsStoreSubCategorySubCategoryItemsFilterDTO filter)
        {
            return clsSubCategoryItemData.GetStoreSubCategoryItems(filter, ref Exception);
        }
    }
}