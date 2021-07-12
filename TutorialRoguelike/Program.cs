using System.Collections.Generic;
using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;
using TutorialRoguelike.EventHandlers;
using TutorialRoguelike.MapGeneration;
using Console = SadConsole.Console;

namespace TutorialRoguelike
{
    class Program
    {
        public const int DungeonWidth = 80;
        public const int DungeonHeight = 45;
        public const int RoomMinSize = 6;
        public const int RoomMaxSize = 10;
        public const int MaxRooms = 30;
        public const int MaxMonstersPerRoom = 2;

        public const int InfoPanelWidth = DungeonWidth;
        public const int InfoPanelHeight = 5;

        public static Engine Engine;

        public const int WindowWidth = DungeonWidth;
        public const int WindowHeight = DungeonHeight + InfoPanelHeight;

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
            var player = EntityFactory.Player;

            var startingConsole = (Console)GameHost.Instance.Screen;

            var infoConsole = new InfoPanel(InfoPanelWidth, InfoPanelHeight, player);
            infoConsole.Position = (0, DungeonHeight);
            startingConsole.Children.Add(infoConsole);

            Engine = new Engine(player, startingConsole, infoConsole);
            Engine.Map = MapGenerator.GenerateDungeon(DungeonWidth, DungeonHeight, MaxRooms, RoomMinSize, RoomMaxSize, MaxMonstersPerRoom, Engine);

            startingConsole.IsFocused = true;
            startingConsole.SadComponents.Add(new KeyboardHandler(Engine));

            Engine.MessageLog.Add("Hello and welcome, adventurer, to yet another dungeon", Colors.WelcomeText);

            Engine.UpdateFov();
            Engine.Render();
        }
    }
}
