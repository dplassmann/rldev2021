using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.Manual
{
    public class TileFactory
    {
        public static Tile Floor => new Tile(true, true, new ColoredGlyph(Colors.Floor, Colors.Background, 0), new ColoredGlyph(Colors.Floor, Colors.Background, ' '));
        public static Tile Wall => new Tile(false, false, new ColoredGlyph(Colors.Wall, Colors.Background, 826), new ColoredGlyph(Colors.Wall, Colors.Background, ' '));
    }
}
