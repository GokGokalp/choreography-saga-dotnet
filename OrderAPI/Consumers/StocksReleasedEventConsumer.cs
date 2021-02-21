using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using OrderAPI.Services;
using Shared.Contracts;

namespace OrderAPI.Consumers
{
    public class StocksReleasedEventConsumer : IConsumeAsync<StocksReleasedEvent>
    {
        private readonly IOrderService _orderService;

        public StocksReleasedEventConsumer(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task ConsumeAsync(StocksReleasedEvent message, CancellationToken cancellationToken = default)
        {
            await _orderService.RejectOrderAsync(message.OrderId, message.Reason);
        }
    }
}