using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts;

namespace StockService.Services
{
    public interface IStockService
    {
        Task ReserveStocksAsync(int orderId);
        Task<bool> ReleaseStocksAsync(int orderId);
    }
}