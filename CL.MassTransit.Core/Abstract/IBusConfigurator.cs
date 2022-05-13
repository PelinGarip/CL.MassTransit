using CL.MassTransit.Core.Enums;
using MassTransit;
using System.Threading.Tasks;

namespace CL.MassTransit.Core.Abstract
{
    public interface IBusConfigurator
    {
        public Task SendMessage(RabbitMQQueueNames queue, IMessage message);
    }
}
