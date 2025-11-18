using ATOZBussinessLayer.Objects.Request.RequestDelivery.Services;
using ATOZDTO.ObjectsDTOs.RequestDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestDeliveryDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATOZAPIs.Controllers.Request.RequestDelivery
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestDeliveryController : ControllerBase
    {
        [HttpGet("{RequestID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public ActionResult<clsRequestDeliveryDTO> GetRequestDelivery(int RequestID)
        {
            if (RequestID < 1)
                return BadRequest("معرف الطلب غير صالح");

            var Service = new clsGetRequestDeliveryService();
            var Result = Service.Get(RequestID);

            if (Result == null)
            {
                if (Service.Exception == null)
                {
                    return NotFound("لم يتم العثور على تفاصيل التوصيل للطلب المطلوب");
                }

                return StatusCode(500, Service.Exception.Message);
            }

            return Ok(Result);
        }
    }
}