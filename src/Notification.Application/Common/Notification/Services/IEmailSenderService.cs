namespace Notification.Application.Common.Notification.Services;

public interface IEmailSenderService
{
    ValueTask<bool> SendAsync(
        string? senderEmailAddress,
        string receiverEmailAddress,
        string subject,
        string body,
        CancellationToken cancellationToken = default);
}
