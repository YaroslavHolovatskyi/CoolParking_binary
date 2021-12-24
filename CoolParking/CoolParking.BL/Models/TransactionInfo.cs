// TODO: implement struct TransactionInfo.
//       Necessarily implement the Sum property (decimal) - is used in tests.
//       Other implementation details are up to you, they just have to meet the requirements of the homework.
using Newtonsoft.Json;
using System;

namespace CoolParking.BL.Models
{
    public struct TransactionInfo
    {
        [JsonProperty("vehicleId")]
        public string vehicleId { get; set; }
        [JsonProperty("sum")]
        public decimal Sum { get; set; }
        [JsonProperty("transactionTime")]
        public DateTime transactionTime { get; set; }
    }
}