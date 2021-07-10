﻿using System.Linq;
using GoRogue.FOV;
using SadConsole;
using SadRogue.Primitives.GridViews;
using TutorialRoguelike.Manual.Entities;
using TutorialRoguelike.Manual.EventHandlers;

namespace TutorialRoguelike.Manual
{
    public class Engine
    {
        public Actor Player;
        public GameMap Map;
        public Console Console;
        public Console InfoConsole;
        public EventHandler EventHandler;

        private IFOV FOV;

        public Engine(Actor player, Console console, Console infoConsole)
        {
            Player = player;
            Console = console;
            InfoConsole = infoConsole;
            EventHandler = new MainGameEventHandler(this);
        }

        public void Render()
        {
            Console.Clear();
            Map.Render(Console);
            InfoConsole.Print(1, 0, $"HP: {Player.Fighter.Hp}/{Player.Fighter.MaxHp}");
        }

        public void HandleEnemyTurns()
        {
            foreach (var actor in Map.Actors.Where(e => e != Player))
            {
                if (actor.AI != null)
                {
                    actor.AI.Perform();
                }
            }
        }

        public void UpdateFov()
        {
            if (FOV == null)
            {
                var transparentTiles = new ArrayView<bool>(Map.Tiles.ToArray().Select(t => t.IsTransparent).ToArray(), Map.Width);
                FOV = new RecursiveShadowcastingFOV(transparentTiles);
            }
            FOV.Calculate(Player.Position, 8);
            Map.Visible.ApplyOverlay(FOV.BooleanResultView);
            Map.Explored.ApplyOverlay(p => Map.Explored[p] | FOV.BooleanResultView[p]);
        }
    }
}
