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

        public static Engine Engine;

        static void Main(string[] args)
        {
            Game.Create(DungeonWidth, DungeonHeight, "Fonts/OneBit.font");
            Settings.WindowTitle = "Yet Another Roguelike Tutorial - Manual Version";
            Game.Instance.OnStart = Init;
            Game.Instance.FrameUpdate += Instance_FrameUpdate;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Instance_FrameUpdate(object sender, GameHost e)
        {
            Engine.Update();
            Engine.Render((Console) Game.Instance.Screen);
        }

        private static void Init()
        {
            var player = new Player((DungeonWidth / 2, DungeonHeight / 2));
            var npc = new Entity((DungeonWidth / 2 - 5, DungeonHeight / 2), 'n', Colors.Npc);
            var entities = new HashSet<Entity> { player };
            var map = MapGenerator.GenerateDungeon(DungeonWidth, DungeonHeight,MaxRooms,RoomMinSize,RoomMaxSize,player);

            Engine = new Engine(entities, player, map);

            var startingConsole = (Console)GameHost.Instance.Screen;
            startingConsole.IsFocused = true;
            startingConsole.SadComponents.Add(new KeyboardHandler(Engine.HandleAction));
        }
    }
}
