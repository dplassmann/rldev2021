using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.Manual.Terrain
{
    public class Tile
    {
        public bool IsWalkable;
        public bool IsTransparent;
        public ColoredGlyph Glyph;
        public ColoredGlyph DarkGlyph;

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
