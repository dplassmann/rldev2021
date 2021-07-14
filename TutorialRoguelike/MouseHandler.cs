using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;

namespace TutorialRoguelike
{
    public class MouseHandler : MouseConsoleComponent
    {
        private Engine Engine;

        public MouseHandler(Engine engine) : base()
        {
            Engine = engine;
        }
        public override void ProcessMouse(IScreenObject host, MouseScreenObjectState state, out bool handled)
        {
            if (state.IsOnScreenObject)
                handled = Engine.EventHandler.ProcessMouse(host, state);
            else
                handled = false;
            Engine.EventHandler.Render();
        }
    }
}
