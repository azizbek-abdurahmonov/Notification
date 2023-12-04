using Microsoft.Extensions.Options;
using Notification.Application.Common.Notification.Brokers;
using Notification.Infrastructure.Common.Notification.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Notification.Infrastructure.Common.Notification.Brokers;

public class TwilioSmsSenderBroker : ISmsSenderBroker
{
    private readonly TwilioSmsSenderSettings _twilioSmsSenderSettings;

    public TwilioSmsSenderBroker(IOptions<TwilioSmsSenderSettings> smsSenderSettings)
    {
        _twilioSmsSenderSettings = smsSenderSettings.Value;
    }

    public ValueTask<bool> SendAsync(string? senderPhoneNumber, string receiverPhoneNumber, string message, CancellationToken cancellationToken)
    {
        TwilioClient.Init(_twilioSmsSenderSettings.AccountSid, _twilioSmsSenderSettings.AuthToken);

        var messageContent = MessageResource.Create(
            body: message,
            from: new Twilio.Types.PhoneNumber(_twilioSmsSenderSettings.SenderPhoneNumber),
            to: new Twilio.Types.PhoneNumber(receiverPhoneNumber)
        );

        return new ValueTask<bool>(true);
    }
}
