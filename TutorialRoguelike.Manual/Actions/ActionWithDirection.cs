using System;
using SadRogue.Primitives;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public abstract class ActionWithDirection : IAction
    {
        public Point Delta { get; private set; }
        public ActionWithDirection(Point delta)
        {
            Delta = delta;
        }

        public abstract void Perform(Engine engine, Entity entity);
    }
}
