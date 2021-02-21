using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Shared.Contracts;
using StockService.Services;

namespace StockService.Consumers
{
    public class OrderCreatedEventConsumer : IConsumeAsync<OrderCreatedEvent>
    {
        private readonly IStockService _stockService;
        private readonly IBus _bus;

        public OrderCreatedEventConsumer(IStockService stockService, IBus bus)
        {
            _stockService = stockService;
            _bus = bus;
        }

        public async Task ConsumeAsync(OrderCreatedEvent message, CancellationToken cancellationToken = default)
        {
            await _stockService.ReserveStocksAsync(message.OrderId);

            await _bus.PubSub.PublishAsync(new StocksReservedEvent
            {
                UserId = message.UserId,
                OrderId = message.OrderId,
                WalletId = message.WalletId,
                TotalAmount = message.TotalAmount
            });
        }
    }
}