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
	class TransactionsHttpService : ITransactionsHttpService
	{
		private readonly HttpClient _client;
		public TransactionsHttpService()
		{
			_client = HttpClientFactory.Create();
		}

		public async Task<List<TransactionInfo>> GetLastTransactions()
		{
			try
			{
				var response = await _client.GetAsync("transactions/last");

				response.EnsureSuccessStatusCode();

				return await ReturnTransactions(response);
			}
			catch (Exception ex)
			{
				LogError(ex.Message);
				return null;
			}
		}

		public async Task<List<TransactionInfo>> AllFromLog()
		{
			try
			{
				var response = await _client.GetAsync("transactions/all");

				response.EnsureSuccessStatusCode();

				return await ReturnTransactions(response);
			}
			catch (Exception ex)
			{
				LogError(ex.Message);
				return null;
			}
		}

		public async Task<TopUpVehicleDto> TopUpVehicle(TopUpVehicleDto topUpVehicleDTO)
		{
			try
			{
				var tupUpParams = JsonConvert.SerializeObject(topUpVehicleDTO);

				var content = new StringContent(tupUpParams, Encoding.UTF8, "application/json");

				var response = await _client.PutAsync("transactions/topUpVehicle", content);

				response.EnsureSuccessStatusCode();

				var topUpJson = await response.Content.ReadAsStringAsync();

				var topUp = JsonConvert.DeserializeObject<TopUpVehicleDto>(topUpJson);

				return topUp;
			}
			catch (Exception ex)
			{
				LogError(ex.Message);
				return null;
			}
		}

		private async Task<List<TransactionInfo>> ReturnTransactions(HttpResponseMessage response)
		{
			var transactionsJson = await response.Content.ReadAsStringAsync();

			var transactions = JsonConvert.DeserializeObject<List<TransactionInfo>>(transactionsJson);

			return transactions;
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
