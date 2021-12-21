// TODO: implement class Settings.
//       Implementation details are up to you, they just have to meet the requirements of the home task.
namespace CoolParking.BL.Models
{
    static public class Settings
    {
        public static decimal ParkingStartBalance { get; } = 0;
        public static int ParkingCapacity { get; } = 10;
        public static int PaymentOffPeriod { get; } = 5000;
        public static int WritingLogPeriod { get; } = 60000;
        public static double PenaltyRatio { get; } = 2.5;
        public static decimal VehicleTariff(VehicleType vehicle)
        {
            switch (vehicle)
            {
                case VehicleType.PassengerCar:
                    return 2;
                case VehicleType.Truck:
                    return 5;
                case VehicleType.Bus:
                    return 3.5M;
                case VehicleType.Motorcycle:
                    return 1;
            }
            return 0;
        }

    }
}