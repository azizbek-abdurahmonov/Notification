namespace Notification.Application.Common.Notification.Brokers;

public interface IEmailSenderBroker
{
    ValueTask<bool> SendAsync(
        string? senderEmailAddress,
        string receiverEmailAddress,
        string subject,
        string body,
        CancellationToken cancellationToken = default);
}
