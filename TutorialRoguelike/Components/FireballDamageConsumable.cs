using System.Linq;
using TutorialRoguelike.Actions;
using TutorialRoguelike.AI;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;
using TutorialRoguelike.EventHandlers;

namespace TutorialRoguelike.Components
{
    public class FireballDamageConsumable : Consumable
    {
        private int Damage { get; set; }
        private int Radius { get; set; }
        public FireballDamageConsumable(int damage, int radius)
        {
            Damage = damage;
            Radius = radius;
        }

        public override IAction GetAction(Actor consumer)
        {
            Engine.MessageLog.Add("Select a target location", Colors.NeedsTarget);
            Engine.EventHandler.TransitionTo(new AreaRangedAttackHandler(Engine, Radius, p => new ItemAction(consumer, (Item)Parent, p)));
            return null;
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
