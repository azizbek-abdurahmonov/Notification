using Microsoft.AspNetCore.Mvc;
using Notification.Application.Common.Notification.Models;
using Notification.Application.Common.Notification.Services;

namespace Notification.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationAggregatorService _notificationAggregatorService;

    public NotificationsController(INotificationAggregatorService notificationAggregatorService)
    {
        _notificationAggregatorService = notificationAggregatorService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> Send([FromBody] NotificationRequest notificationRequest)
    {
        var result = await _notificationAggregatorService.SendAsync(notificationRequest, HttpContext.RequestAborted);

        return result ? Ok(result) : BadRequest();
    }
}
