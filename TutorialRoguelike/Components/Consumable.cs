using TutorialRoguelike.Actions;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Components
{
    public abstract class Consumable : BaseComponent
    {
        // Try to return the action for this item
        public IAction GetAction(Actor consumer)
        {
            return new ItemAction(consumer, (Item) Parent);
        }

        // Invoke this item's ability.
        // 'action' is the context for thsi activation
        public abstract void Activate(ItemAction action);
    }
}
