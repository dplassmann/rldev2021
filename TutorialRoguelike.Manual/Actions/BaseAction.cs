using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public abstract class BaseAction : IAction
    {
        private Actor _entity;

        public Engine Engine => Entity.Map.Engine;

        public Actor Entity { get => _entity; }

        protected BaseAction(Actor entity)
        {
            _entity = entity;
        }

        public abstract void Perform();
    }
}
