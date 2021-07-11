﻿using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Terrain
{
    public class TileFactory
    {
        public static Tile Floor = new Tile(true, true, new ColoredGlyph(Color.Transparent, Colors.Floor, 0));
        public static Tile Wall = new Tile(false, false, new ColoredGlyph(Colors.Wall, Color.Transparent, 826));
    }
}
