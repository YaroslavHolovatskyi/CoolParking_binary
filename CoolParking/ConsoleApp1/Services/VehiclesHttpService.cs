using ConsoleApplication.Factories;
using ConsoleApplication.Interfaces;
using CoolParking.BL.Models;
using CoolParking.WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Services
{
	class VehiclesHttpService : IVehiclesHttpService
	{
		private readonly HttpClient _client;

		public VehiclesHttpService()
		{
			_client = HttpClientFactory.Create();
		}

		public async Task<List<VehicleDto>> GetVehicles()
		{
			try
			{
				var response = await _client.GetAsync("vehicles/vehicles");

				response.EnsureSuccessStatusCode();

				var vehiclesJson = await response.Content.ReadAsStringAsync();

				var vehicles = JsonConvert.DeserializeObject<List<VehicleDto>>(vehiclesJson);

				return vehicles;
			}
			catch (Exception ex)
			{
				LogError(ex.Message);
				return null;
			}
		}

		public async Task<VehicleDto> GetVehicleById(string id)
		{
			try
			{
				var response = await _client.GetAsync($"vehicles/{id}");

				response.EnsureSuccessStatusCode();

				return await JsonParse(response);
			}
			catch (Exception ex)
			{
				LogError(ex.Message);

				return null;
			}
		}
		public async Task<VehicleDto> AddVehicle(Vehicle vehicleToAdd)
		{
			try
			{
				var newVehicle = JsonConvert.SerializeObject(vehicleToAdd);

				var content = new StringContent(newVehicle, Encoding.UTF8, "application/json");

				var response = await _client.PostAsync("vehicles", content);

				response.EnsureSuccessStatusCode();

				return await JsonParse(response);
			}
			catch (Exception ex)
			{
				LogError(ex.Message);

				return null;
			}
		}

		public async Task<VehicleDto> DeleteVehicle(string id)
		{
			try
			{
				var response = await _client.DeleteAsync($"vehicles/{id}");

				response.EnsureSuccessStatusCode();

				return await JsonParse(response);
			}
			catch (Exception ex)
			{
				LogError(ex.Message);

				return null;
			}
		}

		private async Task<VehicleDto> JsonParse(HttpResponseMessage response)
		{
			var vehicleJson = await response.Content.ReadAsStringAsync();

			var vehicle = JsonConvert.DeserializeObject<VehicleDto>(vehicleJson);

			return vehicle;
		}

		private void LogError(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\n");
			Console.WriteLine(message);
			Console.WriteLine("\n");
			Console.ForegroundColor = ConsoleColor.White;
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}
