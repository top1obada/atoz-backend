using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.GetAll;
using ATOZDataLayer.Connection.Request;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestItem;

namespace ATOZBussinessLayer.Objects.Request.Services
{
    public class clsGetRequestContentService : IGetAll<clsRequestItemDetailsDTO,clsRequestContentFilterDTO>
    {
        public Exception Exception;
        public List<clsRequestItemDetailsDTO> GetAll(clsRequestContentFilterDTO filter)
        {
            return clsRequestData.GetRequestContent(filter, ref Exception);
        }

    }
}
