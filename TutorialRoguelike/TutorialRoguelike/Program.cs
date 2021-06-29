using System;
using SadConsole;
using SadConsole.Entities;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;
using Console = SadConsole.Console;

namespace TutorialRoguelike
{
    class Program
    {
        public const int Width = 80;
        public const int Height = 50;
        public static Point PlayerPosition = new Point(Width / 2, Height / 2);

        static void Main(string[] args)
        {
            Game.Create(Width, Height);
            Settings.WindowTitle = "Yet Another Roguelike Tutorial";
            Game.Instance.OnStart = Init;
            Game.Instance.FrameUpdate += Instance_FrameUpdate;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Instance_FrameUpdate(object sender, GameHost e)
        {
            var console = (Console)GameHost.Instance.Screen;
            console.Clear();
            console.Print(PlayerPosition.X, PlayerPosition.Y, "@");
        }

        private static void Init()
        {
            // Any startup code for your game. We will use an example console for now
            var startingConsole = (Console)GameHost.Instance.Screen;
            startingConsole.IsFocused = true;
            startingConsole.SadComponents.Add(new KeyboardHandler());
        }

        public static void HandleAction(IAction action)
        {
            if (action is EscapeAction)
            {
                Game.Instance.MonoGameInstance.Exit();
            }
            if (action is MovementAction move)
            {
                PlayerPosition = PlayerPosition.Add(move.Delta);
            }
        }
    }
}
