using SadRogue.Primitives;

namespace TutorialRoguelike.Manual.Entities
{
    public class Player : Entity
    {
        public Player(Point position) : base(position, '@', Colors.Player) { }
    }
}
