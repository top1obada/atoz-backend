using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.GetAll;
using ATOZDataLayer.Connection.Item;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.ItemDTO;
using ATOZDTO.ObjectsDTOs.SubCategoryItemDTO;
namespace ATOZBussinessLayer.Objects.Item.Services
{
    public class clsGetStoreSubCategoryItemItemsService : IGetAll<clsStoreSubCategoryItemItemDTO,clsStoreSubCategoryItemItemsFilterDTO>
    {
        public Exception Exception;
        public List<clsStoreSubCategoryItemItemDTO> GetAll(clsStoreSubCategoryItemItemsFilterDTO filter)
        {

            return clsItemData.GetStoreSubCategoryItemItems(filter, ref Exception);

        }

    }
}
