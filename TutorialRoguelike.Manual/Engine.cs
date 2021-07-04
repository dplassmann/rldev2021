using System.Collections.Generic;
using SadConsole;
using TutorialRoguelike.Actions;

namespace TutorialRoguelike.Manual
{
    public class Engine
    {
        public ISet<Entity> Entities;
        public Entity Player;

        public Engine(ISet<Entity> entities, Entity player)
        {
            Entities = entities;
            Player = player;
        }
        public void Render(Console console)
        {
            console.Clear();
            foreach (var entity in Entities)
            {
                console.Print(entity.Position.X, entity.Position.Y, entity.Glyph);
            }
        }

        public void HandleAction(IAction action)
        {
            if (action is EscapeAction)
            {
                Game.Instance.MonoGameInstance.Exit();
            }
            if (action is MovementAction move)
            {
                Player.Move(move.Delta);
            }
        }
    }
}
