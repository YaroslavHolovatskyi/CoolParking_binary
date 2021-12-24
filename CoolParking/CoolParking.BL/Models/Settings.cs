// TODO: implement class Settings.
//       Implementation details are up to you, they just have to meet the requirements of the home task.
using System.Collections.Generic;

namespace CoolParking.BL.Models
{
    static public class Settings
    {
        public static decimal ParkingStartBalance { get; } = 0;
        public static int ParkingCapacity { get; } = 10;
        public static int PaymentOffPeriod { get; } = 5000;
        public static int WritingLogPeriod { get; } = 60000;
        public static double PenaltyRatio { get; } = 2.5;

        public static Dictionary<VehicleType, decimal> vehicleParkingTariff = new Dictionary<VehicleType, decimal>()
        {
            {VehicleType.PassengerCar, 2m},
            {VehicleType.Truck, 5m},
            {VehicleType.Bus, 3.5m},
            {VehicleType.Motorcycle, 1m}
        };

    }
}