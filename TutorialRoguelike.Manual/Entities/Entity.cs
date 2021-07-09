using System;
using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.Manual.Entities
{
    public class Entity
    {
        public Point Position;
        public ColoredGlyph Appearance;
        public string Name;
        public bool BlocksMovement;

        public Entity(Point position, ColoredGlyph glyph, string name, bool blocksMovement)
        {
            Position = position;
            Appearance = glyph;
            Name = name;
            BlocksMovement = blocksMovement;
        }

        public Entity(ColoredGlyph glyph, string name, bool blocksMovement)
        {
            Position = (0, 0);
            Appearance = glyph;
            Name = name;
            BlocksMovement = blocksMovement;
        }

        public void Move(Point delta)
        {
            Position += delta;
        }

        public Entity Spawn(GameMap map, Point position)
        {
            Position = position;
            map.Entities.Add(this);
            return this;
        }
    }
}
