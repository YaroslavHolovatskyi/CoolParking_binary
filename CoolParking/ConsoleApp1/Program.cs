using ConsoleApplication.Services;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Services;
using CoolParking.WebAPI.Models;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

		static async Task Main(string[] args)
		{
			var parkingHttpService = new ParkingHttpService();
			var vehiclesHttpService = new VehiclesHttpService();
			var transactionsHttpService = new TransactionsHttpService();

			do
			{
				Console.WriteLine("\t\tMenu");
				Console.WriteLine("\t - Parking balance - 1\n\t " +
					"- Capacity - 2\n\t " +
					"- Free places - 3\n\t " +
					"- Vehicles at the parking - 4\n\t " +
					"- Put vehicle - 5\n\t " +
					"- Get vehicle from the parking - 6\n\t " +
					"- Transactions for a period - 7\n\t " +
					"- Top up vehicle - 8\n\t " +
					"- All transactions - 9\n\t " +
					" Or write exit to out");
				var input = Console.ReadLine();
				Regex reg = new Regex(@"^[1-9]{1}$");
				if (!reg.IsMatch(input) && input != "10" && input.ToUpper() != "EXIT")
				{
					Console.WriteLine("No such option");
				}
				else
				{
					if (input.ToUpper() == "EXIT")
					{
						break;
					}
					HttpClient client = new HttpClient();
					switch (Int32.Parse(input))
					{
						case 1:
							var balance = await parkingHttpService.GetBalance();
							Console.WriteLine($"Balance is {balance}");
							break;

						case 2:
							var capacity = await parkingHttpService.GetCapacity();
							Console.WriteLine($"Capacity is {capacity}");
							break;

						case 3:
							var freePlaces = await parkingHttpService.GetFreePlaces();
							Console.WriteLine($"Free places: {freePlaces}");
							break;
						case 4:
							var vehicles = await vehiclesHttpService.GetVehicles();
							if (vehicles.Count == 0)
							{
								Console.WriteLine("No vehicles at the parking ");
							}
							else
							{
								Console.WriteLine("Vehicles at the parking: ");
								foreach (var oneVehicle in vehicles)
								{
									Console.WriteLine($"{oneVehicle.Id} - {oneVehicle.VehicleType} - {oneVehicle.Balance}");
								}
							}
							break;

						case 5:
							await vehiclesHttpService.AddVehicle(new Vehicle(Vehicle.GenerateRandomRegistrationPlateNumber(), ChooseVehicle(), PutBalance()));
							break;

						case 6:
							await vehiclesHttpService.DeleteVehicle(InputId());
							Console.WriteLine("Vehicle successfully get");
							break;

						case 7:
							var transactionsPeriod = await transactionsHttpService.GetLastTransactions();
							Console.WriteLine("Transactions for a period: ");
							if (transactionsPeriod.Count == 0)
							{
								Console.WriteLine("No transactions for a period");
							}
							else
							{
								foreach (var transaction in transactionsPeriod)
								{
									Console.WriteLine($"{transaction.vehicleId} - {transaction.Sum} - {transaction.transactionTime}");
								}
							}
							break;

                        case 8:
							Console.WriteLine("Enter sum: ");
							decimal sum = decimal.Parse(Console.ReadLine());
							await transactionsHttpService.TopUpVehicle(new TopUpVehicleDto() { Id = InputId(), Sum = sum });
							Console.WriteLine("Successfull operation");
							break;
						case 9:
							var transactions = await transactionsHttpService.AllFromLog();
							Console.WriteLine("All transactions: ");
							if (transactions == null)
							{
								Console.WriteLine("No transactions at all");
							}
							else
							{
								foreach (var transaction in transactions)
								{
									Console.WriteLine($"{transaction.vehicleId} - {transaction.Sum} - {transaction.transactionTime}");
								}
							}
							break;
					}
				}
			}
			while (true);
		}
		private static decimal PutBalance()
		{
			Console.WriteLine("Write your balance: ");
			decimal balance = Decimal.Parse(Console.ReadLine());
			while (balance < 0 || balance > Decimal.MaxValue)
			{
				Console.WriteLine("Fake balance. Try again: ");
				balance = Decimal.Parse(Console.ReadLine());
			}
			return balance;
		}
		private static VehicleType ChooseVehicle()
		{
			Console.WriteLine("Choose vehicle: Passenger Car - 1, Truck - 2, Bus - 3, Motorcycle - 4: ");
			int vehicle = int.Parse(Console.ReadLine());
			while (vehicle != 1 && vehicle != 2 && vehicle != 3 && vehicle != 4)
			{
				Console.WriteLine("No such car, try again: ");
				vehicle = int.Parse(Console.ReadLine());
			}
			switch (vehicle)
			{
				case 1:
					return VehicleType.PassengerCar;
				case 2:
					return VehicleType.Truck;
				case 3:
					return VehicleType.Bus;
				case 4:
					return VehicleType.Motorcycle;
			}
			return 0;
		}
		private static string InputId()
		{
			string inputId;
			do
			{
				Console.WriteLine("Enter id: ");
				inputId = Console.ReadLine();
			} while (!Vehicle.idValidation(inputId));

			return inputId;
		}
	}
}
