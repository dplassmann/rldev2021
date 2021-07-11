using System;
using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Entities
{
    public class Entity
    {
        public GameMap Map;
        public Point Position;
        public ColoredGlyph Appearance;
        public string Name;
        public bool BlocksMovement;
        public RenderOrder RenderOrder;

        public Entity(ColoredGlyph appearance, string name, bool blocksMovement, RenderOrder renderOrder, GameMap map = null)
        {
            Position = (0, 0);
            Appearance = appearance;
            Name = name;
            BlocksMovement = blocksMovement;
            RenderOrder = renderOrder;
            if (map != null)
            {
                Map = map;
                map.Entities.Add(this);
            }
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
