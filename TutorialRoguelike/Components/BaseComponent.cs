using Newtonsoft.Json;
using TutorialRoguelike.Entities;
using TutorialRoguelike.World;

namespace TutorialRoguelike.Components
{
    public abstract class BaseComponent
    {
        private Entity _parent;
        [JsonIgnore]
        public Entity Parent { get => _parent; set => _parent = value; }

        [JsonIgnore]
        public GameMap Map => Parent.Map;

        [JsonIgnore]
        public Engine Engine => Map.Engine;

        [JsonConstructor]
        protected BaseComponent()
        {
        }

        protected BaseComponent(Entity entity)
        {
            _parent = entity;
        }
    }
}
