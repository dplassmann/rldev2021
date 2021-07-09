using SadRogue.Primitives;
using SadConsole;
using System.Collections.Generic;

namespace TutorialRoguelike.Manual
{
    public class GameMap
    {
        public int Width;
        public int Height;

        public Tile[,] Tiles;
        public bool[,] Visible;
        public bool[,] Explored;

        public GameMap(Point size)
        {
            Width = size.X;
            Height = size.Y;
            Tiles = new Tile[size.X, size.Y];
            Visible = new bool[size.X, size.Y];
            Explored = new bool[size.X, size.Y];
            for (int i = 0; i < size.X; i++)
            {
                for (int j = 0; j < size.Y; j++)
                {
                    Visible[i, j] = false;
                    Explored[i, j] = false;
                    Tiles[i,j] = TileFactory.Wall;
                }
            }
        }

        public bool InBounds(Point position)
        {
            return 0 <= position.X && position.X <= Width
                && 0 <= position.Y && position.Y <= Height;
        }

        public void Render(Console console)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (Visible[i, j])
                        console.Print(i, j, Tiles[i, j].Glyph);
                    else if (Explored[i, j])
                        console.Print(i, j, Tiles[i, j].DarkGlyph);
                }
            }
        }

        public Tile this[Point position]
        {
            get { return Tiles[position.X, position.Y]; }
            set { Tiles[position.X, position.Y] = value; }
        }
        public Tile this[int x, int y]
        {
            get { return Tiles[x, y]; }
            set { Tiles[x, y] = value; }
        }

        public void Fill(IEnumerable<Point> area, Tile value)
        {
            foreach (var p in area)
            {
                Tiles[p.X, p.Y] = value.Clone();
            }
        }
    }
}
