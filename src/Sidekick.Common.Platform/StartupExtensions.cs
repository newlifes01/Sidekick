using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Sidekick.Common.Platform.Clipboard;
using Sidekick.Common.Platform.Interprocess;
using Sidekick.Common.Platform.Keyboards;
using Sidekick.Common.Platform.Windows.Processes;
using Sidekick.Common.Platforms.Localization;

namespace Sidekick.Common.Platform
{
    /// <summary>
    /// Functions for startup configuration for platform related features
    /// </summary>
    public static class StartupExtensions
    {
        /// <summary>
        /// Adds platform (operating system) functions to the service collection
        /// </summary>
        /// <param name="services">The services collection to add services to</param>
        /// <returns>The service collection with services added</returns>
        public static IServiceCollection AddSidekickCommonPlatform(this IServiceCollection services, Action<PlatformOptions> options)
        {
            services.Configure(options);

            services.AddTransient<PlatformResources>();
            services.AddTransient<IClipboardProvider, ClipboardProvider>();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                services.AddSidekickInitializableService<IProcessProvider, ProcessProvider>();
            }

            services.AddSidekickInitializableService<IKeyboardProvider, KeyboardProvider>();

            services.AddSingleton<IInterprocessClient, InterprocessClient>();
            services.AddSingleton<IInterprocessService, InterprocessService>();

            return services;
        }
    }
}
