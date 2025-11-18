using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDTO.ObjectsDTOs.RequestDTO;
using ATOZDataLayer.Connection.Request;
namespace ATOZBussinessLayer.Objects.Request.Services
{
    public class clsGetRequestStatusService
    {

        public Exception Exception;

        public enRequestStatus? Get(int RequestID)
        {
            return clsRequestData.GetRequestStatus(RequestID, ref Exception);
        }

    }
}
