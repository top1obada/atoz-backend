using ATOZBussinessLayer.Objects.SubCategoryItem.Services;
using ATOZDTO.ObjectsDTOs.SubCategoryItemDTO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ATOZDTO.FilterationDTO;

namespace ATOZAPIs.Controllers.SubCategoryItem
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryItemController : ControllerBase
    {
        [HttpGet("GetStoreSubCategorySubCategoryItems/{StoreSubCategorySubCategoryItemsJson}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<List<clsSubCategoryItemDTO>> GetStoreSubCategoryItems(string StoreSubCategorySubCategoryItemsJson)
        {
            clsStoreSubCategorySubCategoryItemsFilterDTO storeSubCategorySubCategoriesItemsFilterDTO = null;

            try
            {
                storeSubCategorySubCategoriesItemsFilterDTO = JsonSerializer.Deserialize<clsStoreSubCategorySubCategoryItemsFilterDTO>(StoreSubCategorySubCategoryItemsJson);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            var service = new clsGetStoreSubCategoryItemsService();
            var result = service.GetAll(storeSubCategorySubCategoriesItemsFilterDTO);

            if (result == null) return StatusCode(500, service.Exception.Message);

            return Ok(result);
        }
    }
}