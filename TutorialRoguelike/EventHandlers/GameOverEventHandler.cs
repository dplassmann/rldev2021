using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;

namespace TutorialRoguelike.EventHandlers
{
    public class GameOverEventHandler : EventHandler
    {
        public GameOverEventHandler(Engine engine) : base(engine)
        {
        }

        public override bool ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            if (keyboard.IsKeyPressed(Keys.Escape))
                return HandleAction(new EscapeAction(Engine.Player));

            return false;
        }

        public override bool ProcessMouse(IScreenObject host, MouseScreenObjectState state)
        {
            return false;
        }

        public override bool HandleAction(IAction action)
        {
            action.Perform();

            return true;
        }
    }
}
