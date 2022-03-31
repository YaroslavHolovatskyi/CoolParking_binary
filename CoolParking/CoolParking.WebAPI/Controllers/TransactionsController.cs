using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IParkingService _transaction;
        public TransactionsController(IParkingService transaction)
        {
            _transaction = transaction;
        }
        [HttpGet("last")]
        public IActionResult GetLastTransactions()
        {
            return Ok(_transaction.GetLastParkingTransactions());
        }
        [HttpGet("all")]
        public IActionResult AllFromLog()
        {
            try
            {
                return Ok(_transaction.ReadFromLog());
            }
            catch(InvalidOperationException)
            {
                return NotFound();
            }
        }
        [HttpPut("topUpVehicle")]
        public IActionResult TopUpVehicle(TopUpVehicleDto topUpVehicleDTO)
        {
            if (!Vehicle.idValidation(topUpVehicleDTO.Id) || topUpVehicleDTO.Sum < 0 || topUpVehicleDTO.Sum > decimal.MaxValue)
            {
                return BadRequest();
            }
            if (_transaction.GetVehicleById(topUpVehicleDTO.Id) == null)
            {
                return NotFound();
            }
            _transaction.TopUpVehicle(topUpVehicleDTO.Id, topUpVehicleDTO.Sum);
            return Ok();
        }
    }
}
