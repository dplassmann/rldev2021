using TutorialRoguelike.Actions;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.EventHandlers
{
    public class InventoryActivateHandler : InventoryEventHandler
    {
        public InventoryActivateHandler(Engine engine) : base(engine)
        {
        }

        protected override string Title => "Select an item to use";

        protected override IActionOrEventHandler ItemSelected(Item item)
        {
            return item.Consumable.GetAction(Engine.Player);
        }
    }
}
