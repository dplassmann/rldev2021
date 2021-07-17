using SadRogue.Primitives;
using TutorialRoguelike;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Actions
{
    public class MeleeAction : ActionWithDirection
    {
        public MeleeAction(Actor entity, Direction direction) : base(entity, direction) { }

        public override void Perform()
        {
            if (TargetActor == null)
            {
                throw new ImpossibleException("Nothing to Attack");
            }

            var damage = Entity.Fighter.Power - TargetActor.Fighter.Defense;

            var attackDescription = $"{Entity.Name} attacks {TargetActor.Name}";
            var attackColor = Entity == Engine.Player ? Colors.PlayerAttack : Colors.EnemyAttack;
            if (damage > 0)
            {
                Engine.MessageLog.Add($"{attackDescription} for {damage} hit points.", attackColor);
                TargetActor.Fighter.Hp -= damage;
            }
            else
            {
                Engine.MessageLog.Add($"{attackDescription} but does no damage.", attackColor);
            }
        }
    }
}
