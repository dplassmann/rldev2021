using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public interface IAction
    {
        public void Perform(Engine engine, Entity entity);
    }
}
