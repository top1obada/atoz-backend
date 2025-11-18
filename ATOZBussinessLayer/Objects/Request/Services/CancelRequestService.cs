using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Connection.Request;
using ATOZDTO.ObjectsDTOs.RequestDTO;

namespace ATOZBussinessLayer.Objects.Request.Services
{
    public class clsCancelRequestService
    {
        public Exception Exception;
        public bool Implement(int RequestID)
        {
            return clsRequestData.UpdateRequestCase(RequestID, enRequestStatus.eCancled, ref Exception);
        }

    }
}
