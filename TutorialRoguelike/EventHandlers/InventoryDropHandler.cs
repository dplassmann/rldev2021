using TutorialRoguelike.Actions;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.EventHandlers
{
    public class InventoryDropHandler : InventoryEventHandler
    {
        public InventoryDropHandler(Engine engine) : base(engine, "Select an item to drop")
        {
        }

        protected override IActionOrEventHandler ItemSelected(Item item)
        {
            return new DropAction(Engine.Player, item);
        }
    }
}
