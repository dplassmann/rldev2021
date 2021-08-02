using System.Linq;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;
using TutorialRoguelike.EventHandlers;
using TutorialRoguelike.Exceptions;

namespace TutorialRoguelike.Components
{
    public class FireballDamageConsumable : Consumable
    {
        public int Damage { get; set; }
        public int Radius { get; set; }
        public FireballDamageConsumable(int damage, int radius)
        {
            Damage = damage;
            Radius = radius;
        }

        public override IActionOrEventHandler GetAction(Actor consumer)
        {
            Engine.MessageLog.Add("Select a target location", Colors.NeedsTarget);
            return new AreaRangedAttackHandler(Engine, Radius, p => new ItemAction(consumer, (Item)Parent, p));
        }

        public override void Activate(ItemAction action)
        {
            if (!Engine.Map.Visible[action.Target])
            {
                throw new ImpossibleException("You cannot target an area you cannot see.");
            }

            var targetsHit = false;
            foreach (var actor in Engine.Map.Actors.Where(a => a.Distance(action.Target) <= Radius))
            {
                Engine.MessageLog.Add($"The {actor.Name} is engulfed in a fiery explosion, taking {Damage} damage!");
                actor.Fighter.TakeDamage(Damage);
                targetsHit = true;
            }
            if (!targetsHit)
            {
                throw new ImpossibleException("There are no targets in the radius.");
            }

            Consume();
        }
    }
}
