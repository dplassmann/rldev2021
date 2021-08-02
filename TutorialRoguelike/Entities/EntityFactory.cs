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
        public static Func<Actor> Player = () => new Actor<BaseAI>(
                appearance: new ColoredGlyph(Colors.Player, Color.Transparent, (char)25),
                name: "Player",
                equipment: new Equipment(),
                fighter: new Fighter(hp: 30, baseDefense: 1, basePower: 2),
                inventory: new Inventory(26),
                level: new Level(levelUpBase: 200));
        public static Func<Actor> Orc = () => new Actor<HostileEnemy>(
                appearance: new ColoredGlyph(Colors.Orc, Color.Transparent, (char)121),
                name: "Orc",
                equipment: new Equipment(),
                fighter: new Fighter(hp: 10, baseDefense: 0, basePower: 3),
                inventory: new Inventory(0),
                level: new Level(xpGiven: 35));
        public static Func<Actor> Troll = () => new Actor<HostileEnemy>(
                appearance: new ColoredGlyph(Colors.Orc, Color.Transparent, (char)414),
                name: "Troll",
                equipment: new Equipment(),
                fighter: new Fighter(hp: 16, baseDefense: 1, basePower: 4),
                inventory: new Inventory(0),
                level: new Level(xpGiven: 100));

        //Items
        public static Func<Item> HealthPotion = () => new Item(
            appearance: new ColoredGlyph(Colors.HealthPotion, Color.Transparent, 569), 
            name: "Health Potion", 
            consumable: new HealingConsumable(amount: 4));
        public static Func<Item> LightningScroll = () => new Item(
            appearance: new ColoredGlyph(Colors.LightningScroll, Color.Transparent, 753), 
            name: "Lightning Scroll", 
            consumable: new LightningDamageConsumable(damage: 20, maximumRange: 5));
        public static Func<Item> ConfusionScroll = () => new Item(
            appearance: new ColoredGlyph(Colors.ConfusionScroll, Color.Transparent, 753),
            name: "Confusion Scroll",
            consumable: new ConfusionConsumable(numberOfTurns: 10));
        public static Func<Item> FireballScroll = () => new Item(
            appearance: new ColoredGlyph(Colors.FireballScroll, Color.Transparent, 753),
            name: "Fireball Scroll",
            consumable: new FireballDamageConsumable(damage: 12, radius: 3));

        public static Func<Item> Dagger = () => new Item(
            appearance: new ColoredGlyph(Colors.Weapon, Color.Transparent, 320),
            name: "Dagger",
            equippable: new Dagger());
        public static Func<Item> Sword = () => new Item(
            appearance: new ColoredGlyph(Colors.Weapon, Color.Transparent, 419),
            name: "Sword",
            equippable: new Sword());

        public static Func<Item> LeatherArmor = () => new Item(
            appearance: new ColoredGlyph(Colors.LeatherArmor, Color.Transparent, 81),
            name: "Leather Armor",
            equippable: new LeatherArmor());
        public static Func<Item> ChainMail = () => new Item(
            appearance: new ColoredGlyph(Colors.ChainMail, Color.Transparent, 81),
            name: "Chain Mail",
            equippable: new ChainMail());

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
