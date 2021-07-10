using System;
using SadRogue.Primitives;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public abstract class ActionWithDirection : GameAction
    {
        public Point Delta { get; private set; }
        public ActionWithDirection(Entity entity, Point delta) : base(entity)
        {
            Delta = delta;
        }
    }
}
