using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using PaymentService.Services;
using Shared.Contracts;

namespace PaymentService.Consumers
{
    public class StocksReservedEventConsumer : IConsumeAsync<StocksReservedEvent>
    {
        private readonly IPaymentService _paymentService;
        private readonly IBus _bus;

        public StocksReservedEventConsumer(IPaymentService paymentService, IBus bus)
        {
            _paymentService = paymentService;
            _bus = bus;
        }

        public async Task ConsumeAsync(StocksReservedEvent message, CancellationToken cancellationToken = default)
        {   
            Tuple<bool, string> isPaymentCompleted = await _paymentService.DoPaymentAsync(message.WalletId, message.UserId, message.TotalAmount);

            if (isPaymentCompleted.Item1)
            {
                await _bus.PubSub.PublishAsync(new PaymentCompletedEvent
                {
                    OrderId = message.OrderId
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new PaymentRejectedEvent
                {
                    OrderId = message.OrderId,
                    Reason = isPaymentCompleted.Item2
                });
            }
        }
    }
}