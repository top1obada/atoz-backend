using ATOZDTO.ObjectsDTOs.RequestDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestDeliveryDTO;
using ATOZDataLayer.Connection.Request.RequestDelivery;
namespace ATOZBussinessLayer.Objects.Request.RequestDelivery.Services
{
    public class clsGetRequestDeliveryService
    {
        public Exception Exception;
       
        public clsRequestDeliveryDTO? Get(int RequestID)
        {

            return clsRequestDeliveryData.GetRequestDelivery(RequestID, ref Exception);

        }
    }
}