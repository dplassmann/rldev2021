using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Manual.Constants;

namespace TutorialRoguelike.Manual.Entities
{
    public class EntityFactory
    {
        public static Entity Player => new Entity(new ColoredGlyph(Colors.Player, Color.Transparent, (char)25), "Player", true);
        public static Entity Orc => new Entity(new ColoredGlyph(new Color(63, 127, 63), Color.Transparent, (char)121), "Orc", true);
        public static Entity Troll => new Entity(new ColoredGlyph(Color.Green, Color.Transparent, (char)414), "Troll", true);
    }
}
