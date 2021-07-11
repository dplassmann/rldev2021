using System;
using System.Collections.Generic;
using System.Text;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Actions
{
    public interface IAction
    {
        public Actor Entity { get; }

        public Engine Engine => Entity.Map.Engine;

        public void Perform();
    }
}
