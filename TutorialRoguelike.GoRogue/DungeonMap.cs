using System.Collections.Generic;
using System.Linq;
using GoRogue.MapGeneration;
using SadRogue.Integration;
using SadRogue.Integration.Maps;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace TutorialRoguelike.GoRogue
{
    public class DungeonMap : RogueLikeMap
    {
        private const string WallFloor = "WallFloor";

        public DungeonMap(Point dungeonSize, Point mapSize) : base(dungeonSize.X, dungeonSize.Y, 4, Distance.Euclidean, viewSize: mapSize) {
            var generator = new Generator(dungeonSize.X, dungeonSize.Y)
                .ConfigAndGenerateSafe(gen =>
                {
                    gen.AddSteps(DefaultAlgorithms.RectangleMapSteps());
                    gen.AddSteps(new TileTypeStep());
                    gen.AddSteps(new TestWallGenerationStep());
                });

            var generatedMap = generator.Context.GetFirst<ISettableGridView<TileTypes>>(WallFloor);

            foreach (var location in this.Positions())
            {
                SetTerrain(TileFactory.Get(generatedMap[location], location));
            }
        }

        private class TileTypeStep : GenerationStep
        {
            public TileTypeStep() : base(null, (typeof(ISettableGridView<bool>), WallFloor))
            {
            }

            protected override IEnumerator<object> OnPerform(GenerationContext context)
            {
                var walkability = context.GetFirst<ArrayView<bool>>(WallFloor);
                context.Remove(walkability);

                var tileArray = walkability.ToArray().Select(p => p ? TileTypes.Floor : TileTypes.Wall).ToArray();
                var wallFloor = new ArrayView<TileTypes>(tileArray, walkability.Width);

                context.Add(wallFloor, WallFloor);

                yield return null;
            }
        }

        private class TestWallGenerationStep : GenerationStep
        {
            public TestWallGenerationStep() : base(null, (typeof(ISettableGridView<TileTypes>), WallFloor))
            {
            }

            protected override IEnumerator<object> OnPerform(GenerationContext context)
            {
                var wallFloor = context.GetFirst<ISettableGridView<TileTypes>>(WallFloor);

                foreach(var p in Rectangle.WithPositionAndSize((30, 22), 3, 1).Positions())
                {
                    wallFloor[p] = TileTypes.Wall;
                }

                yield return null;
            }
        }
    }
}
