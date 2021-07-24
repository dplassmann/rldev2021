using SadConsole;
using TutorialRoguelike.Entities;
using TutorialRoguelike.Exceptions;

namespace TutorialRoguelike.Actions
{
    public class EscapeAction : BaseAction
    {
        public EscapeAction(Actor entity) : base(entity)
        {
        }

        public override void Perform()
        {
            throw new SystemExit();
        }
    }
}
