using System.Linq;
using GoRogue.FOV;
using Newtonsoft.Json;
using SadConsole;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using TutorialRoguelike.Entities;
using TutorialRoguelike.Exceptions;
using TutorialRoguelike.UI;
using TutorialRoguelike.World;

namespace TutorialRoguelike
{
    public class Engine
    {
        public Actor Player;

        private GameMap _map;
        public GameMap Map { 
            get => _map; 
            set
            {
                _map = value;
                FOV = null;
            }
        }
        public GameWorld World;

        [JsonIgnore]
        public Console Console;
        [JsonIgnore]
        public InfoPanel InfoConsole;
        [JsonIgnore]
        public Point MouseLocation;
        public MessageLog MessageLog => InfoConsole.MessageLog;

        private IFOV FOV;


        public Engine(Actor player, Console console, InfoPanel infoConsole)
        {
            Player = player;
            Console = console;
            InfoConsole = infoConsole;
            MouseLocation = (0, 0);
        }

        public void Render()
        {
            Console.Clear();
            Map.Render(Console);
            InfoConsole.Render();
        }

        public void HandleEnemyTurns()
        {
            foreach (var actor in Map.Actors.Where(e => e != Player))
            {
                if (actor.IsAlive)
                {
                    try
                    {
                        actor.AI.Perform();
                    } catch (ImpossibleException) { }  //Ignore impossible action exceptions by the AI
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
