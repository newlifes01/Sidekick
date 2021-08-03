using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sidekick.Application.Game.Items.Parser.Patterns;
using Sidekick.Application.Game.Languages;
using Sidekick.Application.Keybinds;
using Sidekick.Domain.Game.Languages;

namespace Sidekick.Application
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSidekickApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IGameLanguageProvider, GameLanguageProvider>();
            services.AddSingleton<IParserPatterns, ParserPatterns>();

            // Keybind handlers
            services.AddSingleton<ChatKeybindHandler>();
            services.AddSingleton<CloseOverlayKeybindHandler>();
            services.AddSingleton<FindItemKeybindHandler>();
            services.AddSingleton<OpenCheatsheetsKeybindHandler>();
            services.AddSingleton<OpenMapInfoKeybindHandler>();
            services.AddSingleton<OpenSettingsKeybindHandler>();
            services.AddSingleton<OpenTradePageKeybindHandler>();
            services.AddSingleton<OpenWikiPageKeybindHandler>();
            services.AddSingleton<PriceCheckItemKeybindHandler>();
            services.AddSingleton<ScrollStashDownKeybindHandler>();
            services.AddSingleton<ScrollStashUpKeybindHandler>();

            return services;
        }
    }
}
