using System.Collections.Generic;
using System.Linq;
using TutorialRoguelike.Entities;
using TutorialRoguelike.Terrain;

namespace TutorialRoguelike.Serialization
{
    public class GameMapSerializable
    {
        public GameMapSerializable(int width, int height, bool[] visible, TileType[] tiles, bool[] explored, IList<Entity> entities)
        {
            Width = width;
            Height = height;
            Visible = visible;
            Tiles = tiles;
            Explored = explored;
            Entities = entities;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool[] Visible { get; private set; }
        public TileType[] Tiles { get; private set; }
        public bool[] Explored { get; private set; }

        public IList<Entity> Entities { get; private set; }

        public static implicit operator GameMapSerializable(GameMap map)
        {
            return new GameMapSerializable(map.Width, map.Height, map.Visible.ToArray(), map.Tiles.ToArray().Select(t => t.TileType).ToArray(), map.Explored.ToArray(), map.Entities);
        }


    }
}
