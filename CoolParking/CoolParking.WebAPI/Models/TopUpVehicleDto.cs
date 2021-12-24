using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CoolParking.WebAPI.Models
{
    public class TopUpVehicleDto
    {
        [JsonProperty("id")]
        [Required]
        public string Id { get; set; }
        [JsonProperty("sum")]
        [Required]
        public decimal Sum { get; set; }
    }
}
