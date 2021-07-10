using SadConsole;
using SadRogue.Primitives;
using SadRogue.Integration;
using TutorialRoguelike.GoRogue.MapGeneration;
using TutorialRoguelike.GoRogue.Entities;

namespace TutorialRoguelike.GoRogue
{
    class Program
    {
        public const int DungeonWidth = 80;
        public const int DungeonHeight = 45;
        public const int MapWidth = 80;
        public const int MapHeight = 50;

        public const int MaxRooms = 30;
        public const int RoomMinSize = 6;
        public const int RoomMaxSize = 10;

        public static Engine Engine;


        static void Main(string[] args)
        {
            Game.Create(MapWidth, MapHeight, "Fonts/OneBit.font");
            Settings.WindowTitle = "Yet Another Roguelike Tutorial - GoRogue Version";
            Game.Instance.OnStart = Init;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            var mapGenerator = new MapGenerator(MaxRooms, RoomMinSize, RoomMaxSize, DungeonWidth, DungeonHeight, 2);
            var dungeonMap = new DungeonMap(mapGenerator, DungeonWidth, DungeonHeight, MapWidth, MapHeight);
            Engine = new Engine(mapGenerator.Player, dungeonMap);

            Game.Instance.Screen = Engine;
        }
    }
}
