using System.Reflection;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockService.Consumers;
using StockService.Services;

namespace StockService
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IStockService, Services.StockService>();

                    var bus = RabbitHutch.CreateBus(hostContext.Configuration["RabbitMQ:ConnectionString"]);

                    services.AddSingleton<IBus>(bus);
                    services.AddSingleton<MessageDispatcher>();
                    services.AddSingleton<AutoSubscriber>(_ =>
                    {
                        return new AutoSubscriber(_.GetRequiredService<IBus>(), Assembly.GetExecutingAssembly().GetName().Name)
                        {
                            AutoSubscriberMessageDispatcher = _.GetRequiredService<MessageDispatcher>()
                        };
                    });

                    services.AddScoped<OrderCreatedEventConsumer>();
                    services.AddScoped<PaymentRejectedEventConsumer>();

                    services.AddHostedService<Worker>();
                });
    }
}