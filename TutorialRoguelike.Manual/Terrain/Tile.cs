using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.Manual.Terrain
{
    public class Tile
    {
        public bool IsWalkable { get; private set; }
        public bool IsTransparent { get; private set; }
        public ColoredGlyph Glyph { get; private set; }
        public ColoredGlyph DarkGlyph { get; private set; }

        public Tile(bool walkable, bool transparent, ColoredGlyph glyph)
        {
            IsWalkable = walkable;
            IsTransparent = transparent;
            Glyph = glyph;

            DarkGlyph = glyph.Clone();
            DarkGlyph.Foreground = DarkGlyph.Foreground.GetDarker();
            DarkGlyph.Background = DarkGlyph.Background.GetDarker();
        }

        public Tile Clone()
        {
            return new Tile(IsWalkable, IsTransparent, Glyph.Clone());
        }
    }
}
