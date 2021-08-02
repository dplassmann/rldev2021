using Newtonsoft.Json;
using SadConsole;
using TutorialRoguelike.Components;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Entities
{
    public class Item : Entity
    {
        public Consumable Consumable { get; private set; }
        public Equippable Equippable { get; private set; }

        [JsonConstructor]
        public Item(ColoredGlyph appearance, string name, Consumable consumable = null, Equippable equippable = null) : base(appearance, name, false, RenderOrder.Item)
        {
            Consumable = consumable;
            if (Consumable != null)
                Consumable.Parent = this;

            Equippable = equippable;
            if (Equippable != null)
                Equippable.Parent = this;
        }

        public Item(Item serializableItem) : base(serializableItem.Appearance, serializableItem.Name, false, RenderOrder.Item)
        {
            Consumable = serializableItem.Consumable;
            if (Consumable != null)
                Consumable.Parent = this;

            Equippable = serializableItem.Equippable;
            if (Equippable != null)
                Equippable.Parent = this;
        }
    }
}
