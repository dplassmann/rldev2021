using System.Collections.Generic;
using SadRogue.Primitives;

namespace TutorialRoguelike.GoRogue.MapGeneration
{
    public class RectangularRoom
    {
        public Rectangle Perimeter { get; private set; }
        public Rectangle Interior { get; private set; }

        public RectangularRoom(Rectangle perimeter)
        {
            Perimeter = perimeter;
            Interior = Perimeter.Translate((1, 1)).WithSize(Perimeter.Size - 2);
        }
        public Point Center => Perimeter.Center;

        public IEnumerable<Point> InteriorPositions() => Interior.Positions();

    }
}
