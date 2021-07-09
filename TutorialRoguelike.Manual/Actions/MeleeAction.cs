using SadRogue.Primitives;
using TutorialRoguelike.Manual;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public class MeleeAction : ActionWithDirection
    {
        public MeleeAction(Point delta) : base(delta) { }

        public override void Perform(Engine engine, Entity entity)
        {
            var newPosition = entity.Position + Delta;
            var target = engine.Map.GetBlockingEntityAt(newPosition);
            if (target != null)
            {
                System.Console.WriteLine($"You kick the {target.Name}, much to its annoyance!");
            }
        }
    }
}
