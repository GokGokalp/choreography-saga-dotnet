using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using OrderAPI.Services;
using Shared.Contracts;

namespace OrderAPI.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumeAsync<PaymentCompletedEvent>
    {
        private readonly IOrderService _orderService;

        public PaymentCompletedEventConsumer(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task ConsumeAsync(PaymentCompletedEvent message, CancellationToken cancellationToken = default)
        {
            await _orderService.CompleteOrderAsync(message.OrderId);
        }
    }
}