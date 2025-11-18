using System;
using System.Collections.Generic;
using ATOZBussinessLayer.Services.GetAll;
using ATOZDataLayer.Connection.Store;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.StoreDTO;

namespace ATOZBussinessLayer.Objects.Store.Services
{
    public class clsGetAllCustomerSearchStoresService : IGetAll<clsCustomerStoreSuggestionDTO, clsCustomerSearchingStoresFilterDTO>
    {
        public Exception Exception;
        public List<clsCustomerStoreSuggestionDTO> GetAll(clsCustomerSearchingStoresFilterDTO customerSearchingStoresFilterDTO)
        {
            return clsStoreData.GetCustomerSearchStores(customerSearchingStoresFilterDTO, ref Exception);
        }
    }
}