using SadConsole;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;
using TutorialRoguelike.EventHandlers;
using TutorialRoguelike.MapGeneration;

namespace TutorialRoguelike
{
    public static class Initialization
    {
        public static Engine NewGame(int windowWidth, int windowHeight)
        {
            int roomMinSize = 6;
            int roomMaxSize = 10;
            int maxRooms = 30;
            int maxMonstersPerRoom = 2;
            int maxItemsPerRoom = 2;

            int infoPanelWidth = windowWidth;
            int infoPanelHeight = 5;

            int dungeonWidth = windowWidth;
            int dungeonHeight = windowHeight - infoPanelHeight;

            var player = EntityFactory.Player;

            var startingConsole = (Console)GameHost.Instance.Screen;

            var infoConsole = new InfoPanel(infoPanelWidth, infoPanelHeight, player);
            infoConsole.Position = (0, dungeonHeight);
            startingConsole.Children.Add(infoConsole);

            var engine = new Engine(player, startingConsole, infoConsole);
            engine.Map = MapGenerator.GenerateDungeon(dungeonWidth, dungeonHeight, maxRooms, roomMinSize, roomMaxSize, maxMonstersPerRoom, maxItemsPerRoom, engine);

            startingConsole.IsFocused = true;
            startingConsole.SadComponents.Add(new InputHandler(new MainGameEventHandler(engine)));

            engine.MessageLog.Add("Hello and welcome, adventurer, to yet another dungeon", Colors.WelcomeText);

            engine.UpdateFov();
            engine.Render();

            return engine;
        }
    }
}
