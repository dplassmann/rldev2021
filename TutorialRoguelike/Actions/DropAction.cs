using SadRogue.Primitives;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Actions
{
    public class DropAction : ItemAction
    {
        public DropAction(Actor entity, Item item, Point? target = null) : base(entity, item, target)
        {
        }

        public override void Perform()
        {
            if (Entity.Equipment.IsItemEquipped(Item))
                Entity.Equipment.ToggleEquipment(Item);

            Entity.Inventory.Drop(Item);
        }
    }
}
