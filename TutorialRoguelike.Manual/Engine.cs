using System.Collections.Generic;
using System.Linq;
using GoRogue.FOV;
using SadConsole;
using SadRogue.Primitives.GridViews;
using TutorialRoguelike.Manual.Actions;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual
{
    public class Engine
    {
        public Entity Player;
        public GameMap Map;
        public Console Console;

        private IFOV FOV;

        public Engine(Entity player, GameMap map, Console console)
        {
            Player = player;
            Map = map;
            Console = console;

            var transparentTiles = new ArrayView<bool>(Map.Tiles.ToArray().Select(t => t.IsTransparent).ToArray(), Map.Width);
            FOV = new RecursiveShadowcastingFOV(transparentTiles);
            UpdateFov();
        }

        public void Render()
        {
            Console.Clear();
            Map.Render(Console);
        }

        public void HandleAction(IAction action)
        {
            action.Perform(this, Player);
            HandleEnemyTurns();
            UpdateFov();
            Render();
        }

        private void HandleEnemyTurns()
        {
            foreach (var entity in Map.Entities.Where(e => e != Player))
            {
                System.Console.WriteLine($"The {entity.Name} wonders when it will get to take a real turn.");
            }
        }

        private void UpdateFov()
        {
            FOV.Calculate(Player.Position, 8);
            Map.Visible.ApplyOverlay(FOV.BooleanResultView);
            Map.Explored.ApplyOverlay(p => Map.Explored[p] | FOV.BooleanResultView[p]);
        }
    }
}
