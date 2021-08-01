using Newtonsoft.Json;
using SadConsole;
using TutorialRoguelike.AI;
using TutorialRoguelike.Components;
using TutorialRoguelike.Constants;
using TutorialRoguelike.World;

namespace TutorialRoguelike.Entities
{
    public class Actor : Entity
    {
        public Fighter Fighter { get; private set; }
        public Inventory Inventory { get; private set; }
        public BaseAI AI { get; set; }

        [JsonConstructor]
        public Actor(ColoredGlyph appearance, string name, Fighter fighter, Inventory inventory, GameMap map = null) : base(appearance, name, true, RenderOrder.Actor, map)
        {
            Fighter = fighter;
            Fighter.Parent = this;

            Inventory = inventory;
            Inventory.Parent = this;
        }

        public Actor(Actor serializableActor, GameMap map = null) : base(serializableActor, map)
        {
            Fighter = serializableActor.Fighter;
            Fighter.Parent = this;

            Inventory = new Inventory(serializableActor.Inventory);
            Inventory.Parent = this;

            AI = serializableActor.AI;
            if (AI != null)
                AI.Entity = this;
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
