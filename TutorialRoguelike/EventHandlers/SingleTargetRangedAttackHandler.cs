using System;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;

namespace TutorialRoguelike.EventHandlers
{
    public class SingleTargetRangedAttackHandler : SetIndexHandler
    {
        private Func<Point, IAction> Callback { get; set; }
        public SingleTargetRangedAttackHandler(Engine engine, Func<Point, IAction> callback) : base(engine)
        {
            Callback = callback;
        }

        public override IActionOrEventHandler IndexSelected(Point position)
        {
            return Callback.Invoke(position);
        }
    }
}
