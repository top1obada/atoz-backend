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
    public class clsGetAllCustomerPreviousRequestsStoresService : IGetAll<clsCustomerStoreSuggestionDTO, clsCustomerFavoriteStoreFilterDTO>
    {
        public Exception Exception;

        public List<clsCustomerStoreSuggestionDTO> GetAll(clsCustomerFavoriteStoreFilterDTO filter)
        {
            return clsStoreData.GetCustomerPreviousRequestsStores(filter, ref Exception);
        }
    }
}