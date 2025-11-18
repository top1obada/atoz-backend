using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.GetAll;
using ATOZDataLayer.Connection.Store;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.StoreDTO;

namespace ATOZBussinessLayer.Objects.Store.Services
{
    public class clsGetAllFamousCustomerStoresService : IGetAll<clsCustomerStoreSuggestionDTO,clsCustomerStoresFilterDTO>
    {
        public Exception Exception;
        public List<clsCustomerStoreSuggestionDTO> GetAll(clsCustomerStoresFilterDTO filter)
        {
            return clsStoreData.GetAllFamousCustomerStores(filter, ref Exception);
        }

    }
}
