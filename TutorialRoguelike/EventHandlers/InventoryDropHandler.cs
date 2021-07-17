using TutorialRoguelike.Actions;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.EventHandlers
{
    public class InventoryDropHandler : InventoryEventHandler
    {
        public InventoryDropHandler(Engine engine) : base(engine)
        {
        }

        protected override string Title => "Select an item to drop";

        protected override IAction ItemSelected(Item item)
        {
            return new DropAction(Engine.Player, item);
        }
    }
}
