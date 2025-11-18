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
    public class clsGetAllCustomerStoreSuggestionsService : IGetAll<clsCustomerStoreSuggestionDTO, clsCustomerStoresFilterDTO>
    {

        public Exception Exception;
        public List<clsCustomerStoreSuggestionDTO> GetAll(clsCustomerStoresFilterDTO CustomerstoreFilterDTO)
        {
            return clsStoreData.GetAllCustomerStoresSuggestions(CustomerstoreFilterDTO, ref Exception);
        }

    }
}
