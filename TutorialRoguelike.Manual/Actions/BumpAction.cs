using SadRogue.Primitives;
using TutorialRoguelike.Manual;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public class BumpAction : ActionWithDirection
    {
        public BumpAction(Entity entity, Point delta) : base(entity, delta) { }

        public override void Perform()
        {
            var newPosition = Entity.Position + Delta;
            if (Engine.Map.GetBlockingEntityAt(newPosition) != null)
            {
                new MeleeAction(Entity, Delta).Perform();
            } else
            {
                new MovementAction(Entity, Delta).Perform();
            }
        }
    }
}
