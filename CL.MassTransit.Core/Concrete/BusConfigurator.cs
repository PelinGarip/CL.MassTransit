using CL.MassTransit.Core.Abstract;
using CL.MassTransit.Core.Enums;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace CL.MassTransit.Core.Concrete
{
    public class BusConfigurator : IBusConfigurator
    {
        private readonly RabbitMQConfiguration _rabbitMQConfiguration;
        public BusConfigurator(RabbitMQConfiguration rabbitMQConfiguration)
        {
            _rabbitMQConfiguration = rabbitMQConfiguration;
        }

        private async Task<IBusControl> ConfigureBus()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
            {
                factory.Host(_rabbitMQConfiguration.HostName, configurator =>
                {
                    configurator.Username(_rabbitMQConfiguration.UserName);
                    configurator.Password(_rabbitMQConfiguration.Password);
                });
            });

            return bus;
        }

        public async Task SendMessage(RabbitMQQueueNames queue, IMessage message)
        {
            var bus = await this.ConfigureBus();

            var queueName = queue.ToString();
            var sendToUri = new Uri($"{_rabbitMQConfiguration.HostName}/{queueName}");
            var endPoint = await bus.GetSendEndpoint(sendToUri);

            await endPoint.Send<IMessage>(message);
        }
    }
}
