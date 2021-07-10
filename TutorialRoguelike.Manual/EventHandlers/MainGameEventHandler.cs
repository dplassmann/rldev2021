using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Manual.Actions;

namespace TutorialRoguelike.Manual.EventHandlers
{
    public class MainGameEventHandler : EventHandler
    {
        public MainGameEventHandler(Engine engine) : base(engine)
        {
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

        public override bool ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            foreach (var movement in Movements)
            {
                if (keyboard.IsKeyPressed(movement.Key))
                {
                    return HandleAction(new BumpAction(Engine.Player, movement.Value));
                }
            }

            foreach (var key in WaitKeys)
            {
                if (keyboard.IsKeyPressed(key))
                {
                    return HandleAction(new WaitAction(Engine.Player));
                }
            }

            if (keyboard.IsKeyPressed(Keys.Escape))
                return HandleAction(new EscapeAction(Engine.Player));

            return false;
        }

        private bool HandleAction(IAction action)
        {
            action.Perform();
            Engine.HandleEnemyTurns();
            Engine.UpdateFov();
            Engine.Render();

            return true;
        }
    }
}
