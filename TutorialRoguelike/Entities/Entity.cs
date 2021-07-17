﻿using System;
using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Entities
{
    public class Entity
    {
        public EntityContainer Parent { get; set; }
        public Point Position { get; set; }
        public ColoredGlyph Appearance { get; set; }
        public string Name { get; set; }
        public bool BlocksMovement { get; set; }
        public RenderOrder RenderOrder { get; set; }
        public GameMap Map => ((GameMap) Parent).Map;

        public Entity(ColoredGlyph appearance, string name, bool blocksMovement, RenderOrder renderOrder, GameMap map = null)
        {
            Position = (0, 0);
            Appearance = appearance;
            Name = name;
            BlocksMovement = blocksMovement;
            RenderOrder = renderOrder;
            if (map != null)
            {
                Parent = map;
                map.Entities.Add(this);
            }
        }

        public void Place(Point position, GameMap map = null)
        {
            Position = position;
            if (map != null)
            {
                if (Parent != null)
                    Parent.Entities.Remove(this);
                Parent = map;
                Parent.Entities.Add(this);
            }
        }
    }
}
