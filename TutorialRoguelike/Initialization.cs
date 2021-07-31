using System.IO;
using Newtonsoft.Json;
using SadConsole;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;
using TutorialRoguelike.EventHandlers;
using TutorialRoguelike.MapGeneration;
using TutorialRoguelike.Serialization;

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

        private static JsonSerializerSettings SerializationSettings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static void SaveAs(Engine engine, string filename = "savegame.sav")
        {
            var serialized = JsonConvert.SerializeObject((EngineSerializable)engine, SerializationSettings);
            File.WriteAllText(filename, serialized);
        }

        public static Engine LoadGame(string filename = "savegame.sav")
        {
            var console = (Console)GameHost.Instance.Screen;

            var serialized = File.ReadAllText(filename);

            var savedEngine = JsonConvert.DeserializeObject<EngineSerializable>(serialized, SerializationSettings);

            var player = new Actor(savedEngine.Player);

            var infoConsole = new InfoPanel(Program.WindowWidth, 5, player);
            infoConsole.Position = (0, Program.WindowHeight - 5);
            console.Children.Add(infoConsole);

            var engine = new Engine(player, console, infoConsole);
            var map = new GameMap(savedEngine.Map, engine);
            player.Place(player.Position, map);
            engine.Map = map;

            console.IsFocused = true;
            console.SadComponents.Add(new InputHandler(new MainGameEventHandler(engine)));

            engine.MessageLog.Add("Hello and welcome, adventurer, to yet another dungeon", Colors.WelcomeText);

            engine.UpdateFov();
            engine.Render();

            return engine;
        }
    }
}
