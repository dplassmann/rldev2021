using SadRogue.Primitives;
using SadConsole;

namespace TutorialRoguelike.Manual
{
    public class GameMap
    {
        public Point Size;
        public Tile[,] Tiles;

        public GameMap(Point size)
        {
            Size = size;
            Tiles = new Tile[size.X, size.Y];
            for (int i = 0; i < size.X; i++)
            {
                for (int j = 0; j < size.Y; j++)
                {
                    Tiles[i,j] = TileFactory.Floor;
                    
                }
            }
            for (int i = 30; i <= 33; i++)
            {
                Tiles[i, 22] = TileFactory.Wall;
            }
        }

        public bool InBounds(Point position)
        {
            return 0 <= position.X && position.X <= Size.X
                && 0 <= position.Y && position.Y <= Size.Y;
        }

        public void Render(Console console)
        {
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    console.Print(i, j, Tiles[i, j].Glyph);
                }
            }
        }

        public Tile this[Point position] => Tiles[position.X, position.Y];
        public Tile this[int x, int y] => Tiles[x,y];
    }
}
