using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;

namespace TutorialRoguelike.EventHandlers
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
                    return HandleTurnAction(new BumpAction(Engine.Player, movement.Value));
                }
            }

            foreach (var key in WaitKeys)
            {
                if (keyboard.IsKeyPressed(key))
                {
                    return HandleTurnAction(new WaitAction(Engine.Player));
                }
            }

            if (keyboard.IsKeyPressed(Keys.Escape))
                return HandleTurnAction(new EscapeAction(Engine.Player));

            return false;
        }

        private bool HandleTurnAction(IAction action)
        {
            if (action != null)
                action.Perform();
            Engine.HandleEnemyTurns();
            Engine.UpdateFov();
            Engine.Render();

            return true;
        }

        private bool HandleNonTurnAction(IAction action = null)
        {
            if (action != null)
                action.Perform();
            Engine.Render();

            return true;
        }

        public override bool ProcessMouse(IScreenObject host, MouseScreenObjectState state)
        {
            if (Engine.Map.InBounds(state.CellPosition))
            {
                Engine.MouseLocation = state.CellPosition;
                return HandleNonTurnAction();
            }

            return false;
        }
    }
}
