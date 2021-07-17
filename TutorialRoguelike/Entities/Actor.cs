using SadConsole;
using TutorialRoguelike.AI;
using TutorialRoguelike.Components;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Entities
{
    public class Actor : Entity
    {
        public Fighter Fighter { get; private set; }
        public Inventory Inventory { get; private set; }
        public BaseAI AI { get; set; }

        public Actor(ColoredGlyph appearance, string name, Fighter fighter, Inventory inventory, GameMap map = null) : base(appearance, name, true, RenderOrder.Actor, map)
        {
            Fighter = fighter;
            Fighter.Parent = this;

            Inventory = inventory;
            Inventory.Parent = this;
        }

        public bool IsAlive => AI != null;
    }

    public class Actor<T> : Actor where T : BaseAI
    {

        public Actor(ColoredGlyph appearance, string name, Fighter fighter, Inventory inventory, GameMap map = null) : base(appearance, name, fighter, inventory, map)
        {
            var aiConstructor = typeof(T).GetConstructor(new[] { typeof(Actor) });
            AI = (T)aiConstructor.Invoke(new object[] { this });
        }
    }
}
