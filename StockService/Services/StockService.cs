using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts;

namespace StockService.Services
{
    public class StockService : IStockService
    {
        public Task<bool> ReleaseStocksAsync(int orderId)
        {
            // Stock release logic

            return Task.FromResult(true);
        }

        public Task ReserveStocksAsync(int orderId)
        {
            // Stock reserve logic

            return Task.CompletedTask;
        }
    }
}