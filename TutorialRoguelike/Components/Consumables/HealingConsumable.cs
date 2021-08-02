using TutorialRoguelike.Actions;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Exceptions;

namespace TutorialRoguelike.Components
{
    public class HealingConsumable : Consumable
    {
        public int Amount { get; private set; }

        public HealingConsumable(int amount)
        {
            Amount = amount;
        }

        public override void Activate(ItemAction action)
        {
            var consumer = action.Entity;
            var amountRecovered = consumer.Fighter.Heal(Amount);

            if (amountRecovered > 0)
            {
                Engine.MessageLog.Add($"You consume the {Parent.Name}, and recover {amountRecovered} HP!", Colors.HealthRecovered);
                Consume();
            }
            else
            {
                throw new ImpossibleException("Your health is already full.");
            }
        }
    }
}
