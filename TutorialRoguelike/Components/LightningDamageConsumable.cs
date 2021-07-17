using System.Linq;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Components
{
    public class LightningDamageConsumable : Consumable
    {
        public LightningDamageConsumable(int damage, double maximumRange)
        {
            Damage = damage;
            MaximumRange = maximumRange;
        }

        public int Damage { get; private set; }
        public double MaximumRange { get; private set; }
        public override void Activate(ItemAction action)
        {
            var consumer = action.Entity;
            Actor target = null;
            var closestDistance = MaximumRange + 1;

            foreach (var actor in Engine.Map.Actors.Where(a => a != consumer && Parent.Map.Visible[a.Position]))
            {
                var distance = consumer.Distance(actor.Position);
                if (distance < closestDistance)
                {
                    target = actor;
                    closestDistance = distance;
                }
            }

            if (target != null)
            {
                Engine.MessageLog.Add($"A lightning bolt strikes the {target.Name} with a loud thunder, for {Damage} damage!");
                target.Fighter.TakeDamage(Damage);
                Consume();
            }
            else
            {
                throw new ImpossibleException("No enemy is close enough to strike.");
            }
        }
    }
}
