using System.Text.Json;
using ATOZBussinessLayer.Objects.StoreType.Services;
using ATOZDTO.ObjectsDTOs.StoreTypeDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATOZAPIs.Controllers.StoreType
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreTypeController : ControllerBase
    {
        [HttpGet("GetStoreTypesWithColors")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<List<clsStoreTypeWithColorDTO>> GetStoreTypesWithColors()
        {
            var service = new clsGetAllStoreTypesService();
            var items = service.GetAll();

            if (items == null)
            {
                return StatusCode(500, service.Exception?.Message ?? "An error occurred while retrieving store types with colors");
            }

            return Ok(items);
        }
    }
}