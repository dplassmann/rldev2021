using System.Collections.Generic;
using System.Linq;
using GoRogue.MapGeneration;
using SadRogue.Integration;
using SadRogue.Integration.FieldOfView.Memory;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using TutorialRoguelike.GoRogue.Terrain;

namespace TutorialRoguelike.GoRogue.MapGeneration
{
    public class MapGenerator
    {
        public ISettableGridView<MemoryAwareRogueLikeCell> Terrain;
        public Point PlayerSpawnPoint;

        public MapGenerator(int maxRooms, int roomMinSize, int roomMaxSize, int dungeonWidth, int dungeonHeight)
        {
            var generator = new Generator(dungeonWidth, dungeonHeight)
                .ConfigAndGenerateSafe(gen =>
                {
                    gen.AddSteps(
                        new RectangularRoomsStep(maxRooms, roomMinSize, roomMaxSize),
                        new TunnelsStep(),
                        new DigStep());
                });
            Terrain = generator.Context.GetFirst<ISettableGridView<MemoryAwareRogueLikeCell>>("WallFloor");
            PlayerSpawnPoint = generator.Context.GetFirst<IEnumerable<RectangularRoom>>("Rooms").First().Center;
        }
    }
}
