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

        private static Entity Player;
        private static Renderer EntityManager;

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
            startingConsole.IsFocused = true;
            EntityManager = new Renderer();
            startingConsole.SadComponents.Add(EntityManager);
            startingConsole.SadComponents.Add(new KeyboardHandler());

            Player = new Entity(new ColoredGlyph(Color.White, Color.Black, '@'), 10);
            Player.Position = new Point(Width / 2, Height / 2);
            EntityManager.Add(Player);
        }

        public static void HandleAction(IAction action)
        {
            if (action is EscapeAction)
            {
                Game.Instance.MonoGameInstance.Exit();
            }
            if (action is MovementAction move)
            {
                Player.Position = Player.Position.Add(move.Delta);
            }
        }
    }
}
