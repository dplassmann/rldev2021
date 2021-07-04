﻿using SadRogue.Primitives;
using TutorialRoguelike.Manual;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Actions
{
    public class MovementAction : IAction
    {
        public Point Delta { get; private set; }
        public MovementAction(int x, int y)
        {
            Delta = (x, y);
        }
        public MovementAction(Point delta)
        {
            Delta = delta;
        }

        public void Perform(Engine engine, Entity entity)
        {
            var newPosition = entity.Position + Delta;
            if (engine.Map.TileAt(newPosition).IsWalkable)
                entity.Move(Delta);
        }
    }
}
