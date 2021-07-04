using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
