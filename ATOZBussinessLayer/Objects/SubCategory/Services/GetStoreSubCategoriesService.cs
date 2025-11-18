using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.GetAll;
using ATOZDataLayer.Connection.SubCategory;
using ATOZDTO.ObjectsDTOs.SubCategoryDTO;
using ATOZDTO.FilterationDTO;

namespace ATOZBussinessLayer.Objects.SubCategory.Services
{
    public class clsGetStoreSubCategoriesService : IGetAll<clsSubCategoryDTO, clsStoreCategorySubCategoriesFilterDTO>
    {
        public Exception Exception;

        public List<clsSubCategoryDTO> GetAll(clsStoreCategorySubCategoriesFilterDTO filter)
        {
            return clsSubCategoryData.GetStoreSubCategories(filter, ref Exception);
        }
    }
}