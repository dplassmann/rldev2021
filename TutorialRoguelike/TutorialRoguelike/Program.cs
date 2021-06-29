using System;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
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
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            // Any startup code for your game. We will use an example console for now
            var startingConsole = (Console)GameHost.Instance.Screen;
            startingConsole.Print(PlayerPosition.X, PlayerPosition.Y, "@");
        }
    }
}
