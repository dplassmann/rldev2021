using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Actions
{
    public interface IAction : IActionOrEventHandler
    {
        public Actor Entity { get; }

        public void Perform();
    }
}
