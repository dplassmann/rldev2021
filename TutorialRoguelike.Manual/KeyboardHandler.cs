using System;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;
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

        public override void ProcessKeyboard(IScreenObject host, Keyboard keyboard, out bool handled)
        {
            Actions.GameAction action = null;
            handled = false;

            if (keyboard.IsKeyPressed(Keys.Left))
                action = new BumpAction(Engine.Player, Directions.Left);
            else if (keyboard.IsKeyPressed(Keys.Right))
                action = new BumpAction(Engine.Player, Directions.Right);
            else if (keyboard.IsKeyPressed(Keys.Up))
                action = new BumpAction(Engine.Player, Directions.Up);
            else if (keyboard.IsKeyPressed(Keys.Down))
                action = new BumpAction(Engine.Player, Directions.Down);

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
