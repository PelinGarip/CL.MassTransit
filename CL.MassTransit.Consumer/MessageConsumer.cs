using CL.MassTransit.Core.Abstract;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace CL.MassTransit.Consumer
{
    public class MessageConsumer : IConsumer<IMessage>
    {
        public async Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelen mesaj : {context.Message.Text}");
        }
    }
}
