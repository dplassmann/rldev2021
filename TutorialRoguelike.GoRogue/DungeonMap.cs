using SadRogue.Integration.FieldOfView.Memory;
using SadRogue.Integration.Maps;
using SadRogue.Primitives;
using TutorialRoguelike.GoRogue.MapGeneration;

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
            AllComponents.Add(new DimmingMemoryFieldOfViewHandler(0.8f));
            ApplyTerrainOverlay(generator.Terrain);
        }

    }
}
