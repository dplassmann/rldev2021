using SadRogue.Primitives;
using TutorialRoguelike.Manual;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public class BumpAction : ActionWithDirection
    {
        public BumpAction(Actor entity, Direction direction) : base(entity, direction) { }

        public override void Perform()
        {
            if (TargetActor != null)
            {
                new MeleeAction(Entity, Direction).Perform();
            } else
            {
                new MovementAction(Entity, Direction).Perform();
            }
        }
    }
}
