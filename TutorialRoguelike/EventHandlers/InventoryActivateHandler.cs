using TutorialRoguelike.Actions;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.EventHandlers
{
    public class InventoryActivateHandler : InventoryEventHandler
    {
        public InventoryActivateHandler(Engine engine) : base(engine, "Select an item to use")
        {
        }

        protected override IActionOrEventHandler ItemSelected(Item item)
        {
            if (item.Consumable != null)
                return item.Consumable.GetAction(Engine.Player);
            else if (item.Equippable != null)
                return new EquipAction(Engine.Player, item);
            else
                return null;
        }
    }
}
