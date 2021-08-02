using System.IO;
using Newtonsoft.Json;
using SadConsole;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;
using TutorialRoguelike.EventHandlers;
using TutorialRoguelike.Serialization;
using TutorialRoguelike.UI;
using TutorialRoguelike.World;

namespace TutorialRoguelike
{
    public static class Initialization
    {
        public static Engine NewGame(int windowWidth, int windowHeight)
        {
            int roomMinSize = 6;
            int roomMaxSize = 10;
            int maxRooms = 30;

            int infoPanelWidth = windowWidth;
            int infoPanelHeight = 5;

            int dungeonWidth = windowWidth;
            int dungeonHeight = windowHeight - infoPanelHeight;

            var player = EntityFactory.Player();

            var startingConsole = (Console)GameHost.Instance.Screen;

            var infoConsole = new InfoPanel(infoPanelWidth, infoPanelHeight, player);
            infoConsole.Position = (0, dungeonHeight);
            startingConsole.Children.Add(infoConsole);

            var engine = new Engine(player, startingConsole, infoConsole);
            engine.World = new GameWorld(engine, dungeonWidth, dungeonHeight, maxRooms, roomMinSize, roomMaxSize);
            engine.World.GenerateFloor();

            startingConsole.IsFocused = true;
            startingConsole.SadComponents.Add(new InputHandler(new MainGameEventHandler(engine)));

            engine.MessageLog.Add("Hello and welcome, adventurer, to yet another dungeon", Colors.WelcomeText);

            var startingWeapon = EntityFactory.Dagger();
            var startingArmor = EntityFactory.LeatherArmor();

            player.Inventory.Items.Add(startingWeapon);
            startingWeapon.Parent = player.Inventory;
            player.Equipment.ToggleEquipment(startingWeapon, false);

            player.Inventory.Items.Add(startingArmor);
            startingArmor.Parent = player.Inventory;
            player.Equipment.ToggleEquipment(startingArmor, false);

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
            engine.World = savedEngine.World;
            engine.World.Engine = engine;

            console.IsFocused = true;
            console.SadComponents.Add(new InputHandler(new MainGameEventHandler(engine)));

            engine.MessageLog.Add("Hello and welcome, adventurer, to yet another dungeon", Colors.WelcomeText);

            engine.UpdateFov();
            engine.Render();

            return engine;
        }

        public static void DeleteGame(string filename = "savegame.sav")
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
    }
}
