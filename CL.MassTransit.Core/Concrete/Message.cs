using CL.MassTransit.Core.Abstract;

namespace CL.MassTransit.Core.Concrete
{
    public class Message : IMessage
    {
        public string Text { get; set; }
    }
}
