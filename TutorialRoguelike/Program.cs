using SadConsole;
using TutorialRoguelike.UI;

namespace TutorialRoguelike
{
    class Program
    {
        public const int WindowWidth = 80;
        public const int WindowHeight = 50;

        public static bool SaveOnExit = true;

        private static InputHandler InputHandler { get; set; }

        static void Main(string[] args)
        {
            Game.Create(WindowWidth, WindowHeight, "Fonts/OneBit.font");
            Settings.WindowTitle = "Yet Another Roguelike Tutorial - Manual Version";
            Game.Instance.OnStart = Init;
            Game.Instance.OnEnd = End;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            var console = Game.Instance.StartingConsole;
            console.IsFocused = true;
            InputHandler = new InputHandler(new MainMenu());
            console.SadComponents.Add(InputHandler);
        }

        public static void End()
        {
            if (SaveOnExit)
                InputHandler.SaveGame();
        } 
    }
}
