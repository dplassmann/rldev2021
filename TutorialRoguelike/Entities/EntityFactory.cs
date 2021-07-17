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
        // Actors
        public static Actor Player => new Actor<BaseAI>(
                appearance: new ColoredGlyph(Colors.Player, Color.Transparent, (char)25),
                name: "Player",
                fighter: new Fighter(hp: 30, defense: 2, power: 5),
                inventory: new Inventory(26));
        public static Actor Orc => new Actor<HostileEnemy>(
                appearance: new ColoredGlyph(Colors.Orc, Color.Transparent, (char)121),
                name: "Orc",
                fighter: new Fighter(hp: 10, defense: 0, power: 3),
                inventory: new Inventory(0));
        public static Actor Troll => new Actor<HostileEnemy>(
                appearance: new ColoredGlyph(Colors.Orc, Color.Transparent, (char)414),
                name: "Troll",
                fighter: new Fighter(hp: 16, defense: 1, power: 4),
                inventory: new Inventory(0));

        //Items
        public static Item HealthPotion => new Item(
            appearance: new ColoredGlyph(Colors.HealthPotion, Color.Transparent, 569), 
            name: "Health Potion", 
            consumable: new HealingConsumable(amount: 4));
        public static Item LightningScroll => new Item(
            appearance: new ColoredGlyph(Colors.LightningScroll, Color.Transparent, 753), 
            name: "Lightning Scroll", 
            consumable: new LightningDamageConsumable(damage: 20, maximumRange: 5));

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
