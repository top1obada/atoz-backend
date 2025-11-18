using ATOZBussinessLayer.Objects.SubCategory.Services;
using ATOZDTO.ObjectsDTOs.SubCategoryDTO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ATOZDTO.FilterationDTO;

namespace ATOZAPIs.Controllers.SubCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        [HttpGet("GetStoreCategorySubCategories/{StoreCategorySubCategoriesFilterJson}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<List<clsSubCategoryDTO>> GetStoreSubCategories(string StoreCategorySubCategoriesFilterJson)
        {
            clsStoreCategorySubCategoriesFilterDTO storeCategorySubCategoriesFilterDTO = null;

            try
            {
                storeCategorySubCategoriesFilterDTO = JsonSerializer.Deserialize<clsStoreCategorySubCategoriesFilterDTO>(StoreCategorySubCategoriesFilterJson);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            var service = new clsGetStoreSubCategoriesService();
            var result = service.GetAll(storeCategorySubCategoriesFilterDTO);

            if (result == null) return StatusCode(500, service.Exception.Message);

            return Ok(result);
        }
    }
}