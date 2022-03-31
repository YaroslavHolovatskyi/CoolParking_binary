using CoolParking.BL.Models;
using CoolParking.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApplication.Interfaces
{
	interface ITransactionsHttpService : IDisposable
	{
		Task<List<TransactionInfo>> GetLastTransactions();
		Task<List<TransactionInfo>> AllFromLog();
		Task<TopUpVehicleDto> TopUpVehicle(TopUpVehicleDto topUpVehicleDTO);
	}
}
