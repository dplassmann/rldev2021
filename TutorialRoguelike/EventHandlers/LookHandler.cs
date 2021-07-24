using SadRogue.Primitives;
using TutorialRoguelike.Actions;

namespace TutorialRoguelike.EventHandlers
{
    public class LookHandler : SetIndexHandler
    {
        public LookHandler(Engine engine) : base(engine)
        {
        }

        public override IActionOrEventHandler IndexSelected(Point position)
        {
            return new MainGameEventHandler(Engine);
        }
    }
}
