using CoolParking.BL.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CoolParking.WebAPI.Models
{
    public class VehicleDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("vehicleType")]
        [Required]
        public VehicleType VehicleType { get; set; }
        [JsonProperty("balance")]
        [Required]
        public decimal Balance { get; set; }
    }
}
