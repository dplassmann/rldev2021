using SadRogue.Primitives;
using TutorialRoguelike.Actions;

namespace TutorialRoguelike.EventHandlers
{
    public class LookHandler : SetIndexHandler
    {
        public LookHandler(Engine engine) : base(engine)
        {
        }

        public override IAction IndexSelected(Point position)
        {
            Engine.EventHandler = new MainGameEventHandler(Engine);
            return null;
        }
    }
}
