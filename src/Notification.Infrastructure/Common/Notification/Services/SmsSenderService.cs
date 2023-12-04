using Notification.Application.Common.Notification.Brokers;
using Notification.Application.Common.Notification.Services;

namespace Notification.Infrastructure.Common.Notification.Services;

public class SmsSenderService : ISmsSenderService
{
    private IEnumerable<ISmsSenderBroker> _smsSenderBrokers;

    public SmsSenderService(IEnumerable<ISmsSenderBroker> smsSenderBrokers)
    {
        _smsSenderBrokers = smsSenderBrokers;
    }

    public async ValueTask<bool> SendAsync(string? senderPhoneNumber, string receiverPhoneNumber, string message, CancellationToken cancellationToken = default)
    {
        var result = false;

        foreach (var senderBroker in _smsSenderBrokers)
        {
            try
            {
                result = await senderBroker.SendAsync(senderPhoneNumber, receiverPhoneNumber, message, cancellationToken);
                if (result) return result;
            }
            catch
            {
                //TODO: log exceptions
            }
        }

        return result;
    }
}
