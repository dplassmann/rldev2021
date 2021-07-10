using System.Collections.Generic;
using System.Linq;
using GoRogue.MapGeneration;
using SadRogue.Integration;
using SadRogue.Integration.FieldOfView.Memory;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using TutorialRoguelike.GoRogue.Entities;

namespace TutorialRoguelike.GoRogue.MapGeneration
{
    public class MapGenerator
    {
        public ISettableGridView<MemoryAwareRogueLikeCell> Terrain;
        public ISet<RogueLikeEntity> Entities;
        public Player Player;
        public Point PlayerSpawnPoint;

        public MapGenerator(int maxRooms, int roomMinSize, int roomMaxSize, int dungeonWidth, int dungeonHeight, int maxMonstersPerRoom)
        {
            var generator = new Generator(dungeonWidth, dungeonHeight)
                .ConfigAndGenerateSafe(gen =>
                {
                    gen.AddSteps(
                        new RectangularRoomsStep(maxRooms, roomMinSize, roomMaxSize),
                        new TunnelsStep(),
                        new DigStep(),
                        new EnemySpawnStep(maxMonstersPerRoom));
                });
            Terrain = generator.Context.GetFirst<ISettableGridView<MemoryAwareRogueLikeCell>>("WallFloor");
            Entities = generator.Context.GetFirst<ISet<RogueLikeEntity>>("Entities");
            var playerSpawnPoint = generator.Context.GetFirst<IEnumerable<RectangularRoom>>("Rooms").First().Center;
            Player = new Player(playerSpawnPoint);
            Entities.Add(Player);
        }
    }
}
