using SadRogue.Primitives;
using TutorialRoguelike.Exceptions;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Actions
{
    public class MovementAction : ActionWithDirection
    {
        public MovementAction(Actor entity, Direction direction) : base(entity, direction) { }

        public override void Perform()
        {
            if (Engine.Map.InBounds(Destination)
                && Engine.Map[Destination].IsWalkable
                && Engine.Map.GetBlockingEntityAt(Destination) == null)
            {
                Entity.Position += Direction;
            } else
            {
                throw new ImpossibleException("That way is blocked.");
            }
        }
    }
}
