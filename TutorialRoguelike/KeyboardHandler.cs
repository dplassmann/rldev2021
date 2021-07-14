using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;

namespace TutorialRoguelike
{
    class KeyboardHandler : KeyboardConsoleComponent
    {
        private Engine Engine;

        public KeyboardHandler(Engine engine) : base()
        {
            Engine = engine;
        }

        public override void ProcessKeyboard(IScreenObject host, Keyboard keyboard, out bool handled)
        {
            handled = Engine.EventHandler.ProcessKeyboard(host, keyboard);
            Engine.EventHandler.Render();
        }
    }
}
