using Notification.Application.Common.Notification.Models;
using Notification.Application.Common.Notification.Services;
using Notification.Domain.Entities;
using Notification.Domain.Enums;

namespace Notification.Infrastructure.Common.Notification.Services;

public class NotificationAggregatorService : INotificationAggregatorService
{
    private readonly IEmailOrchestrationService _emailOrchestrationService;
    private readonly ISmsOrchestrationService _smsOrchestrationService;

    public NotificationAggregatorService(IEmailOrchestrationService emailOrchestrationService, ISmsOrchestrationService smsOrchestrationService)
    {
        _emailOrchestrationService = emailOrchestrationService;
        _smsOrchestrationService = smsOrchestrationService;
    }

    public async ValueTask<bool> SendAsync(NotificationRequest notificationRequest, CancellationToken cancellationToken = default)
    {
        var user1 = new User
        {
            Id = Guid.Parse("750c0ab1-de95-4f6e-b3b9-cc5d13a152dc"),
            FirstName = "Falonchi",
            LastName = "Falonchi",
            Email = "falonchi@gmail.com",
            PhoneNumber = "+12345678",
            Password = "Password"
        };

        var system = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "System",
            LastName = "System",
            Email = "sultonbek.rakhimov.recovery@gmail.com",
            PhoneNumber = "+12345678",
            Password = "Password"
        };

        var user2 = new User
        {
            Id = Guid.Parse("5e7edbee-5e39-4398-8032-ba6a2c970a72"),
            FirstName = "Azizbek",
            LastName = "Abdurahmonov",
            Email = "abdura52.uz@gmail.com",
            PhoneNumber = "+998970183595",
            Password = "Password"
        };

        var senderUser = notificationRequest.SenderUserId.HasValue ? user1 : system;

        var result = notificationRequest.NotificationType switch
        {
            NotificationType.Email => _emailOrchestrationService.SendAsync(senderUser.Email, user2.Email, notificationRequest.NotificationTemplateType, notificationRequest.Variables, cancellationToken),
            NotificationType.Sms => _smsOrchestrationService.SendAsync(senderUser.PhoneNumber, user2.PhoneNumber, notificationRequest.NotificationTemplateType, notificationRequest.Variables, cancellationToken),
            _ => throw new InvalidOperationException("Invalid operation!")
        };

        return await result;
    }
}
