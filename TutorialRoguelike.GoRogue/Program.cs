using SadConsole;
using SadRogue.Primitives;
using SadRogue.Integration.Maps;

namespace TutorialRoguelike.GoRogue
{
    class Program
    {
        public static readonly Point DungeonSize = (80, 50);
        public static readonly Point MapSize = (80, 50);


        private static Player Player;
        private static DungeonMap Map;

        static void Main(string[] args)
        {
            Game.Create(MapSize.X, MapSize.Y);
            Settings.WindowTitle = "Yet Another Roguelike Tutorial - GoRogue Version";
            Game.Instance.OnStart = Init;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            Map = GenerateMap();
            Player = GeneratePlayer();
            Map.AddEntity(Player);

            Game.Instance.Screen = Map;
        }

        private static Player GeneratePlayer()
        {
            return new Player(MapSize / 2);
        }

        private static DungeonMap GenerateMap()
        {
            return new DungeonMap(DungeonSize, MapSize);
        }
    }
}
