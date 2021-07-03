using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;
using Console = SadConsole.Console;

namespace TutorialRoguelike.Manual
{
    class Program
    {
        public const int Width = 80;
        public const int Height = 50;

        private static Player Player;

        static void Main(string[] args)
        {
            Game.Create(Width, Height);
            Settings.WindowTitle = "Yet Another Roguelike Tutorial - Manual Version";
            Game.Instance.OnStart = Init;
            Game.Instance.FrameUpdate += Instance_FrameUpdate;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            // Any startup code for your game. We will use an example console for now
            var startingConsole = (Console)GameHost.Instance.Screen;
            startingConsole.IsFocused = true;
            startingConsole.SadComponents.Add(new KeyboardHandler());
            
            Player = new Player(Width / 2, Height / 2);
        }

        private static void Instance_FrameUpdate(object sender, GameHost e)
        {
            var console = (Console)GameHost.Instance.Screen;
            console.Clear();
            console.Print(Player.Position.X, Player.Position.Y, "@");
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
