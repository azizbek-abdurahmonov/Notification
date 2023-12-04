using Notification.Application.Common.Notification.Brokers;
using System.Net.Mail;
using System.Net;
using Notification.Infrastructure.Common.Notification.Settings;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Extensions.Options;

namespace Notification.Infrastructure.Common.Notification.Brokers;

public class SmtpEmailSenderBroker : IEmailSenderBroker
{
    private readonly SmtpEmailSenderSettings _senderSettings;

    public SmtpEmailSenderBroker(IOptions<SmtpEmailSenderSettings> senderSettings)
    {
        _senderSettings = senderSettings.Value;
    }

    public ValueTask<bool> SendAsync(string? senderEmailAddress, string receiverEmailAddress, string subject, string body, CancellationToken cancellationToken = default)
    {
        senderEmailAddress ??= _senderSettings.CredentialAddress;

        var mail = new MailMessage(senderEmailAddress, receiverEmailAddress);
        mail.Subject = subject;
        mail.Body = body;

        var smtpClient = new SmtpClient(_senderSettings.Host, _senderSettings.Port);
        smtpClient.Credentials =
            new NetworkCredential(_senderSettings.CredentialAddress, _senderSettings.Password);
        smtpClient.EnableSsl = true;

        smtpClient.Send(mail);

        return new ValueTask<bool>(true);
    }

}
