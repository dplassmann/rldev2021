using SadConsole;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
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
