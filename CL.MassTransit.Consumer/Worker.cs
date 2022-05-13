using CL.MassTransit.Core;
using CL.MassTransit.Core.Enums;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;

namespace CL.MassTransit.Consumer
{
    public class Worker : BackgroundService
    {
        private IBusControl _bus;
        private readonly RabbitMQConfiguration _rabbitMQConfiguration;

        public Worker(RabbitMQConfiguration rabbitMQConfiguration)
        {
            _rabbitMQConfiguration = rabbitMQConfiguration;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(factory =>
            {
                factory.Host(_rabbitMQConfiguration.HostName, configurator =>
                {
                    configurator.Username(_rabbitMQConfiguration.UserName);
                    configurator.Password(_rabbitMQConfiguration.Password);
                });

                factory.ReceiveEndpoint(RabbitMQQueueNames.Email.ToString(), endpoint => 
                {
                    endpoint.Consumer<MessageConsumer>();
                    endpoint.Bind("EmailExchange", x =>
                    {
                        x.ExchangeType = ExchangeType.Direct;
                        x.RoutingKey = "EmailRoutingKey";
                    });

                    endpoint.UseMessageRetry(r => r.Immediate(5));

                });
            });

            await _bus.StartAsync();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bus.StopAsync();
        }
    }
}
