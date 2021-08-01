using Newtonsoft.Json;

namespace TutorialRoguelike.World
{
    public class GameWorld
    {
        [JsonIgnore]
        public Engine Engine { get; set; }

        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public int MaxRooms { get; private set; }
        public int RoomMinSize { get; private set; }
        public int RoomMaxSize { get; private set; }
        
        public int CurrentFloor { get; private set; }

        public GameWorld(Engine engine, int mapWidth, int mapHeight, int maxRooms, int roomMinSize, int roomMaxSize, int currentFloor = 0)
        {
            Engine = engine;
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            MaxRooms = maxRooms;
            RoomMinSize = roomMinSize;
            RoomMaxSize = roomMaxSize;
            CurrentFloor = currentFloor;
        }

        public void GenerateFloor()
        {
            CurrentFloor += 1;
            Engine.Map = MapGenerator.GenerateDungeon(MapWidth, MapHeight, MaxRooms, RoomMinSize, RoomMaxSize, Engine);
        }
    }
}
