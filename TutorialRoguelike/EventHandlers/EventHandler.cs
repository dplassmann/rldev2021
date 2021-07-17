using System.Collections.Generic;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.EventHandlers
{
    public abstract class EventHandler
    {
        private MouseScreenObjectState LastMouseState { get; set; }


        protected Dictionary<Keys, Direction> MovementKeys = new Dictionary<Keys, Direction>{
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

        protected Keys[] ConfirmationKeys = new[] { Keys.Enter };

        protected Keys[] WaitKeys = new[]
        {
            Keys.NumPad5, Keys.OemPeriod, Keys.Delete
        };
        protected EventHandler(Engine engine)
        {
            Engine = engine;
        }

        public Engine Engine { get; private set; }

        public abstract bool ProcessKeyboard(IScreenObject host, Keyboard keyboard);

        public virtual bool ProcessMouse(IScreenObject host, MouseScreenObjectState state)
        {
            // Ignore first mouse event - prevent mouse move from triggering on every handler start
            // Could avoid this by constructing event handlers with mouse state from previous?
            if (LastMouseState == null)
            {
                LastMouseState = state;
                return false;
            }

            if (state.IsOnScreenObject)
            {
                if (LastMouseState.CellPosition != state.CellPosition)
                {
                    if (Engine.Map.InBounds(state.CellPosition))
                    {
                        Engine.MouseLocation = state.CellPosition;
                        LastMouseState = state;
                        return true;
                    }
                }
                LastMouseState = state;
            }
            return false;
        }

        public virtual bool HandleAction(IAction action)
        {
            if (action == null)
            {
                return false;
            }

            try
            {
                action.Perform();
                Engine.HandleEnemyTurns();
                Engine.UpdateFov();
                return true;
            }
            catch (ImpossibleException ex)
            {
                Engine.MessageLog.Add(ex.Message, Colors.Impossible);
                return false;
            }
        }

        public virtual void Render()
        {
            Engine.Render();
        }

        public virtual void TransitionTo(EventHandler newHandler)
        {
            Engine.EventHandler = newHandler;
        }
    }
}
