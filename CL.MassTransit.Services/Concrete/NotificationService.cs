using CL.MassTransit.Core.Abstract;
using CL.MassTransit.Core.Concrete;
using CL.MassTransit.Core.Enums;
using CL.MassTransit.Services.Abstract;
using CL.MassTransit.Shared;
using CL.MassTransit.Shared.Model;
using Newtonsoft.Json;
using System;

namespace CL.MassTransit.Services.Concrete
{
    public class NotificationService : INotificationService
    {
        private readonly IBusConfigurator _busConfigurator;
        public NotificationService(IBusConfigurator busConfigurator)
        {
            _busConfigurator = busConfigurator;
        }

        public Result Send(NotificationTypes notificationType, NotificationModel notificationModel)
        {
            try
            {
                var queueName = notificationType == NotificationTypes.Email ?
                        RabbitMQQueueNames.Email :
                        RabbitMQQueueNames.SMS;

                var message = new Message
                {
                    Text = JsonConvert.SerializeObject(notificationModel)
                };

                _busConfigurator.SendMessage(queueName, message);

                return new Result(true, "Ok");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
