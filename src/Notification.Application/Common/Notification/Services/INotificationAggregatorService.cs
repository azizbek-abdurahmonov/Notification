using Notification.Application.Common.Notification.Models;
using Notification.Domain.Enums;

namespace Notification.Application.Common.Notification.Services;

public interface INotificationAggregatorService
{

    ValueTask<bool> SendAsync(
       NotificationRequest notificationRequest,
       CancellationToken cancellationToken = default);
}