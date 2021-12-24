using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IParkingService _vehicles;
        public VehiclesController(IParkingService vehicle)
        {
            _vehicles = vehicle;
        }
        [HttpGet("vehicles")]
        public IActionResult GetVehicles()
        {
            return Ok(_vehicles.GetVehicles());
        }
        [HttpGet("{id}")]
        public IActionResult GetVehicleById(string id)
        {
            Vehicle vehicle;
            try
            {
                vehicle = _vehicles.GetVehicleById(id);
            }
            catch(ArgumentException)
            {
                return BadRequest();
            }
            if(vehicle == null) 
            { 
                return NotFound();
            }
            return Ok(vehicle);
        }
        [HttpPost]
        public IActionResult AddVehicle([FromBody] VehicleDto vehicleDTO)
        {
            if(!((vehicleDTO.VehicleType>0 && (int)vehicleDTO.VehicleType < 5) && (vehicleDTO.Balance > 0 && vehicleDTO.Balance <= decimal.MaxValue)))
            {
                return BadRequest();
            }
            var vehicle = new Vehicle(Vehicle.GenerateRandomRegistrationPlateNumber(),(VehicleType)vehicleDTO.VehicleType,vehicleDTO.Balance);
            _vehicles.AddVehicle(vehicle);
            return StatusCode(201, vehicle);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(string id)
        {
            if (!Vehicle.idValidation(id))
            {
                return BadRequest();
            }
            try
            {
                _vehicles.RemoveVehicle(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
