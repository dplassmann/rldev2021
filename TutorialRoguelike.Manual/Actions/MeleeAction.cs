using SadRogue.Primitives;
using TutorialRoguelike.Manual;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public class MeleeAction : ActionWithDirection
    {
        public MeleeAction(Entity entity, Point delta) : base(entity, delta) { }

        public override void Perform()
        {
            var newPosition = Entity.Position + Delta;
            var target = Engine.Map.GetBlockingEntityAt(newPosition);
            if (target != null)
            {
                System.Console.WriteLine($"You kick the {target.Name}, much to its annoyance!");
            }
        }
    }
}
