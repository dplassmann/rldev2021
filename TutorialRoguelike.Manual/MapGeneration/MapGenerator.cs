using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SadRogue.Primitives;
using GoRogue;
using GoRogue.Random;
using TutorialRoguelike.Manual.Entities;
using TutorialRoguelike.Manual.Terrain;

namespace TutorialRoguelike.Manual.MapGeneration
{
    public class MapGenerator
    {
        public static GameMap GenerateDungeon(int mapWidth, int mapHeight, int maxRooms, int roomMinSize, int roomMaxSize, int maxMonstersPerRoom, Engine engine)
        {
            var dungeon = new GameMap((mapWidth, mapHeight), engine);
            var rooms = new List<RectangularRoom>();

            for (int i = 0; i < maxRooms; i++)
            {
                var roomWidth = GlobalRandom.DefaultRNG.Next(roomMinSize, roomMaxSize);
                var roomHeight = GlobalRandom.DefaultRNG.Next(roomMinSize, roomMaxSize);

                var x = GlobalRandom.DefaultRNG.Next(0, dungeon.Width - roomWidth - 1);
                var y = GlobalRandom.DefaultRNG.Next(0, dungeon.Height - roomHeight - 1);

                var newRoom = new RectangularRoom(x, y, roomWidth, roomHeight);

                //Check all previous rooms for overlap. If any intersect, throw out the room and move on
                if (rooms.Any(r => newRoom.Intersects(r)))
                    continue;

                dungeon.CloneFill(newRoom.Inner, TileFactory.Floor);

                //If this is the first room, start the player here
                if (!rooms.Any())
                {
                    engine.Player.Place(newRoom.Center, dungeon);
                }
                else //For all other rooms, tunnel to the previous
                {
                    dungeon.CloneFill(TunnelBetween(newRoom.Center, rooms.Last().Center), TileFactory.Floor);
                }

                PlaceEntities(newRoom, dungeon, maxMonstersPerRoom);

                //Finally save the new room
                rooms.Add(newRoom);
            }
            return dungeon;
        }

        private static void PlaceEntities(RectangularRoom room, GameMap dungeon, int maxMonstersPerRoom)
        {
            int numberOfMonsters = GlobalRandom.DefaultRNG.Next(0, maxMonstersPerRoom+1);

            for (int i = 0; i < numberOfMonsters; i++)
            {
                var x = GlobalRandom.DefaultRNG.Next(room.MinExtent.X + 1, room.MaxExtent.X - 1);
                var y = GlobalRandom.DefaultRNG.Next(room.MinExtent.Y + 1, room.MaxExtent.Y - 1);
                var position = (x, y);

                if (!dungeon.Entities.Any(e => e.Position == position))
                {
                    if (GlobalRandom.DefaultRNG.NextDouble() < 0.8)
                    {
                        EntityFactory.Orc.Place(position, dungeon);
                        continue;
                    }
                    else
                    {
                        EntityFactory.Troll.Place(position, dungeon);
                        continue;
                    }
                }
            }
        }

        private static IEnumerable<Point> TunnelBetween(Point start, Point end)
        {
            Point corner;

            //randomly decide movement direction
            corner = (GlobalRandom.DefaultRNG.NextDouble() < 0.5) ? (end.X, start.Y) : (start.X, end.Y);

            foreach (var p in Lines.Get(start, corner))
            {
                yield return p;
            }
            foreach (var p in Lines.Get(corner, end))
            {
                yield return p;
            }
        }
    }
}
