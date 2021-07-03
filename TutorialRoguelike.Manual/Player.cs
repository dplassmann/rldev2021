using SadRogue.Primitives;

namespace TutorialRoguelike.Manual
{
    public class Player
    {
        public Point Position { get; set; }

        public Player(Point position)
        {
            Position = position;
        }
        public Player(int x, int y)
        {
            Position = (x, y);
        }
    }
}
