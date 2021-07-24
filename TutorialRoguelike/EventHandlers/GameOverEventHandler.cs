using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Exceptions;

namespace TutorialRoguelike.EventHandlers
{
    public class GameOverEventHandler : EventHandler
    {
        public GameOverEventHandler(Engine engine) : base(engine)
        {
        }

        public override IActionOrEventHandler ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            if (keyboard.IsKeyPressed(Keys.Escape))
                throw new SystemExit();

            return null;
        }

        public override IActionOrEventHandler ProcessMouse(IScreenObject host, MouseScreenObjectState state)
        {
            return null;
        }
    }
}
