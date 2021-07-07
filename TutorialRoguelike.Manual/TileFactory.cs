using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.Manual
{
    public class TileFactory
    {
        public static Tile Floor => new Tile(true, true, new ColoredGlyph(Color.White, Colors.Floor, 0), new ColoredGlyph(Color.White, Colors.Floor, ' '));
        public static Tile Wall => new Tile(false, false, new ColoredGlyph(Color.White, Colors.Wall, 826), new ColoredGlyph(Color.White, Colors.Wall, ' '));
    }
}
