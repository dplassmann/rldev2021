using SadRogue.Primitives;

namespace TutorialRoguelike.Manual.Entities
{
    public class Player : Entity
    {
        public Player(Point position) : base(position, (char)25, Colors.Player) { }
    }
}
