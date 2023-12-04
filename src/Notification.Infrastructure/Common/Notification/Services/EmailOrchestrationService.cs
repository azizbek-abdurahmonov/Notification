using Notification.Application.Common.Notification.Services;
using Notification.Domain.Enums;
using System.Text;
using System.Text.RegularExpressions;

namespace Notification.Infrastructure.Common.Notification.Services;

public class EmailOrchestrationService : IEmailOrchestrationService
{
    private readonly IEmailSenderService _emailSenderService;

    public EmailOrchestrationService(IEmailSenderService emailSenderService)
    {
        _emailSenderService = emailSenderService;
    }

    public async ValueTask<bool> SendAsync(string? senderEmailAddress, string receiverEmailAddress, NotificationTemplateType notificationTemplateType, Dictionary<string, string> Variables, CancellationToken cancellationToken = default)
    {
        var template = GetTemplate(notificationTemplateType);

        var message = Render(template, Variables);

        var result = await _emailSenderService.SendAsync(
            senderEmailAddress,
            receiverEmailAddress,
            null,
            message,
            cancellationToken);

        return result;

    }

    private string GetTemplate(NotificationTemplateType templateType)
    {
        return templateType switch
        {
            NotificationTemplateType.SystemWelcomeNotification => "Hi {{User}}, Welcome yo our system",
            NotificationTemplateType.EmailVerificationNotification => "Hi {{User}} , Link for verificate email!",
            _ => throw new ArgumentException("Invalid notification template type")
        };
    }

    private string Render(string template, Dictionary<string, string> variables)
    {
        var placeholderRegex = new Regex("\\{\\{([^\\{\\}]+)\\}\\}",
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(5));

        var placeholderValueRegex = new Regex("{{(.*?)}}",
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(5));

        var matches = placeholderRegex.Matches(template);

        if (matches.Any() && !variables.Any())
            throw new InvalidOperationException("Variables are required for this template");

        var placeholders = matches.Select(match =>
        {
            var placeholder = match.Value;
            var placeholderValue = placeholderValueRegex.Match(placeholder).Groups[1].Value;
            var valid = variables.TryGetValue(placeholderValue, out var value);

            return new
            {
                Placeholder = placeholder,
                PlaceholderValue = placeholderValue,
                Value = value,
                IsValid = valid,
            };
        });

        var messageBuilder = new StringBuilder(template);

        placeholders.ToList().ForEach(placeholder => messageBuilder.Replace(placeholder.Placeholder, placeholder.Value));

        return messageBuilder.ToString();
    }
}
