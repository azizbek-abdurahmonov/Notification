using Notification.Application.Common.Notification.Brokers;
using Notification.Application.Common.Notification.Services;
using Notification.Infrastructure.Common.Notification.Brokers;
using Notification.Infrastructure.Common.Notification.Services;
using Notification.Infrastructure.Common.Notification.Settings;
using System.Globalization;
namespace Notification.Api.Configuration;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddNotificationsInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<SmtpEmailSenderSettings>(builder.Configuration.GetSection(nameof(SmtpEmailSenderSettings)));

        builder.Services.Configure<TwilioSmsSenderSettings>(builder.Configuration.GetSection(nameof(TwilioSmsSenderSettings)));

        builder.Services
            .AddScoped<IEmailSenderBroker, SmtpEmailSenderBroker>()
            .AddScoped<ISmsSenderBroker, TwilioSmsSenderBroker>();

        builder.Services
            .AddScoped<IEmailSenderService, EmailSenderService>()
            .AddScoped<IEmailOrchestrationService, EmailOrchestrationService>();

        builder.Services
            .AddScoped<ISmsSenderService, SmsSenderService>()
            .AddScoped<ISmsOrchestrationService, SmsOrchestrationService>();

        builder.Services.AddScoped<INotificationAggregatorService, NotificationAggregatorService>();


        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddSwaggerGen()
            .AddEndpointsApiExplorer();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        return builder;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app
            .MapControllers();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app
            .UseSwagger()
            .UseSwaggerUI();

        return app;
    }
}