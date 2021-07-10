using System.Collections.Generic;
using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Manual.Entities;
using TutorialRoguelike.Manual.MapGeneration;
using Console = SadConsole.Console;

namespace TutorialRoguelike.Manual
{
    class Program
    {
        public const int DungeonWidth = 80;
        public const int DungeonHeight = 45;
        public const int RoomMinSize = 6;
        public const int RoomMaxSize = 10;
        public const int MaxRooms = 30;
        public const int MaxMonstersPerRoom = 2;

        public static Engine Engine;

        static void Main(string[] args)
        {
            Game.Create(DungeonWidth, DungeonHeight, "Fonts/OneBit.font");
            Settings.WindowTitle = "Yet Another Roguelike Tutorial - Manual Version";
            Game.Instance.OnStart = Init;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            var player = EntityFactory.Player;

            var startingConsole = (Console)GameHost.Instance.Screen;

            var infoConsole = new Console(startingConsole.Width, 1);
            infoConsole.Font = Game.Instance.EmbeddedFont;
            infoConsole.Position = (0, startingConsole.Height - 1);
            startingConsole.Children.Add(infoConsole);

            Engine = new Engine(player, startingConsole, infoConsole);
            Engine.Map = MapGenerator.GenerateDungeon(DungeonWidth, DungeonHeight, MaxRooms, RoomMinSize, RoomMaxSize, MaxMonstersPerRoom, Engine);
            Engine.UpdateFov();

            startingConsole.IsFocused = true;
            startingConsole.SadComponents.Add(new KeyboardHandler(Engine));
            Engine.Render();
        }
    }
}
