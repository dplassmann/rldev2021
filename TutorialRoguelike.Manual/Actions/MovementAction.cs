using SadRogue.Primitives;
using TutorialRoguelike.Manual;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public class MovementAction : ActionWithDirection
    {
        public MovementAction(Entity entity, Point delta) : base(entity, delta) { }

        public override void Perform()
        {
            var newPosition = Entity.Position + Delta;
            if (Engine.Map.InBounds(newPosition)
                && Engine.Map[newPosition].IsWalkable
                && Engine.Map.GetBlockingEntityAt(newPosition) == null)
            {
                Entity.Move(Delta);
            }
        }
    }
}
