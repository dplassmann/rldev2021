using TutorialRoguelike.Actions;
using TutorialRoguelike.AI;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;
using TutorialRoguelike.EventHandlers;
using TutorialRoguelike.Exceptions;

namespace TutorialRoguelike.Components
{
    public class ConfusionConsumable : Consumable
    {
        private int NumberOfTurns { get; set; }
        public ConfusionConsumable(int numberOfTurns)
        {
            NumberOfTurns = numberOfTurns;
        }

        public override IActionOrEventHandler GetAction(Actor consumer)
        {
            Engine.MessageLog.Add("Select a target location", Colors.NeedsTarget);
            return new SingleTargetRangedAttackHandler(Engine, p => new ItemAction(consumer, (Item)Parent, p));
        }

        public override void Activate(ItemAction action)
        {
            var consumer = action.Entity;
            var target = action.TargetActor;

            if (!Engine.Map.Visible[action.Target])
            {
                throw new ImpossibleException("You cannot target an area you cannot see.");
            }
            if (target == null)
            {
                throw new ImpossibleException("You must select an enemy to target.");
            }
            if (target == consumer)
            {
                throw new ImpossibleException("You cannot confuse yourself!");
            }

            Engine.MessageLog.Add($"The eyes of the {target.Name} look vacant, as it starts to stumble around");
            target.AI = new ConfusedEnemy(target, NumberOfTurns);
            Consume();
        }
    }
}
