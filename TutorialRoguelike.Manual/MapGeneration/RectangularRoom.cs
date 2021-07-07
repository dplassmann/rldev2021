using System.Collections.Generic;
using SadRogue.Primitives;

namespace TutorialRoguelike.Manual.MapGeneration
{
    public class RectangularRoom
    {
        Point MinExtent;
        Point MaxExtent;

        public RectangularRoom(int x, int y, int width, int height)
        {
            MinExtent = (x, y);
            MaxExtent = (x + width, y + height);
        }
        public RectangularRoom(Point minExtent, int width, int height)
        {
            MinExtent = minExtent;
            MaxExtent = minExtent + (width, height);
        }

        public Point Center => (MinExtent + MaxExtent) / 2;

        //Returns the inner area of the room
        //Python implementation returned Tuple[slice,slice], allowing it to be directly used as a 2D array index
        public IEnumerable<Point> Inner
        {
            get {
                for (int i = MinExtent.X + 1; i < MaxExtent.X; i++)
                {
                    for (int j = MinExtent.Y + 1; j < MaxExtent.Y; j++)
                    {
                        yield return (i, j);
                    }
                }
            }
        }

        public bool Intersects(RectangularRoom other)
        {
            return MinExtent.X <= other.MaxExtent.X
                && MinExtent.Y <= other.MaxExtent.Y
                && MaxExtent.X >= other.MinExtent.X
                && MaxExtent.Y >= other.MinExtent.Y;
        }
    }
}
