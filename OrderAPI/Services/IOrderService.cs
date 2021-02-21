using System.Threading.Tasks;
using OrderAPI.Models;

namespace OrderAPI.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderRequest request);
        Task CompleteOrderAsync(int orderId);
        Task RejectOrderAsync(int orderId, string reason);
    }
}