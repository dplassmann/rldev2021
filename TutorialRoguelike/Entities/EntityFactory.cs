using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.AI;
using TutorialRoguelike.Components;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Entities
{
    public class EntityFactory
    {
        public static Actor Player => new Actor<BaseAI>(
                appearance: new ColoredGlyph(Colors.Player, Color.Transparent, (char)25), 
                name: "Player", 
                fighter: new Fighter(hp: 30, defense: 2, power: 5));
        public static Actor Orc => new Actor<HostileEnemy>(
                appearance: new ColoredGlyph(new Color(63, 127, 63), Color.Transparent, (char)121),
                name: "Orc",
                fighter: new Fighter(hp: 10, defense: 0, power: 3));
        public static Actor Troll => new Actor<HostileEnemy>(
                appearance: new ColoredGlyph(Color.Green, Color.Transparent, (char)414),
                name: "Troll",
                fighter: new Fighter(hp: 16, defense: 1, power: 4));

        public static Actor Corpse(Actor actor)
        {
            actor.Appearance = new ColoredGlyph(new Color(191, 0, 0), Color.Transparent, (char)720);
            actor.Name = $"remains of {actor.Name}";
            actor.BlocksMovement = false;
            actor.AI = null;
            actor.RenderOrder = RenderOrder.Corpse;

            return actor;
        }
    }
}
