using SadConsole;
using TutorialRoguelike.Components;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Entities
{
    public class Item : Entity
    {
        public Consumable Consumable { get; private set; }

        public Item(ColoredGlyph appearance, string name, Consumable consumable) : base(appearance, name, false, RenderOrder.Item)
        {
            Consumable = consumable;
            Consumable.Parent = this;
        }
    }
}
