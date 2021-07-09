using SadRogue.Primitives;
using TutorialRoguelike.Manual;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public class BumpAction : ActionWithDirection
    {
        public BumpAction(Point delta) : base(delta) { }

        public override void Perform(Engine engine, Entity entity)
        {
            var newPosition = entity.Position + Delta;
            if (engine.Map.GetBlockingEntityAt(newPosition) != null)
            {
                new MeleeAction(Delta).Perform(engine, entity);
            } else
            {
                new MovementAction(Delta).Perform(engine, entity);
            }
        }
    }
}
