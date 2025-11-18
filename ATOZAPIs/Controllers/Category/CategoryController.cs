using ATOZBussinessLayer.Objects.Category.Services;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.CategoryDTO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ATOZAPIs.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("GetStoreCategories/{storeItemsFilterDTO}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<List<clsCategoryDTO>> GetStoreCategories(string storeItemsFilterDTO)
        {
            clsStoreCategoriesFilterDTO storeItemsFilter = null;

            try
            {
                storeItemsFilter = JsonSerializer.Deserialize<clsStoreCategoriesFilterDTO>(storeItemsFilterDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            var service = new clsGetStoreCategoriesService();
            var result = service.GetAll(storeItemsFilter);

            if (result == null) return StatusCode(500, service.Exception.Message);

            return Ok(result);
        }
    }
}