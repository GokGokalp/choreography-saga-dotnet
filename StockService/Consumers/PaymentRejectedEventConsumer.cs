using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Shared.Contracts;
using StockService.Services;

namespace StockService.Consumers
{
    public class PaymentRejectedEventConsumer : IConsumeAsync<PaymentRejectedEvent>
    {
        private readonly IStockService _stockService;
        private readonly IBus _bus;

        public PaymentRejectedEventConsumer(IStockService stockService, IBus bus)
        {
            _stockService = stockService;
            _bus = bus;
        }

        public async Task ConsumeAsync(PaymentRejectedEvent message, CancellationToken cancellationToken = default)
        {
            await _stockService.ReleaseStocksAsync(message.OrderId);

            await _bus.PubSub.PublishAsync(new StocksReleasedEvent
            {
                OrderId = message.OrderId,
                Reason = message.Reason
            });
        }
    }
}