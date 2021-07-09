using System.Collections.Generic;
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

            var transparentTiles = new ArrayView<bool>(Map.Width, Map.Height);
            for (int i = 0; i < Map.Width; i++)
            {
                for (int j = 0; j < Map.Height; j++)
                {
                    transparentTiles[i, j] = Map[i, j].IsTransparent;
                }
            }

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
            Map.Visible = new bool[Map.Width, Map.Height];
            foreach (var p in FOV.BooleanResultView.Positions())
            {
                Map.Visible[p.X, p.Y] = FOV.BooleanResultView[p];
                Map.Explored[p.X, p.Y] |= FOV.BooleanResultView[p];
            }
        }
    }
}
