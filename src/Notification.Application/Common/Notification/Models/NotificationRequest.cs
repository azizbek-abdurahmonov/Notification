using Notification.Domain.Enums;

namespace Notification.Application.Common.Notification.Models;

public class NotificationRequest
{
    public Guid? SenderUserId { get; set; }

    public Guid ReceiverUserId { get; set; }

    public NotificationType NotificationType { get; set; }

    public NotificationTemplateType NotificationTemplateType { get; set; }

    public Dictionary<string, string> Variables { get; set; } = new Dictionary<string, string>();
}
