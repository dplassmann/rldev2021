using Microsoft.Xna.Framework;

namespace TutorialRoguelike.Actions
{
    class MovementAction : IAction
    {
        public Point Delta { get; private set; }
        public MovementAction(int x, int y)
        {
            Delta = new Point(x, y);
        }
    }
}
