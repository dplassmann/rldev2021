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
            Entity.Inventory.Drop(Item);
        }
    }
}
