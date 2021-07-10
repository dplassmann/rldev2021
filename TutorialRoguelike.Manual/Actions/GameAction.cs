using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public abstract class GameAction
    {
        public Entity Entity;

        public Engine Engine => Entity.Map.Engine;

        protected GameAction(Entity entity)
        {
            Entity = entity;
        }

        public abstract void Perform();
    }
}
