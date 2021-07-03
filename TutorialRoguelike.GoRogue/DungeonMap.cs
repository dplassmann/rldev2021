using System;
using System.Collections.Generic;
using System.Text;
using SadRogue.Integration.Maps;
using SadRogue.Primitives;

namespace TutorialRoguelike.GoRogue
{
    public class DungeonMap : RogueLikeMap
    {
        public DungeonMap(Point dungeonSize, Point mapSize) : base(dungeonSize.X, dungeonSize.Y, 1, Distance.Euclidean, viewSize: mapSize) { }
    }
}
