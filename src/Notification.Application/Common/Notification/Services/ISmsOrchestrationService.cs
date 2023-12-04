using Notification.Domain.Enums;

namespace Notification.Application.Common.Notification.Services;

public interface ISmsOrchestrationService
{
    ValueTask<bool> SendAsync(
        string? senderPhoneNumber,
        string receiverPhoneNumber,
        NotificationTemplateType templateType,
        Dictionary<string, string> Variables,
        CancellationToken cancellationToken = default);
}
