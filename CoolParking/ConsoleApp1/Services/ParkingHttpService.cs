using ConsoleApplication.Factories;
using ConsoleApplication.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApplication.Services
{
    public class ParkingHttpService :IParkingHttpService
    {
        private readonly HttpClient _client;
        public ParkingHttpService()
        {
            _client = HttpClientFactory.Create();
        }
        public async Task<int?> GetFreePlaces()
        {
            try
            {
                var response = await _client.GetAsync("parking/freePlaces");
                response.EnsureSuccessStatusCode();
                var freePlaces = await response.Content.ReadAsStringAsync();
                return int.Parse(freePlaces); 
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                return null;
            }
        }
        public async Task<int?> GetCapacity()
        {
            try
            {
                var response = await _client.GetAsync("parking/capacity");

                response.EnsureSuccessStatusCode();

                var capacity = await response.Content.ReadAsStringAsync();

                return int.Parse(capacity);

            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                return null;
            }
        }
        public async Task<decimal?> GetBalance()
        {
            try
            {
                var response = await _client.GetAsync("parking/balance");

                response.EnsureSuccessStatusCode();

                var balance = await response.Content.ReadAsStringAsync();
                return decimal.Parse(balance);
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                return null;
            }
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
