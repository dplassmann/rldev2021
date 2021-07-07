using SadConsole;

namespace TutorialRoguelike.Manual
{
    public class Tile
    {
        public bool IsWalkable;
        public bool IsTransparent;
        public ColoredGlyph Glyph;
        public ColoredGlyph DarkGlyph;

        public Tile(bool walkable, bool transparent, ColoredGlyph glyph, ColoredGlyph darkGlyph)
        {
            IsWalkable = walkable;
            IsTransparent = transparent;
            Glyph = glyph;
            DarkGlyph = darkGlyph;
        }

        public Tile Clone()
        {
            return new Tile(IsWalkable, IsTransparent, Glyph.Clone(), DarkGlyph.Clone());
        }
    }
}
