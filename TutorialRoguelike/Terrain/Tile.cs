using SadConsole;
using SadRogue.Primitives;
using Newtonsoft.Json;
using SadConsole.SerializedTypes;

namespace TutorialRoguelike.Terrain
{
    public class Tile
    {
        public bool IsWalkable { get; private set; }
        public bool IsTransparent { get; private set; }

        [JsonConverter(typeof(ColoredGlyphJsonConverter))]
        public ColoredGlyph Glyph { get; private set; }

        [JsonConverter(typeof(ColoredGlyphJsonConverter))]
        public ColoredGlyph DarkGlyph { get; private set; }

        public TileType TileType { get; private set; }

        public Tile(bool walkable, bool transparent, ColoredGlyph glyph, TileType tileType)
        {
            IsWalkable = walkable;
            IsTransparent = transparent;
            Glyph = glyph;
            TileType = tileType;

            DarkGlyph = glyph.Clone();
            DarkGlyph.Foreground = DarkGlyph.Foreground.GetDarker();
            DarkGlyph.Background = DarkGlyph.Background.GetDarker();
        }

        public Tile Clone()
        {
            return new Tile(IsWalkable, IsTransparent, Glyph.Clone(), TileType);
        }
    }
}
