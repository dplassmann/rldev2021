using SadConsole;
using SadRogue.Primitives;
using SadRogue.Integration;

namespace TutorialRoguelike.GoRogue
{
    class Program
    {
        public static readonly Point DungeonSize = (80, 50);
        public static readonly Point MapSize = (80, 50);

        private static Player Player;
        private static DungeonMap DungeonMap;

        static void Main(string[] args)
        {
            Game.Create(MapSize.X, MapSize.Y, "Fonts/OneBit.font");
            Settings.WindowTitle = "Yet Another Roguelike Tutorial - GoRogue Version";
            Game.Instance.OnStart = Init;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            DungeonMap = new DungeonMap(DungeonSize, MapSize);
            Player = new Player(MapSize / 2);
            DungeonMap.AddEntity(Player);
            DungeonMap.AddEntity(GenerateNpc(Player.Position - (5,0)));

            Game.Instance.Screen = DungeonMap;
        }

        private static RogueLikeEntity GenerateNpc(Point position)
        {
            return new RogueLikeEntity(position, Colors.Npc, 'n', false);
        }
    }
}
