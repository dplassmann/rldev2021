using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Manual.Actions;
using TutorialRoguelike.Manual.Constants;

namespace TutorialRoguelike.Manual
{
    class KeyboardHandler : KeyboardConsoleComponent
    {
        private Engine Engine;

        public KeyboardHandler(Engine engine) : base()
        {
            Engine = engine;
        }

        private Dictionary<Keys, Direction> Movements = new Dictionary<Keys, Direction>{ 
            //Arrows
            { Keys.Left, Direction.Left },
            { Keys.Right, Direction.Right },
            { Keys.Up, Direction.Up },
            { Keys.Down, Direction.Down },
            { Keys.Home, Direction.UpLeft },
            { Keys.PageUp, Direction.UpRight },
            { Keys.End, Direction.DownLeft },
            { Keys.PageDown, Direction.DownRight },
            //NumPad
            { Keys.NumPad1, Direction.DownLeft },
            { Keys.NumPad2, Direction.Down },
            { Keys.NumPad3, Direction.DownRight },
            { Keys.NumPad6, Direction.Right },
            { Keys.NumPad9, Direction.UpRight },
            { Keys.NumPad8, Direction.Up },
            { Keys.NumPad7, Direction.UpLeft },
            { Keys.NumPad4, Direction.Left },
        };

        private List<Keys> WaitKeys = new List<Keys>
        {
            Keys.NumPad5, Keys.OemPeriod, Keys.Delete
        };

        public override void ProcessKeyboard(IScreenObject host, Keyboard keyboard, out bool handled)
        {
            IAction action = null;
            handled = false;

            foreach (var movement in Movements)
            {
                if (keyboard.IsKeyPressed(movement.Key))
                {
                    action = new BumpAction(Engine.Player, movement.Value);
                }
            }

            foreach (var key in WaitKeys)
            {
                if (keyboard.IsKeyPressed(key))
                {
                    action = new WaitAction(Engine.Player);
                }
            }

            if (keyboard.IsKeyPressed(Keys.Escape))
                action = new EscapeAction(Engine.Player);

            if (action != null)
            {
                Engine.HandleAction(action);
                handled = true;
            }
        }
    }
}
