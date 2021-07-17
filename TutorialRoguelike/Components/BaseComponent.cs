using System;
using System.Collections.Generic;
using System.Text;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Components
{
    public abstract class BaseComponent
    {
        private Entity _parent;
        public Entity Parent { get => _parent; set => _parent = value; }

        public GameMap Map => Parent.Map;

        public Engine Engine => Map.Engine;

        protected BaseComponent()
        {
        }

        protected BaseComponent(Entity entity)
        {
            _parent = entity;
        }
    }
}
