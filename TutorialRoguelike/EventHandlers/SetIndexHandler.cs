using System;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.EventHandlers
{
    public abstract class SetIndexHandler : AskUserEventHandler
    {
        private Actor Player => Engine.Player;

        // Sets the cursor to the player when this handler is constructed.
        public SetIndexHandler(Engine engine) : base(engine)
        {
            Engine.MouseLocation = Player.Position;
        }

        // Highlight the tile under the cursor.
        public override void Render()
        {
            base.Render();
            Engine.Console.AddDecorator(Engine.MouseLocation.X, Engine.MouseLocation.Y, 1, new[] { new CellDecorator(Color.Red, 817, Mirror.None) });
        }

        public override bool ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            if (keyboard.HasKeysPressed)
            {
                foreach (var key in MovementKeys)
                {
                    if (keyboard.IsKeyPressed(key.Key))
                    {
                        var modifier = 1; //Holding modifier keys will speed up movement
                        if (keyboard.IsKeyReleased(Keys.LeftShift) || keyboard.IsKeyReleased(Keys.RightShift)) //Shift handling seems weird in SadConsole - gives Released event
                            modifier *= 5;
                        if (keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl))
                            modifier *= 10;
                        if (keyboard.IsKeyDown(Keys.LeftAlt) || keyboard.IsKeyDown(Keys.RightAlt))
                            modifier *= 20;

                        var targetPosition = Engine.MouseLocation;
                        var delta = new Point(key.Value.DeltaX, key.Value.DeltaY);
                        targetPosition += delta * modifier;
                        // Clamp the cursor to the map size;
                        targetPosition = new Point(
                            Math.Max(0, Math.Min(targetPosition.X, Engine.Map.Width - 1)), 
                            Math.Max(0, Math.Min(targetPosition.Y, Engine.Map.Height - 1)));
                        Engine.MouseLocation = targetPosition;
                        return true;
                    }
                }

                foreach (var key in ConfirmationKeys)
                {
                    if (keyboard.IsKeyPressed(key))
                    {
                        return HandleAction(IndexSelected(Engine.MouseLocation));
                    }
                }
            }

            return base.ProcessKeyboard(host, keyboard);
        }

        public override bool ProcessMouse(IScreenObject host, MouseScreenObjectState state)
        {
            // Left click confirms selection
            if (state.Mouse.LeftClicked && Engine.Map.InBounds(state.CellPosition))
            {
                return HandleAction(IndexSelected(Engine.MouseLocation));
            }

            return base.ProcessMouse(host, state);
        }

        public abstract IAction IndexSelected(Point position);
    }
}
