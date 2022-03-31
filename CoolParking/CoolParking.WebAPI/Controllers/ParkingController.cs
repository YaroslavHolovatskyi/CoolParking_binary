using CoolParking.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }
        [HttpGet("balance")]
        public IActionResult GetBalance()
        {
            return Ok(_parkingService.GetBalance());
        }
        [HttpGet("capacity")]
        public IActionResult GetCapacity()
        {
            return Ok(_parkingService.GetCapacity());
        }
        [HttpGet("freePlaces")]
        public IActionResult GetFreePlaces()
        {
            return Ok(_parkingService.GetFreePlaces());
        }
    }
}
