using SadRogue.Primitives;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Actions
{
    public class ItemAction : BaseAction
    {
        public Item Item { get; private set; }
        public Point Target { get; private set; }

        public ItemAction(Actor entity, Item item, Point? target = null) : base(entity)
        {
            Item = item;
            Target = target.HasValue ? target.Value : entity.Position;
        }

        public Actor TargetActor => Engine.Map.GetActorAt(Target);

        //Invoke the item's ability, this action will be given to provide context
        public override void Perform()
        {
            Item.Consumable.Activate(this);
        }
    }
}
