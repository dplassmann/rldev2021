using SadConsole;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Actions
{
    public class WaitAction : BaseAction
    {
        public WaitAction(Actor entity) : base(entity)
        {
        }

        public override void Perform()
        {
            return;
        }
    }
}
