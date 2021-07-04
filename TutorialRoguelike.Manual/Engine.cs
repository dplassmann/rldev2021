using System.Collections.Generic;
using SadConsole;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual
{
    public class Engine
    {
        public ISet<Entity> Entities;
        public Entity Player;
        public GameMap Map;

        public Engine(ISet<Entity> entities, Entity player, GameMap map)
        {
            Entities = entities;
            Player = player;
            Map = map;
        }
        public void Render(Console console)
        {
            console.Clear();
            Map.Render(console);
            foreach (var entity in Entities)
            {
                //Force transparency by copying onto cloned map tile
                var displayGlyph = Map[entity.Position].Glyph.Clone();
                displayGlyph.Foreground = entity.Appearance.Foreground;
                displayGlyph.Glyph = entity.Appearance.Glyph;
                console.Print(entity.Position.X, entity.Position.Y, displayGlyph);
            }
        }

        public void HandleAction(IAction action)
        {
            action.Perform(this, Player);
        }
    }
}
