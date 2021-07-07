using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SadRogue.Primitives;
using GoRogue;
using GoRogue.Random;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.MapGeneration
{
    public class MapGenerator
    {
        public static GameMap GenerateDungeon(int mapWidth, int mapHeight, int maxRooms, int roomMinSize, int roomMaxSize, Player player)
        {
            var dungeon = new GameMap((mapWidth, mapHeight));
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

                dungeon.Fill(newRoom.Inner, TileFactory.Floor);

                //If this is the first room, start the player here
                if (!rooms.Any())
                {
                    player.Position = newRoom.Center;
                }
                else //For all other rooms, tunnel to the previous
                {
                    dungeon.Fill(TunnelBetween(newRoom.Center, rooms.Last().Center), TileFactory.Floor);
                }

                //Finally save the new room
                rooms.Add(newRoom);
            }

            return dungeon;
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
