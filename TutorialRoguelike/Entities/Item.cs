using Newtonsoft.Json;
using SadConsole;
using TutorialRoguelike.Components;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Entities
{
    public class Item : Entity
    {
        public Consumable Consumable { get; private set; }

        [JsonConstructor]
        public Item(ColoredGlyph appearance, string name, Consumable consumable) : base(appearance, name, false, RenderOrder.Item)
        {
            Consumable = consumable;
            Consumable.Parent = this;
        }

        public Item(Item serializableItem) : base(serializableItem.Appearance, serializableItem.Name, false, RenderOrder.Item)
        {
            Consumable = serializableItem.Consumable;
            Consumable.Parent = this;
        }
    }
}
