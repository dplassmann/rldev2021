using SadRogue.Primitives;

namespace TutorialRoguelike.Actions
{
    class MovementAction : IAction
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
