using SadConsole;
using SadConsole.Input;
using TutorialRoguelike.Actions;

namespace TutorialRoguelike.EventHandlers
{
    public class MainGameEventHandler : EventHandler
    {
        public MainGameEventHandler(Engine engine) : base(engine)
        {
        }

        public override bool ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            foreach (var movement in MovementKeys)
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

            if (keyboard.IsKeyPressed(Keys.V))
            {
                Engine.EventHandler = new HistoryViewer(Engine);
                return true;
            }

            if (keyboard.IsKeyPressed(Keys.I))
            {
                Engine.EventHandler = new InventoryActivateHandler(Engine);
                return true;
            }

            if (keyboard.IsKeyPressed(Keys.D))
            {
                Engine.EventHandler = new InventoryDropHandler(Engine);
                return true;
            }

            if (keyboard.IsKeyPressed(Keys.Divide))
            {
                Engine.EventHandler = new LookHandler(Engine);
                return true;
            }

            if (keyboard.IsKeyPressed(Keys.G))
            {
                return HandleAction(new PickupAction(Engine.Player));
            }

            if (keyboard.IsKeyPressed(Keys.Escape))
                return HandleAction(new EscapeAction(Engine.Player));

            return false;
        }
    }
}
