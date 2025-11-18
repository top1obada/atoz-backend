using ATOZDTO.ObjectsDTOs.StoreDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ATOZBussinessLayer.Objects.Store.Services;
using System.Text.Json;
using ATOZDTO.FilterationDTO;

namespace ATOZAPIs.Controllers.Store
{

    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        [HttpGet("GetCustomerStoresSuggestions/{customerStoresFilterJson}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
       
        public ActionResult<List<clsCustomerStoreSuggestionDTO>> GetCustomerStoresSuggistions(string customerStoresFilterJson)
        {
            clsCustomerStoresFilterDTO customerStoresFilterDTO = null;

            try
            {
                customerStoresFilterDTO = JsonSerializer.Deserialize<clsCustomerStoresFilterDTO>(customerStoresFilterJson);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (customerStoresFilterDTO == null)
            {
                return BadRequest("The Inputs Is Null");
            }

            var Service = new clsGetAllCustomerStoreSuggestionsService();
            var CustomerStores = Service.GetAll(customerStoresFilterDTO);

            if (CustomerStores == null)
            {
                return StatusCode(500, Service.Exception.Message);
            }

            return Ok(CustomerStores);
        }


        [HttpGet("GetCustomerFavoriteStores/{customerFavoriteStoreFilterDTO}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public ActionResult<List<clsCustomerStoreSuggestionDTO>> GetCustomerFavoriteStores(string
            customerFavoriteStoreFilterDTO)
        {

            clsCustomerFavoriteStoreFilterDTO CustomerFavoriteStoreFilter = null;

            try
            {
                CustomerFavoriteStoreFilter = JsonSerializer.Deserialize<clsCustomerFavoriteStoreFilterDTO>(customerFavoriteStoreFilterDTO);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
                
            var Service = new clsGetAllCustomerFavoriteStoresService();

            var CustomerStores = Service.GetAll(CustomerFavoriteStoreFilter);

            if (CustomerStores == null)
            {
                return StatusCode(500, Service.Exception.Message);
            }

            return Ok(CustomerStores);
        }


        [HttpGet("GetFamousCustomerStores/{customerStoresFilterJson}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public ActionResult<List<clsCustomerStoreSuggestionDTO>> GetFamousCustomerStores(string customerStoresFilterJson)
        {
            clsCustomerStoresFilterDTO customerStoresFilterDTO = null;

            try
            {
                customerStoresFilterDTO = JsonSerializer.Deserialize<clsCustomerStoresFilterDTO>(customerStoresFilterJson);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (customerStoresFilterDTO == null)
            {
                return BadRequest("The Inputs Is Null");
            }

            var Service = new clsGetAllFamousCustomerStoresService();
            var CustomerStores = Service.GetAll(customerStoresFilterDTO);

            if (CustomerStores == null)
            {
                return StatusCode(500, Service.Exception.Message);
            }

            return Ok(CustomerStores);
        }


        [HttpGet("GetCustomerPreviousRequestsStores/{customerFavoriteStoreFilterDTO}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<List<clsCustomerStoreSuggestionDTO>> GetCustomerPreviousRequestsStores(string customerFavoriteStoreFilterDTO)
        {
            clsCustomerFavoriteStoreFilterDTO customerFilter = null;

            try
            {
                customerFilter = JsonSerializer.Deserialize<clsCustomerFavoriteStoreFilterDTO>(customerFavoriteStoreFilterDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            var service = new clsGetAllCustomerPreviousRequestsStoresService();
            var previousStores = service.GetAll(customerFilter);

            if (previousStores == null)
            {
                return StatusCode(500, service.Exception?.Message ?? "An error occurred while retrieving previous requests stores");
            }

            return Ok(previousStores);
        }


        [HttpGet("GetCustomerSearchStores/{customerSearchingStoresFilterDTO}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<List<clsCustomerStoreSuggestionDTO>> GetCustomerSearchStores(string customerSearchingStoresFilterDTO)
        {
            clsCustomerSearchingStoresFilterDTO customerSearchingStoresFilter = null;

            try
            {
                customerSearchingStoresFilter = JsonSerializer.Deserialize<clsCustomerSearchingStoresFilterDTO>(customerSearchingStoresFilterDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            var Service = new clsGetAllCustomerSearchStoresService();

            var CustomerStores = Service.GetAll(customerSearchingStoresFilter);

            if (CustomerStores == null)
            {
                return StatusCode(500, Service.Exception.Message);
            }

            return Ok(CustomerStores);
        }

    }
}
