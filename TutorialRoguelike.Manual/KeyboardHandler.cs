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
        public Action<IAction> ActionHandler;

        public KeyboardHandler(Action<IAction> actionHandler)
        {
            ActionHandler = actionHandler;
        }

        public override void ProcessKeyboard(IScreenObject host, Keyboard keyboard, out bool handled)
        {
            IAction action = null;
            handled = false;

            if (keyboard.IsKeyPressed(Keys.Left))
                action = new BumpAction(Directions.Left);
            else if (keyboard.IsKeyPressed(Keys.Right))
                action = new BumpAction(Directions.Right);
            else if (keyboard.IsKeyPressed(Keys.Up))
                action = new BumpAction(Directions.Up);
            else if (keyboard.IsKeyPressed(Keys.Down))
                action = new BumpAction(Directions.Down);

            if (keyboard.IsKeyPressed(Keys.Escape))
                action = new EscapeAction();

            if (action != null)
            {
                ActionHandler(action);
                handled = true;
            }
        }
    }
}
