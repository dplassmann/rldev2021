using System.Collections.Generic;
using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Terrain
{
    public class TileFactory
    {
        public static Tile Floor = new Tile(true, true, new ColoredGlyph(Color.Transparent, Colors.Floor, 0), TileType.Floor);
        public static Tile Wall = new Tile(false, false, new ColoredGlyph(Colors.Wall, Color.Transparent, 826), TileType.Wall);
        public static Tile DownStairs = new Tile(true, true, new ColoredGlyph(Colors.Stairs, Color.Transparent, 291), TileType.DownStairs);

        private static Dictionary<TileType, Tile> Mapping = new Dictionary<TileType, Tile>
        {
            { TileType.Floor, Floor },
            { TileType.Wall, Wall },
            { TileType.DownStairs, DownStairs},
        };

        public static Tile Get(TileType type) => Mapping[type];
    }

    public enum TileType
    {
        Floor,
        Wall,
        DownStairs
    }
}
