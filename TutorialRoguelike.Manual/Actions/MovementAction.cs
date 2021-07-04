using SadRogue.Primitives;

namespace TutorialRoguelike.Actions
{
    public class MovementAction : IAction
    {
        public Point Delta { get; private set; }
        public MovementAction(int x, int y)
        {
            Delta = (x, y);
        }
        public MovementAction(Point delta)
        {
            Delta = delta;
        }
    }
}
