using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.Manual
{
    public class TileFactory
    {
        public static Tile Floor => new Tile(true, true, new ColoredGlyph(Color.White, Colors.Floor, ' '), new ColoredGlyph(Color.White, Colors.Floor, ' '));
        public static Tile Wall => new Tile(false, false, new ColoredGlyph(Color.White, Colors.Wall, ' '), new ColoredGlyph(Color.White, Colors.Wall, ' '));
    }
}
