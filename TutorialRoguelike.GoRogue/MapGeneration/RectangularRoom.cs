using System.Collections.Generic;
using SadRogue.Primitives;

namespace TutorialRoguelike.GoRogue.MapGeneration
{
    public class RectangularRoom
    {
        public Rectangle Perimeter;

        public RectangularRoom(Rectangle perimeter)
        {
            Perimeter = perimeter;
        }
        public Point Center => Perimeter.Center;

        public IEnumerable<Point> InteriorPositions() => Perimeter.Translate((1, 1)).WithSize(Perimeter.Size - 2).Positions();
    }
}
