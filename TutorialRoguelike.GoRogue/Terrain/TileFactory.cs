using System;
using System.Collections.Generic;
using SadRogue.Integration;
using SadRogue.Integration.FieldOfView.Memory;
using SadRogue.Primitives;

namespace TutorialRoguelike.GoRogue.Terrain
{
    public class TileFactory
    {
        public static MemoryAwareRogueLikeCell Floor(Point position) => new MemoryAwareRogueLikeCell(position, Color.Transparent, Colors.Floor, 0, 0, true, true);
        public static MemoryAwareRogueLikeCell Wall(Point position) => new MemoryAwareRogueLikeCell(position, Colors.Wall, Color.Transparent, 826, 0, false, false);

        public static MemoryAwareRogueLikeCell Create(TileTypes type, Point position) => Generators[type](position);

        private static Dictionary<TileTypes, Func<Point, MemoryAwareRogueLikeCell>> Generators = new Dictionary<TileTypes, Func<Point, MemoryAwareRogueLikeCell>>
        {
            { TileTypes.Floor, Floor },
            { TileTypes.Wall, Wall }
        };
    }
}
