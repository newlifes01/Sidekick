using Sidekick.Apis.Poe.Clients;
using Sidekick.Apis.Poe.Static.Models;
using Sidekick.Common.Cache;
using Sidekick.Common.Game.Items;
using Sidekick.Common.Game.Languages;
using Sidekick.Common.Initialization;

namespace Sidekick.Apis.Poe.Static
{
    public class ItemStaticDataProvider(
        ICacheProvider cacheProvider,
        IPoeTradeClient poeTradeClient,
        IGameLanguageProvider gameLanguageProvider) : IItemStaticDataProvider
    {
        private Dictionary<string, string> ImageUrls { get; set; } = new();
        private Dictionary<string, string> Ids { get; set; } = new();

        /// <inheritdoc/>
        public InitializationPriority Priority => InitializationPriority.Medium;

        /// <inheritdoc/>
        public async Task Initialize()
        {
            var result = await cacheProvider.GetOrSet(
                "ItemStaticDataProvider",
                () => poeTradeClient.Fetch<StaticItemCategory>("data/static"));

            ImageUrls.Clear();
            Ids.Clear();
            foreach (var category in result.Result)
            {
                foreach (var entry in category.Entries)
                {
                    if (entry.Id == null || entry.Image == null || entry.Text == null)
                    {
                        continue;
                    }

                    ImageUrls.Add(entry.Id, entry.Image);
                    if (!Ids.ContainsKey(entry.Text))
                    {
                        Ids.Add(entry.Text, entry.Id);
                    }
                }
            }
        }

        public string? GetImage(string id)
        {
            id = id switch
            {
                "exalt" => "exalted",
                _ => id
            };

            if (gameLanguageProvider.Language == null || string.IsNullOrEmpty(id) || !ImageUrls.TryGetValue(id, out var result))
            {
                return null;
            }

            return $"{gameLanguageProvider.Language.PoeCdnBaseUrl}{result.Trim('/')}";
        }

        public string? GetId(ItemMetadata metadata)
        {
            var text = metadata.Name ?? metadata.Type ?? metadata.ApiType;
            if (text != null && Ids.TryGetValue(text, out var result))
            {
                return result;
            }

            return null;
        }
    }
}
