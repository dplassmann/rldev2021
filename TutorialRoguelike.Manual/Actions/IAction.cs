using TutorialRoguelike.Manual;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Actions
{
    public interface IAction
    {
        public void Perform(Engine engine, Entity entity);
    }
}
