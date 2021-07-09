using SadRogue.Primitives;
using SadConsole;
using System.Collections.Generic;
using SadRogue.Primitives.GridViews;
using TutorialRoguelike.Manual.Entities;
using System.Linq;

namespace TutorialRoguelike.Manual
{
    public class GameMap
    {
        public int Width;
        public int Height;

        public ArrayView<Tile> Tiles;
        public ArrayView<bool> Visible;
        public ArrayView<bool> Explored;

        public ISet<Entity> Entities;

        public GameMap(Point size)
        {
            Width = size.X;
            Height = size.Y;
            Tiles = new ArrayView<Tile>(size.X, size.Y);
            Visible = new ArrayView<bool>(size.X, size.Y);
            Explored = new ArrayView<bool>(size.X, size.Y);
            Entities = new HashSet<Entity>();

            CloneFill(Tiles.Positions(), TileFactory.Wall);
            Visible.Fill(false);
            Explored.Fill(false);
        }

        public Entity GetBlockingEntityAt(Point position)
        {
            return Entities.FirstOrDefault(e => e.Position == position && e.BlocksMovement);
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

            foreach (var entity in Entities)
            {
                if (Visible[entity.Position.X, entity.Position.Y])
                {
                    //Force transparency by copying onto cloned map tile
                    var displayGlyph = Tiles[entity.Position].Glyph.Clone();
                    displayGlyph.Foreground = entity.Appearance.Foreground;
                    displayGlyph.Glyph = entity.Appearance.Glyph;
                    console.Print(entity.Position.X, entity.Position.Y, displayGlyph);
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

        public void CloneFill(IEnumerable<Point> area, Tile value)
        {
            foreach (var p in area)
            {
                Tiles[p.X, p.Y] = value.Clone();
            }
        }
    }
}
