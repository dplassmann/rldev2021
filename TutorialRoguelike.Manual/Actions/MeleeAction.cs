using SadRogue.Primitives;
using TutorialRoguelike.Manual;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public class MeleeAction : ActionWithDirection
    {
        public MeleeAction(Actor entity, Direction direction) : base(entity, direction) { }

        public override void Perform()
        {
            if (TargetActor == null)
            {
                return;
            }

            var damage = Entity.Fighter.Power - TargetActor.Fighter.Defense;

            var attackDescription = $"{Entity.Name} attacks {TargetActor.Name}";
            if (damage > 0)
            {
                System.Console.WriteLine($"{attackDescription} for {damage} hit points.");
                TargetActor.Fighter.Hp -= damage;
            }
            else
            {
                System.Console.WriteLine($"{attackDescription} but does no damage.");
            }
        }
    }
}
