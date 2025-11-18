using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Connection.Request;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestLocationDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestLocationDTO.ATOZDTO.ObjectsDTOs.RequestDTO.RequestLocationDTO;

namespace ATOZBussinessLayer.Objects.Request.RequestLocation.Services
{
    public class clsGetRequestLocationService
    {

        public Exception Exception;
        public clsRequestLocationDTO Find(int RequestID)
        {
            return clsRequestData.GetRequestLocation(RequestID, ref Exception);
        }

    }
}
