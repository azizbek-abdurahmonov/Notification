using Notification.Domain.Enums;

namespace Notification.Application.Common.Notification.Services;

public interface IEmailOrchestrationService
{
    ValueTask<bool> SendAsync(
        string? senderEmailAddress,
        string receiverEmailAddress,
        NotificationTemplateType notificationTemplateType,
        Dictionary<string, string> Variables,
        CancellationToken cancellationToken = default);
}
