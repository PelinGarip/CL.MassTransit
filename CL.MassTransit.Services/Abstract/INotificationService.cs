using CL.MassTransit.Core.Enums;
using CL.MassTransit.Shared;
using CL.MassTransit.Shared.Model;

namespace CL.MassTransit.Services.Abstract
{
    public interface INotificationService
    {
        Result Send(NotificationTypes notificationType, NotificationModel notificationModel);
    }
}
