using System;
using System.Linq;
using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.EventHandlers
{
    public class AreaRangedAttackHandler : SetIndexHandler
    {
        private int Radius { get; set; }
        private Func<Point, IAction> Callback { get; set; }

        public AreaRangedAttackHandler(Engine engine, int radius, Func<Point, IAction> callback) : base(engine)
        {
            Radius = radius;
            Callback = callback;
        }

        public override void Render()
        {
            base.Render();
            var target = Engine.MouseLocation;

            foreach (var p in new Rectangle((target.X, target.Y), Radius, Radius).Positions().Where(p => p != target))
                Engine.Console.AddDecorator(p.X, p.Y, 1, new[] { new CellDecorator(Colors.TargetingAreaOverlay, 817, Mirror.None) });
        }


        public override IAction IndexSelected(Point position)
        {
            return Callback(position);
        }
    }
}
