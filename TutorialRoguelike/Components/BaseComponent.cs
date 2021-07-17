using System;
using System.Collections.Generic;
using System.Text;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Components
{
    public abstract class BaseComponent
    {
        private Entity _entity;
        public Entity Entity { get => _entity; set => _entity = value; }

        public GameMap Map => Entity.Map;

        public Engine Engine => Map.Engine;

        protected BaseComponent()
        {
        }

        protected BaseComponent(Entity entity)
        {
            _entity = entity;
        }
    }
}
