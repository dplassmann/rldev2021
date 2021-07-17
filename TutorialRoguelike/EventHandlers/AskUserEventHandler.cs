using SadConsole;
using SadConsole.Input;
using TutorialRoguelike.Actions;

namespace TutorialRoguelike.EventHandlers
{
    public class AskUserEventHandler : EventHandler
    {
        private Keys[] ModifierKeys = { Keys.LeftShift, Keys.RightShift, Keys.LeftControl, Keys.RightControl, Keys.LeftAlt, Keys.RightAlt };

        public AskUserEventHandler(Engine engine) : base(engine)
        {
        }

        // By default any non-modifier key exits this input handler.
        public override bool ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            if (keyboard.HasKeysPressed)
            {
                foreach (var key in ModifierKeys)
                {
                    if (keyboard.IsKeyPressed(key))
                    {
                        return false;
                    }
                }
                return Exit();
            }

            return false;
        }

        // By default any mouse click exits this input handler.
        public override bool ProcessMouse(IScreenObject host, MouseScreenObjectState state)
        {
            if (state.Mouse.LeftClicked || state.Mouse.RightClicked || state.Mouse.MiddleClicked)
                return Exit();

            return base.ProcessMouse(host, state);
        }

        // Return to the main event handler when a valid action was performed
        public override bool HandleAction(IAction action)
        {
            if (base.HandleAction(action))
            {
                ResetEventHandler();
                return true;
            }
            return false;
        }

        // Called when the user is trying to exit or cancel an action
        //
        // By default this returns to the main event handler
        public bool Exit()
        {
            ResetEventHandler();
            return false;
        }

        public virtual void ResetEventHandler()
        {
            Engine.EventHandler = new MainGameEventHandler(Engine);
        }
    }
}
