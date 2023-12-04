using Notification.Application.Common.Notification.Brokers;
using Notification.Application.Common.Notification.Services;

namespace Notification.Infrastructure.Common.Notification.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly IEnumerable<IEmailSenderBroker> _emailSenderBrokers;

    public EmailSenderService(IEnumerable<IEmailSenderBroker> emailSenderBrokers)
    {
        _emailSenderBrokers = emailSenderBrokers;
    }

    public async ValueTask<bool> SendAsync(string? senderEmailAddress, string receiverEmailAddress, string subject, string body, CancellationToken cancellationToken = default)
    {
        var result = false;
        foreach(var broker in _emailSenderBrokers)
        {
            try
            {
                result = await broker.SendAsync(senderEmailAddress, receiverEmailAddress, subject, body, cancellationToken);
                if (result) return result;
            }catch (Exception ex)
            {
                //TODO: log exceptions
            }
        }

        return result;
    }
}
