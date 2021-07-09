using System.Collections.Generic;
using System.Linq;
using GoRogue.FOV;
using SadConsole;
using SadRogue.Primitives.GridViews;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual
{
    public class Engine
    {
        public ISet<Entity> Entities;
        public Entity Player;
        public GameMap Map;

        private IFOV FOV;

        public Engine(ISet<Entity> entities, Entity player, GameMap map)
        {
            Entities = entities;
            Player = player;
            Map = map;

            var transparentTiles = new ArrayView<bool>(Map.Tiles.ToArray().Select(t => t.IsTransparent).ToArray(), Map.Width);
            FOV = new RecursiveShadowcastingFOV(transparentTiles);
            UpdateFov();
        }

        public void Update()
        {
            UpdateFov();
        }

        public void Render(Console console)
        {
            console.Clear();
            Map.Render(console);
            foreach (var entity in Entities)
            {
                if (Map.Visible[entity.Position.X, entity.Position.Y])
                {
                    //Force transparency by copying onto cloned map tile
                    var displayGlyph = Map[entity.Position].Glyph.Clone();
                    displayGlyph.Foreground = entity.Appearance.Foreground;
                    displayGlyph.Glyph = entity.Appearance.Glyph;
                    console.Print(entity.Position.X, entity.Position.Y, displayGlyph);
                }
            }
        }

        public void HandleAction(IAction action)
        {
            action.Perform(this, Player);
        }

        private void UpdateFov()
        {
            FOV.Calculate(Player.Position, 8);
            Map.Visible.ApplyOverlay(FOV.BooleanResultView);
            Map.Explored.ApplyOverlay(p => Map.Explored[p] | FOV.BooleanResultView[p]);
        }
    }
}
