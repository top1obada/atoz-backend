using System.Text.Json;
using ATOZBussinessLayer.Objects.Item.Services;
using ATOZDTO.ObjectsDTOs.ItemDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ATOZDTO.FilterationDTO;
namespace ATOZAPIs.Controllers.Item
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        [HttpGet("GetStoreSubCategoryItemItems/{storeSubCategoryItemItemsFilterJson}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<List<clsStoreSubCategoryItemItemDTO>> GetStoreSubCategoryItemItems(string storeSubCategoryItemItemsFilterJson)
        {
            clsStoreSubCategoryItemItemsFilterDTO storeSubCategoryItemItemsFilterDTO = null;

            try
            {
                storeSubCategoryItemItemsFilterDTO = JsonSerializer.Deserialize<clsStoreSubCategoryItemItemsFilterDTO>(storeSubCategoryItemItemsFilterJson);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (storeSubCategoryItemItemsFilterDTO == null)
            {
                return BadRequest("The Inputs Is Null");
            }

            var service = new clsGetStoreSubCategoryItemItemsService();
            var items = service.GetAll(storeSubCategoryItemItemsFilterDTO);

            if (items == null)
            {
                return StatusCode(500, service.Exception?.Message ?? "An error occurred while retrieving store subcategory item items");
            }

            return Ok(items);
        }
    }
}
