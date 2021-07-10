using System;
using SadRogue.Primitives;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public abstract class ActionWithDirection : BaseAction
    {
        public Direction Direction { get; private set; }
        public ActionWithDirection(Actor entity, Direction direction) : base(entity)
        {
            Direction = direction;
        }

        public Point Destination => Entity.Position + Direction;

        public Entity BlockingEntity => Engine.Map.GetBlockingEntityAt(Destination);

        public Actor TargetActor => Engine.Map.GetActorAt(Destination);
    }
}
