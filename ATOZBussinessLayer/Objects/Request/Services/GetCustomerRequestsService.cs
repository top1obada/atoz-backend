using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.GetAll;
using ATOZDataLayer.Connection.Request;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO;

namespace ATOZBussinessLayer.Objects.Request.Services
{
    public class clsGetCustomerRequestsService : IGetAll<clsCustomerRequestDTO,clsCustomerRequestsFilterDTO>
    {
        public Exception Exception;
        public List<clsCustomerRequestDTO> GetAll(clsCustomerRequestsFilterDTO customerRequestsFilterDTO)
        {

            return clsRequestData.GetCustomerRequests(customerRequestsFilterDTO, ref Exception);

        }

    }
}
