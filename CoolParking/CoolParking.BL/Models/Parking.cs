// TODO: implement class Parking.
//       Implementation details are up to you, they just have to meet the requirements 
//       of the home task and be consistent with other classes and tests.
using System.Collections.Generic;
using System.Linq;
using System;

namespace CoolParking.BL.Models
{
    public class Parking
    {
        private static Parking parking;
        private List<Vehicle> vehicles;

        public decimal Balance { get; set; }
         public List<Vehicle> Vehicles
        {
            get { 
                return vehicles;
            }
        }
        private Parking()
        {
            vehicles = new List<Vehicle>() { Capacity = 10 };
        }
        public static Parking getInstance()
        {
            if (parking != null)
            {
                return parking;
            }
            else
            {
                parking = new Parking();
                return parking;
            }
        }
        public void WithdrawSum(string vehicleId, decimal sum)
        {
            var vehicle = from car in vehicles
                          where car.Id == vehicleId
                          select car;
            if(vehicle != null)
            {
                vehicle.First().Balance -= sum;
                Balance += sum;
            }
        }
        public void Dispose()
        {
            Vehicles.Clear();
            Balance = 0;
        }
    }
}