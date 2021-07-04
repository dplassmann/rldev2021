using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.Manual.Entities
{
    public class Entity
    {
        public Point Position;
        public ColoredGlyph Appearance;

        public Entity(Point position, ColoredGlyph glyph)
        {
            Position = position;
            Appearance = glyph;
        }
        public Entity(Point position, char glyph, Color color)
        {
            Position = position;
            Appearance = new ColoredGlyph(color, Color.Transparent, glyph);
        }

        public void Move(Point delta)
        {
            Position += delta;
        }
    }
}
