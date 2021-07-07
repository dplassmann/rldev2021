using System;
using System.Collections.Generic;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace TutorialRoguelike.GoRogue.Terrain
{
    public class TileFactory
    {
        public static RogueLikeCell Floor(Point position) => new RogueLikeCell(position, Colors.Floor, Colors.Background, 0, 0, true, true);
        public static RogueLikeCell Wall(Point position) => new RogueLikeCell(position, Colors.Wall, Colors.Background, 826, 0, false, false);

        public static RogueLikeCell Create(TileTypes type, Point position) => Generators[type](position);

        private static Dictionary<TileTypes, Func<Point, RogueLikeCell>> Generators = new Dictionary<TileTypes, Func<Point, RogueLikeCell>>
        {
            { TileTypes.Floor, Floor },
            { TileTypes.Wall, Wall }
        };
    }
}
