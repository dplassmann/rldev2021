using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace TutorialRoguelike.GoRogue.Entities
{
    public class EntityFactory
    {
        public static RogueLikeEntity Orc(Point position) => new RogueLikeEntity(position, new Color(63, 127, 63), 121, walkable: false) { Name = "Orc" };
        public static RogueLikeEntity Troll(Point position) => new RogueLikeEntity(position, Color.Green, 414, walkable: false) { Name = "Troll" };
    }
}
