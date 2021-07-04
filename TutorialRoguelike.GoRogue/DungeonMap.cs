using GoRogue.MapGeneration;
using SadRogue.Integration;
using SadRogue.Integration.Maps;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace TutorialRoguelike.GoRogue
{
    public class DungeonMap : RogueLikeMap
    {
        public DungeonMap(Point dungeonSize, Point mapSize) : base(dungeonSize.X, dungeonSize.Y, 4, Distance.Euclidean, viewSize: mapSize) {
            var generator = new Generator(dungeonSize.X, dungeonSize.Y)
                .ConfigAndGenerateSafe(gen =>
                {
                    gen.AddSteps(DefaultAlgorithms.RectangleMapSteps());
                });

            var generatedMap = generator.Context.GetFirst<ISettableGridView<bool>>("WallFloor");

            foreach (var location in this.Positions())
            {
                bool walkable = generatedMap[location];
                Color background = walkable ? Colors.Floor : Colors.Wall;
                SetTerrain(new RogueLikeCell(location, Color.White, background, ' ', 0, walkable, walkable));
            }
        }
    }
}
