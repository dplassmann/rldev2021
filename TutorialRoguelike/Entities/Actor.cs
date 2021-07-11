using SadConsole;
using TutorialRoguelike.Components;
using TutorialRoguelike.Components.AI;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Entities
{
    public class Actor : Entity
    {
        public Fighter Fighter { get; private set; }
        public BaseAI AI { get; set; }

        public Actor(ColoredGlyph appearance, string name, Fighter fighter, GameMap map = null) : base(appearance, name, true, RenderOrder.Actor, map)
        {
            Fighter = fighter;
            Fighter.Entity = this;
        }

        public bool IsAlive => AI != null;
    }

    public class Actor<T> : Actor where T : BaseAI
    {

        public Actor(ColoredGlyph appearance, string name, Fighter fighter, GameMap map = null) : base(appearance, name, fighter, map)
        {
            var aiConstructor = typeof(T).GetConstructor(new[] { typeof(Actor) });
            AI = (T)aiConstructor.Invoke(new object[] { this });
        }
    }
}
