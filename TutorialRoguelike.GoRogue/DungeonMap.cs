using SadRogue.Integration.Maps;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using TutorialRoguelike.GoRogue.MapGeneration;
using TutorialRoguelike.GoRogue.Terrain;

namespace TutorialRoguelike.GoRogue
{
    public class DungeonMap : RogueLikeMap
    {
        public DungeonMap(MapGenerator generator,
                          int dungeonWidth,
                          int dungeonHeight,
                          int viewWidth,
                          int viewHeight) : base(dungeonWidth, dungeonHeight, 1, Distance.Euclidean, viewSize: (viewWidth, viewHeight))
        {
            foreach (var location in generator.Tiles.Positions())
            {
                SetTerrain(TileFactory.Create(generator.Tiles[location], location));
            }
        }

    }
}
