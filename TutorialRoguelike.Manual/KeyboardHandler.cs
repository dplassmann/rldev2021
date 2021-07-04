using System;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;
using TutorialRoguelike.Actions;

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
                action = new MovementAction(-1, 0);
            else if (keyboard.IsKeyPressed(Keys.Right))
                action = new MovementAction(1, 0);
            else if (keyboard.IsKeyPressed(Keys.Up))
                action = new MovementAction(0, -1);
            else if (keyboard.IsKeyPressed(Keys.Down))
                action = new MovementAction(0, 1);

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
