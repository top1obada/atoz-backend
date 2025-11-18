using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using ATOZBussinessLayer.Objects.CustomerBalance.Services;
using ATOZDTO.ObjectsDTOs.CustomerBalanceDTO;
using ATOZDataLayer.Connection.CustomerBalance;
using ATOZDTO.FilterationDTO;

namespace ATOZAPIs.Controllers.CustomerBalance
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBalanceController : ControllerBase
    {
        [HttpGet("GetCustomerBalancePaymentsHistoryAndBalance/{customerBalancePaymentsHistoryFilterJson}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<clsCustomerBalancePaymentsHistoryAndCustomerBalanceDTO> GetCustomerBalancePaymentsHistoryAndBalance
            (string customerBalancePaymentsHistoryFilterJson)
        {
            clsCustomerBalancePaymentsHistoryFilterDTO customerBalancePaymentsHistoryFilterDTO = null;

            try
            {
                customerBalancePaymentsHistoryFilterDTO = JsonSerializer.Deserialize<clsCustomerBalancePaymentsHistoryFilterDTO>(customerBalancePaymentsHistoryFilterJson);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (customerBalancePaymentsHistoryFilterDTO == null)
            {
                return BadRequest("The Inputs Is Null");
            }

            var service = new clsGetCustomerBalancePaymentsHistoryAndCustomerBalanceService();
            var customerBalanceData = service.Get(customerBalancePaymentsHistoryFilterDTO);

            if (customerBalanceData == null)
            {
                return StatusCode(500, service.Exception?.Message ?? "An error occurred while retrieving customer balance data");
            }

            return Ok(customerBalanceData);
        }



        [HttpGet("GetCustomerBalance/{customerID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<float> GetCustomerBalance(int customerID)
        {
            if (customerID <= 0)
            {
                return BadRequest("Invalid Customer ID");
            }

            var service = new clsGetCustomerBalanceService();
            float balance = service.Get(customerID);

            if (service.Exception != null)
            {
                return StatusCode(500, service.Exception.Message);
            }

            return Ok(balance);
        }
    }
}