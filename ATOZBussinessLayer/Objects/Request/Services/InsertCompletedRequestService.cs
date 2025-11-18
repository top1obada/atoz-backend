using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.Save;
using ATOZDataLayer.Connection.Request;
using ATOZDTO.ObjectsDTOs.RequestDTO;

namespace ATOZBussinessLayer.Objects.Request.Services
{
    public class clsInsertCompletedRequestService : ISaveService<clsCompletedRequestDTO>
    {

        public Exception Exception;

        public bool Save(clsCompletedRequestDTO completedRequestDTO)
        {
            
            completedRequestDTO.RequestDTO.RequestID = clsRequestData.ExecuteInsertCompletedRequest(completedRequestDTO, ref Exception);

            return completedRequestDTO.RequestDTO.RequestID != null;
        }

    }
}
