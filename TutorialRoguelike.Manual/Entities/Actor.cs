using SadConsole;
using TutorialRoguelike.Manual.Components;
using TutorialRoguelike.Manual.Components.AI;

namespace TutorialRoguelike.Manual.Entities
{
    public class Actor : Entity
    {
        public Fighter Fighter { get; private set; }
        public BaseAI AI { get; set; }

        public Actor(ColoredGlyph appearance, string name, Fighter fighter, GameMap map = null) : base(appearance, name, true, map)
        {
            Fighter = fighter;
            Fighter.Entity = this;
        }

        public bool IsAlive => AI != null; //TODO Is this really doing anything
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
