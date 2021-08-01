using SadRogue.Primitives;
using SadConsole;
using System.Collections.Generic;
using SadRogue.Primitives.GridViews;
using TutorialRoguelike.Entities;
using System.Linq;
using TutorialRoguelike.Terrain;
using Newtonsoft.Json;
using TutorialRoguelike.Serialization;

namespace TutorialRoguelike.World
{
    public class GameMap : EntityContainer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public ArrayView<Tile> Tiles { get; private set; }
        public ArrayView<bool> Visible { get; private set; }
        public ArrayView<bool> Explored { get; private set; }

        private bool _isWalkableDirty = true;
        private ArrayView<bool> _walkable;
        public ArrayView<bool> Walkable
        {
            get
            {
                if (_isWalkableDirty)
                {
                    _walkable = new ArrayView<bool>(Tiles.ToArray().Select(t => t.IsWalkable).ToArray(), Width);
                    _isWalkableDirty = false;
                }
                return _walkable;
            }
        }

        public IList<Entity> Entities { get; private set; }

        public Point DownStairsLocation { get; set; }

        public Engine Engine { get; private set; }
        public IEnumerable<Actor> Actors => Entities.Where(e => e is Actor actor && actor.IsAlive).Cast<Actor>();
        public IEnumerable<Item> Items => Entities.Where(e => e is Item).Cast<Item>();
        public GameMap Map => this;

        [JsonConstructor]
        public GameMap(Point size, Engine engine)
        {
            Width = size.X;
            Height = size.Y;
            Tiles = new ArrayView<Tile>(size.X, size.Y);
            Visible = new ArrayView<bool>(size.X, size.Y);
            Explored = new ArrayView<bool>(size.X, size.Y);
            Entities = new List<Entity>();
            Engine = engine;

            CloneFill(Tiles.Positions(), TileFactory.Wall);
            Visible.Fill(false);
            Explored.Fill(false);
        }

        public GameMap(GameMapSerializable serializableMap, Engine engine)
        {
            Width = serializableMap.Width;
            Height = serializableMap.Height;
            Tiles = new ArrayView<Tile>(serializableMap.Tiles.Select(t => TileFactory.Get(t)).ToArray(), Width);
            Visible = new ArrayView<bool>(serializableMap.Visible, Width);
            Explored = new ArrayView<bool>(serializableMap.Explored, Width);
            Entities = new List<Entity>();
            DownStairsLocation = serializableMap.DownStairsLocation;
            foreach (var e in serializableMap.Entities.Where(e => e.Name != "Player"))
            {
                e.Place(e.Position, this);
                var actor = e as Actor;
                if (actor != null && actor.IsAlive)
                {
                    actor.AI.Entity = actor;
                }
            }
            Engine = engine;
        }

        public Entity GetBlockingEntityAt(Point position)
        {
            return Entities.FirstOrDefault(e => e.Position == position && e.BlocksMovement);
        }

        public Actor GetActorAt(Point position)
        {
            return Actors.FirstOrDefault(e => e.Position == position);
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

            foreach (var entity in Entities.OrderBy(e => e.RenderOrder))
            {
                if (Visible[entity.Position.X, entity.Position.Y])
                {
                    console.Print(entity.Position.X, entity.Position.Y, entity.Appearance.GlyphCharacter.ToString(), entity.Appearance.Foreground);
                }
            }
        }

        public Tile this[Point position]
        {
            get { return Tiles[position.X, position.Y]; }
            set
            {
                Tiles[position.X, position.Y] = value;
                _isWalkableDirty = true;
            }
        }
        public Tile this[int x, int y]
        {
            get { return Tiles[x, y]; }
            set
            {
                Tiles[x, y] = value;
                _isWalkableDirty = true;
            }
        }

        public void CloneFill(IEnumerable<Point> area, Tile value)
        {
            foreach (var p in area)
            {
                Tiles[p.X, p.Y] = value.Clone();
                _isWalkableDirty = true;
            }
        }
    }
}
