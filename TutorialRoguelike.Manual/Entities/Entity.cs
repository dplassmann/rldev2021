using System;
using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.Manual.Entities
{
    public class Entity
    {
        public GameMap Map;
        public Point Position;
        public ColoredGlyph Appearance;
        public string Name;
        public bool BlocksMovement;

        public Entity(ColoredGlyph glyph, string name, bool blocksMovement, GameMap map = null)
        {
            Position = (0, 0);
            Appearance = glyph;
            Name = name;
            BlocksMovement = blocksMovement;
            if (map != null)
            {
                Map = map;
                map.Entities.Add(this);
            }
        }

        public void Move(Point delta)
        {
            Position += delta;
        }

        public void Place(Point position, GameMap map = null)
        {
            Position = position;
            if (map != null)
            {
                if (Map != null)
                    Map.Entities.Remove(this);
                Map = map;
                Map.Entities.Add(this);
            }
        }
    }
}
