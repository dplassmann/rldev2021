using System;
using System.Collections.Generic;
using System.Text;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public interface IAction
    {
        public Actor Entity { get; }

        public Engine Engine => Entity.Map.Engine;

        public void Perform();
    }
}
