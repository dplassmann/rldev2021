using SadConsole;

namespace TutorialRoguelike
{
    class Program
    {
        public const int WindowWidth = 80;
        public const int WindowHeight = 50;

        static void Main(string[] args)
        {
            Game.Create(WindowWidth, WindowHeight, "Fonts/OneBit.font");
            Settings.WindowTitle = "Yet Another Roguelike Tutorial - Manual Version";
            Game.Instance.OnStart = Init;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            var console = Game.Instance.StartingConsole;
            console.IsFocused = true;
            console.SadComponents.Add(new InputHandler(new MainMenu()));
        }
    }
}
