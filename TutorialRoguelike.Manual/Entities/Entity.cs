using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.Manual.Entities
{
    public class Entity
    {
        public Point Position;
        public ColoredGlyph Glyph;

        public Entity(Point position, ColoredGlyph glyph)
        {
            Position = position;
            Glyph = glyph;
        }
        public Entity(Point position, char glyph, Color color)
        {
            Position = position;
            Glyph = new ColoredGlyph(color, Color.Transparent, glyph);
        }

        public void Move(Point delta)
        {
            Position += delta;
        }
    }
}
