using CoolParking.BL.Models;
using CoolParking.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApplication.Interfaces
{
	public interface IVehiclesHttpService : IDisposable
	{
		Task<List<VehicleDto>> GetVehicles();
		Task<VehicleDto> GetVehicleById(string id);
		Task<VehicleDto> AddVehicle(Vehicle vehicle);
		Task<VehicleDto> DeleteVehicle(string id);
	}
}
