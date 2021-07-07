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

        private static Player Player;
        private static DungeonMap DungeonMap;

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
            var mapGenerator = new MapGenerator(MaxRooms, RoomMinSize, RoomMaxSize, DungeonWidth, DungeonHeight);
            Player = new Player(mapGenerator.PlayerSpawnPoint);
            DungeonMap = new DungeonMap(mapGenerator, DungeonWidth, DungeonHeight, MapWidth, MapHeight);
            DungeonMap.AddEntity(Player);
            //DungeonMap.AddEntity(GenerateNpc(Player.Position - (5,0)));

            Game.Instance.Screen = DungeonMap;
        }

        private static RogueLikeEntity GenerateNpc(Point position)
        {
            return new RogueLikeEntity(position, Colors.Npc, 'n', false);
        }
    }
}
