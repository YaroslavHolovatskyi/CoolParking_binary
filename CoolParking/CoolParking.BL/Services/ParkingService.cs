// TODO: implement the ParkingService class from the IParkingService interface.
//       For try to add a vehicle on full parking InvalidOperationException should be thrown.
//       For try to remove vehicle with a negative balance (debt) InvalidOperationException should be thrown.
//       Other validation rules and constructor format went from tests.
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in ParkingServiceTests you can find the necessary constructor format and validation rules.
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoolParking.BL.Services
{
    public class ParkingService : IParkingService
    {
        private readonly Parking parking = Parking.GetInstance();
        private readonly List<TransactionInfo> transactions = new List<TransactionInfo>(0);
        private readonly ITimerService _withdrawTimer;
        private readonly ITimerService _logTimer;
        private readonly ILogService _logService;

        public ParkingService(ITimerService withdrawTimer, ITimerService logTimer, ILogService logService)
        {
            _withdrawTimer = withdrawTimer;
            _logTimer = logTimer;
            _logService = logService;
            _withdrawTimer.Elapsed += MakeTransaction;
            _logTimer.Elapsed += WriteToLog;
            _logTimer.Interval = Settings.WritingLogPeriod;
            _logTimer.Start();
            _withdrawTimer.Interval = Settings.PaymentOffPeriod;
            _withdrawTimer.Start();
        }

        public decimal GetBalance()
        {
            return parking.Balance;
        }
        public int GetCapacity()
        {
            return parking.Vehicles.Capacity;
        }
        public int GetFreePlaces()
        {
            return parking.Vehicles.Capacity - parking.Vehicles.Count;
        }
        public ReadOnlyCollection<Vehicle> GetVehicles()
        {
            return parking.Vehicles.AsReadOnly();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (GetFreePlaces() == 0 || parking.Vehicles.Find(v => v.Id == vehicle.Id) != null)
            {
                throw new ArgumentException();
            }
            else
            {
                parking.Vehicles.Add(vehicle);
            }
        }

        public void RemoveVehicle(string vehicleId)
        {
            var removeVecicle = parking.Vehicles.Find(v => v.Id == vehicleId);
            if(removeVecicle == null ||removeVecicle.Balance < 0)
            {
                throw new ArgumentException();
            }
            else
            {
                parking.Vehicles.Remove(removeVecicle);
            }
        }
        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            var vehicle = parking.Vehicles.Find(v => v.Id == vehicleId);
            if(vehicle!=null && sum>0 && sum <= decimal.MaxValue)
            {
                parking.Vehicles.Find(v => v.Id == vehicleId).Balance += sum;
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public TransactionInfo[] GetLastParkingTransactions()
        {
            return transactions.ToArray();
        }
        public void MakeTransaction(object sender, EventArgs e)
        {
            foreach(var i in parking.Vehicles)
            {
                decimal tarrif = Settings.VehicleTariff(i.VehicleType);
                decimal sum;
                if (i.Balance > 0 && i.Balance - tarrif > 0)
                {
                    sum = tarrif;
                }
                else
                {
                    sum = tarrif * (decimal)Settings.PenaltyRatio;
                }
                parking.WithdrawSum(i.Id, sum);
                transactions.Add(new TransactionInfo() { vehicleId = i.Id, Sum = sum, transactionTime = DateTime.Now });
            }
        }
        public void WriteToLog(object sender, EventArgs e)
        {
            string writeToFile = "";
            for(int i = 0; i < transactions.Count; i++)
            {
                writeToFile += transactions[i].transactionTime.ToString() + "\t" + transactions[i].vehicleId + "\t" + transactions[i].Sum + "\n";
            }
            _logService.Write(writeToFile);
            transactions.Clear();
        }
        public string ReadFromLog()
        {
            return _logService.Read();
        }
        public void Dispose()
        {
            parking.Dispose();
            transactions.Clear();
        }
    }

}