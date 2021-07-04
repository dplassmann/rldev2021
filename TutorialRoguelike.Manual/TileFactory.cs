using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.Manual
{
    public class TileFactory
    {
        public static Tile Floor { get { return new Tile(true, true, new ColoredGlyph(Color.White, Color.AnsiBlueBright, ' '), new ColoredGlyph(Color.White, Color.AnsiBlue, ' ')); } }
        public static Tile Wall { get { return new Tile(false, false, new ColoredGlyph(Color.White, Color.AnsiGreenBright, ' '), new ColoredGlyph(Color.White, Color.AnsiGreen, ' ')); } }
    }
}
