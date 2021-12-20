// TODO: implement class Vehicle.
//       Properties: Id (string), VehicleType (VehicleType), Balance (decimal).
//       The format of the identifier is explained in the description of the home task.
//       Id and VehicleType should not be able for changing.
//       The Balance should be able to change only in the CoolParking.BL project.
//       The type of constructor is shown in the tests and the constructor should have a validation, which also is clear from the tests.
//       Static method GenerateRandomRegistrationPlateNumber should return a randomly generated unique identifier.
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CoolParking.BL.Models
{
    public class Vehicle
    {
        public string Id { get; }
        public VehicleType VehicleType { get; }
        public decimal Balance { get; internal set; }
        public Vehicle(string id, VehicleType vehicleType, decimal balance)
        {
            if (idValidation(id))
                Id = id;
            else 
                throw new ArgumentException();

            VehicleType = vehicleType;
            if (balance > 0 && balance <= Decimal.MaxValue)
                Balance = balance;
            else
                throw new ArgumentException();
        }

        public static string GenerateRandomRegistrationPlateNumber()
        {
            List<string> idList = new List<string>();
            Random random = new Random();
            string id = "";
            do
            {
                id += ((char)random.Next(68, 91)).ToString() + ((char)random.Next(68, 91)).ToString() + '-';
                id += random.Next(0, 10).ToString() + random.Next(0, 10).ToString() + random.Next(0, 10).ToString() + random.Next(0, 10).ToString();
                id += '-' + ((char)random.Next(68, 91)).ToString() + ((char)random.Next(68, 91)).ToString();

            } while (idList.Contains(id));

            idList.Add(id);
            return id;
        }

        private static bool idValidation(string Id)
        {
            Regex regex = new Regex(@"^[A-Z]{2}-\d{4}-^[A-Z]{2}$");
            if (regex.IsMatch(Id))
                return true;
            else
                return false;
        }

    }
}