using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;
using SadRogue.Primitives;

namespace TutorialRoguelike.GoRogue
{
    public class KeyboardComponent : KeyboardConsoleComponent
    {
        private Dictionary<Keys, Direction> Motions = new Dictionary<Keys, Direction>()
        {
            { Keys.Left, Direction.Left},
            { Keys.Right, Direction.Right},
            { Keys.Up, Direction.Up},
            { Keys.Down, Direction.Down},
        };

        private Dictionary<Keys, Action> Actions = new Dictionary<Keys, Action>()
        {
            { Keys.Escape, Game.Instance.MonoGameInstance.Exit }
        };

        public override void ProcessKeyboard(IScreenObject host, Keyboard keyboard, out bool handled)
        {
            foreach (var motion in Motions)
            {
                if (keyboard.IsKeyPressed(motion.Key))
                {
                    if (Program.Engine.Player.Move(motion.Value))
                    {
                        Program.Engine.ProcessTurn();
                    }
                }
            }

            foreach (KeyValuePair<Keys, Action> action in Actions)
                if (keyboard.IsKeyPressed(action.Key))
                    action.Value();

            handled = true;
        }
    }
}
