namespace Notification.Application.Common.Notification.Services;

public interface ISmsSenderService
{
    ValueTask<bool> SendAsync(
        string? senderPhoneNumber,
        string receiverPhoneNumber,
        string message,
        CancellationToken cancellationToken = default);
}
