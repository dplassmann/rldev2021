﻿using SadRogue.Primitives;
using TutorialRoguelike.Manual;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public class MovementAction : ActionWithDirection
    {
        public MovementAction(Point delta) : base(delta) { }

        public override void Perform(Engine engine, Entity entity)
        {
            var newPosition = entity.Position + Delta;
            if (engine.Map.InBounds(newPosition)
                && engine.Map[newPosition].IsWalkable
                && engine.Map.GetBlockingEntityAt(newPosition) == null)
            {
                entity.Move(Delta);
            }
        }
    }
}
