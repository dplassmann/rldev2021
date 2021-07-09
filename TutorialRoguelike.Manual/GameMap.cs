using SadRogue.Primitives;
using SadConsole;
using System.Collections.Generic;
using SadRogue.Primitives.GridViews;

namespace TutorialRoguelike.Manual
{
    public class GameMap
    {
        public int Width;
        public int Height;

        public ArrayView<Tile> Tiles;
        public ArrayView<bool> Visible;
        public ArrayView<bool> Explored;

        public GameMap(Point size)
        {
            Width = size.X;
            Height = size.Y;
            Tiles = new ArrayView<Tile>(size.X, size.Y);
            Visible = new ArrayView<bool>(size.X, size.Y);
            Explored = new ArrayView<bool>(size.X, size.Y);

            CloneFill(Tiles.Positions(), TileFactory.Wall);
            Visible.Fill(false);
            Explored.Fill(false);
        }

        public bool InBounds(Point position)
        {
            return 0 <= position.X && position.X <= Width
                && 0 <= position.Y && position.Y <= Height;
        }

        public void Render(Console console)
        {
            foreach (Point p in Tiles.Positions())
            {
                if (Visible[p])
                    console.Print(p.X, p.Y, Tiles[p].Glyph);
                else if (Explored[p])
                    console.Print(p.X, p.Y, Tiles[p].DarkGlyph);
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

        public void CloneFill(IEnumerable<Point> area, Tile value)
        {
            foreach (var p in area)
            {
                Tiles[p.X, p.Y] = value.Clone();
            }
        }
    }
}
