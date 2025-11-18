using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ATOZBussinessLayer.Objects.Address.Services;
using ATOZDTO.ObjectsDTOs.AddressDTO;
using ATOZBussinessLayer.Objects.Address;

namespace ATOZAPIs.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<int> AddAddress([FromBody] clsAddressDTO addressDTO)
        {
            if (addressDTO == null)
            {
                return BadRequest("Address data is required");
            }

            // Validate required fields for new address
            if (string.IsNullOrEmpty(addressDTO.City))
            {
                return BadRequest("City is required");
            }

            var address = new clsAddress();

            address.AddressDTO = addressDTO;
           

            var service = new clsSaveAddressService();

            bool isSaved = service.Save(address);

            if (service.Exception != null)
            {
                return StatusCode(500, service.Exception.Message);
            }

            if (isSaved && address.AddressID.HasValue)
            {
                return Ok(address.AddressID.Value);
            }
            else
            {
                return StatusCode(500, "Failed to save address");
            }
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<bool> UpdateAddress([FromBody] clsAddressDTO addressDTO)
        {

            if (addressDTO == null)
            {
                return BadRequest("Address data is required");
            }

            if (!addressDTO.AddressID.HasValue || addressDTO.AddressID <= 0)
            {
                return BadRequest("Valid Address ID is required");
            }

            // Validate required fields for update
            if (string.IsNullOrEmpty(addressDTO.City))
            {
                return BadRequest("City is required");
            }

            Exception exception = null;

            var address = clsAddress.Find(addressDTO.AddressID.Value, ref exception);

            if (address == null) return BadRequest("This Person ID Not Found");

            address.AddressDTO = addressDTO;

            var service = new clsSaveAddressService();

            bool isUpdated = service.Save(address);

            if (service.Exception != null)
            {
                return StatusCode(500, service.Exception.Message);
            }

            if (isUpdated)
            {
                return Ok(true);
            }
            else
            {
                return NotFound("Address not found or update failed");
            }
        }

        [HttpGet("FindAddress/{PersonID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<clsAddressDTO> FindAddress(int PersonID)
        {
            if (PersonID <= 0)
            {
                return BadRequest("Invalid Address ID");
            }

            Exception ex = null;
            var address = clsAddress.Find(PersonID, ref ex);

            if (ex != null)
            {
                return StatusCode(500, ex.Message);
            }

            if (address == null)
            {
                return NotFound("Address not found");
            }

            return Ok(address.AddressDTO);
        }

    }
}