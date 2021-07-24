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

        public override IActionOrEventHandler ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            foreach (var movement in MovementKeys)
            {
                if (keyboard.IsKeyPressed(movement.Key))
                {
                    return new BumpAction(Engine.Player, movement.Value);
                }
            }

            foreach (var key in WaitKeys)
            {
                if (keyboard.IsKeyPressed(key))
                {
                    return new WaitAction(Engine.Player);
                }
            }

            if (keyboard.IsKeyPressed(Keys.V))
            {
                return new HistoryViewer(Engine);
            }

            if (keyboard.IsKeyPressed(Keys.I))
            {
                return new InventoryActivateHandler(Engine);
            }

            if (keyboard.IsKeyPressed(Keys.D))
            {
                return new InventoryDropHandler(Engine);
            }

            if (keyboard.IsKeyPressed(Keys.Divide))
            {
                return new LookHandler(Engine);
            }

            if (keyboard.IsKeyPressed(Keys.G))
            {
                return new PickupAction(Engine.Player);
            }

            if (keyboard.IsKeyPressed(Keys.Escape))
                return new EscapeAction(Engine.Player);

            return null;
        }
    }
}
