using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using OrderAPI.Models;
using Shared.Contracts;

namespace OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBus _bus;

        public OrderService(IBus bus)
        {
            _bus = bus;
        }

        public async Task CreateOrderAsync(CreateOrderRequest request)
        {
            // Order creation logic in "Pending" state.

            await _bus.PubSub.PublishAsync(new OrderCreatedEvent
            {
                UserId = 1,
                OrderId = 1,
                WalletId = 1,
                TotalAmount = request.TotalAmount,
            });
        }

        public Task CompleteOrderAsync(int orderId)
        {
            // Change the order status as completed.

            return Task.CompletedTask;
        }

        public Task RejectOrderAsync(int orderId, string reason)
        {
            // Change the order status as rejected.

            return Task.CompletedTask;
        }
    }
}