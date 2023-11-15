using Sidekick.Common.Game.Items;

namespace Sidekick.Apis.Poe.Trade.Models
{
    public class TradeItem : Item
    {
        public TradeItem(
            ItemMetadata metadata,
            Header original,
            Properties properties,
            Influences influences,
            List<Socket> sockets,
            List<ModifierLine> modifierLines,
            List<PseudoModifier> pseudoModifiers,
            string text)
            : base(metadata, null, original, properties, influences, sockets, modifierLines, pseudoModifiers, text)
        {
        }

        public string? Id { get; set; }
        public TradePrice? Price { get; set; }

        public string? Image { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public List<LineContent> PropertyContents { get; set; } = new();
        public List<LineContent> AdditionalPropertyContents { get; set; } = new();
        public List<LineContent> RequirementContents { get; set; } = new();
    }
}
