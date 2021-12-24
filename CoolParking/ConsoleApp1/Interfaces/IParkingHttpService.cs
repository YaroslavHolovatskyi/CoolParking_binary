using System;
using System.Threading.Tasks;

namespace ConsoleApplication.Interfaces
{
    public interface IParkingHttpService:IDisposable
    {
        Task<decimal?> GetBalance();
        Task<int?> GetCapacity();
        Task<int?> GetFreePlaces();
    }
}
