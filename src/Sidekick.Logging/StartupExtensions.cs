using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sentry;
using Serilog;
using Sidekick.Domain.Settings;

namespace Sidekick.Logging
{

    public static class StartupExtensions
    {
        public static IServiceCollection AddSidekickLogging(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            var logSink = new LogSink();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Sidekick_log.log"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 1,
                    fileSizeLimitBytes: 5242880,
                    rollOnFileSizeLimit: true)
                .WriteTo.Sink(logSink)
                .AddSentryLogging(configuration, environment)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.AddSerilog();
            });

            services.AddSingleton(logSink);

            return services;
        }

        public static LoggerConfiguration AddSentryLogging(this LoggerConfiguration loggerConfiguration, IConfiguration configuration, IHostEnvironment environment)
        {
            loggerConfiguration
                .WriteTo
                .Conditional(x => configuration.GetValue<bool>(nameof(ISidekickSettings.SendCrashReports)),
                             c => c.Sentry(o =>
                             {
                                 o.Dsn = "https://7182a08eae7443a8a1b6aae8e64a0adb@o152592.ingest.sentry.io/5645809";
                                 o.Environment = environment.EnvironmentName;
                                 o.BeforeSend += e =>
                                 {
                                     e.User = new User() { Id = configuration.GetValue<string>(nameof(ISidekickSettings.UserId)), Username = configuration.GetValue<string>(nameof(ISidekickSettings.Character_Name)) };
                                     return e;
                                 };
                             }));

            return loggerConfiguration;
        }

    }
}
