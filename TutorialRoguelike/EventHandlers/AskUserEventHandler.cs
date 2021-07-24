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
        public override IActionOrEventHandler ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            if (keyboard.HasKeysPressed)
            {
                foreach (var key in ModifierKeys)
                {
                    if (keyboard.IsKeyPressed(key))
                    {
                        return null;
                    }
                }
                return Exit();
            }

            return null;
        }

        // By default any mouse click exits this input handler.
        public override IActionOrEventHandler ProcessMouse(IScreenObject host, MouseScreenObjectState state)
        {
            if (state.Mouse.LeftClicked || state.Mouse.RightClicked || state.Mouse.MiddleClicked)
                return Exit();

            return base.ProcessMouse(host, state);
        }

        // Called when the user is trying to exit or cancel an action
        //
        // By default this returns to the main event handler
        public EventHandler Exit()
        {
            return new MainGameEventHandler(Engine);
        }
    }
}
