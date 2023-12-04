namespace Notification.Application.Common.Notification.Brokers;

public interface ISmsSenderBroker
{
    ValueTask<bool> SendAsync(
        string? senderPhoneNumber,
        string receiverPhoneNumber,
        string message,
        CancellationToken cancellationToken);
}
