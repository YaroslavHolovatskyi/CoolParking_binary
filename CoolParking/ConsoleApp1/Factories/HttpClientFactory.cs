using System;
using System.Net.Http;

namespace ConsoleApplication.Factories
{
    internal static class HttpClientFactory
    {
        internal static HttpClient Create()
        {
            return new HttpClient() { BaseAddress = new Uri("http://localhost:8104/api/") }; 
        }
    }
}
