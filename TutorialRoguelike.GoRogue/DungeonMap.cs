using System.Collections.Generic;
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
                    gen.AddSteps(new TestWallGenerationStep());
                });

            var generatedMap = generator.Context.GetFirst<ISettableGridView<bool>>(WallFloor);

            foreach (var location in this.Positions())
            {
                bool walkable = generatedMap[location];
                Color foreground = walkable ? Colors.Floor : Colors.Wall;
                int glyph = walkable ? 0 : 826; 
                SetTerrain(new RogueLikeCell(location, foreground, Colors.Background, glyph, 0, walkable, walkable));
            }
        }
        private class TestWallGenerationStep : GenerationStep
        {
            public TestWallGenerationStep() : base(null, (typeof(ISettableGridView<bool>), WallFloor))
            {
            }

            protected override IEnumerator<object> OnPerform(GenerationContext context)
            {
                var wallFloor = context.GetFirst<ISettableGridView<bool>>(WallFloor);

                foreach(var p in Rectangle.WithPositionAndSize((30, 22), 3, 1).Positions())
                {
                    wallFloor[p] = false;
                }

                yield return null;
            }
        }
    }
}
