using System;
using System.Collections.Generic;
using System.Linq;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using SadRogue.Integration;
using TutorialRoguelike.GoRogue.Entities;

namespace TutorialRoguelike.GoRogue
{
    public class Engine : ScreenObject
    {
        public Player Player;
        public DungeonMap Map;

        public Engine(Player player, DungeonMap map)
        {
            Player = player;
            Map = map;
            Children.Add(map);
            SadComponents.Add(new KeyboardComponent());

            IsVisible = true;
            IsFocused = true;
            Player.CalculateFOV();
        }

        public void ProcessTurn()
        {
            foreach (var entity in Map.Entities.Where(e => e.Item != Player))
            {
                System.Console.WriteLine($"The {((RogueLikeEntity)entity.Item).Name} wonders when it will get to take a real turn");
            }
        }
    }
}
