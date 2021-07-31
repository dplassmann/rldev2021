using System;
using Newtonsoft.Json;
using SadConsole;
using SadConsole.SerializedTypes;
using SadRogue.Primitives;
using TutorialRoguelike.Components;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Entities
{
    public class Entity
    {
        [JsonIgnore]
        public EntityContainer Parent { get; set; }
        public Point Position { get; set; }

        [JsonConverter(typeof(ColoredGlyphJsonConverter))]
        public ColoredGlyph Appearance { get; set; }
        public string Name { get; set; }
        public bool BlocksMovement { get; set; }
        public RenderOrder RenderOrder { get; set; }

        [JsonIgnore]
        public GameMap Map => Parent.Map;

        [JsonConstructor]
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

        public Entity(Entity serializableEntity, GameMap map)
        {
            Position = serializableEntity.Position;
            Appearance = serializableEntity.Appearance;
            Name = serializableEntity.Name;
            BlocksMovement = serializableEntity.BlocksMovement;
            RenderOrder = serializableEntity.RenderOrder;
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

        // Returns the distance betwene the current entity and the given point.
        public double Distance(Point position)
        {
            return SadRogue.Primitives.Distance.Chebyshev.Calculate(Position - position);
        }
    }
}
