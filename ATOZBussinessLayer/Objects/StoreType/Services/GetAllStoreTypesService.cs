using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.GetAll;
using ATOZDataLayer.Connection.StoreType;
using ATOZDTO.ObjectsDTOs.StoreTypeDTO;

namespace ATOZBussinessLayer.Objects.StoreType.Services
{
    public class clsGetAllStoreTypesService : IGenericGetAll<clsStoreTypeWithColorDTO>
    {
        public Exception Exception;
        public List<clsStoreTypeWithColorDTO> GetAll()
        {

            return clsStoreTypeData.GetStoreTypesWithColors(ref Exception);

        }

    }
}
