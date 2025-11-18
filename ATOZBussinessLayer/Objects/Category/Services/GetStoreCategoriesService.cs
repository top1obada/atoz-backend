using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.GetAll;
using ATOZDataLayer.Connection.Category;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.CategoryDTO;

namespace ATOZBussinessLayer.Objects.Category.Services
{
    public class clsGetStoreCategoriesService : IGetAll<clsCategoryDTO, clsStoreCategoriesFilterDTO>
    {
        public Exception Exception;

        public List<clsCategoryDTO> GetAll(clsStoreCategoriesFilterDTO filter)
        {
            return clsCategoryData.GetStoreCategories(filter, ref Exception);
        }
    }
}