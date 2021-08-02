using System.Collections.Generic;
using System.Linq;
using SadRogue.Primitives;
using GoRogue;
using GoRogue.Random;
using TutorialRoguelike.Entities;
using TutorialRoguelike.Terrain;
using System;

namespace TutorialRoguelike.World
{
    public static class MapGenerator
    {
        public static (int, int)[] MaxItemsByFloor = {
            (1, 1),
            (4, 2),
        };

        public static (int, int)[] MaxMonstersByFloor = {
            (1, 2),
            (4, 3),
            (6, 5),
        };

        public static Dictionary<int, List<(Func<Entity>, int)>> ItemChances = new Dictionary<int, List<(Func<Entity>, int)>>{
            {0, new List<(Func<Entity>, int)> {(EntityFactory.HealthPotion, 35)} },
            {2, new List<(Func<Entity>, int)> {(EntityFactory.ConfusionScroll, 10)} },
            {4, new List<(Func<Entity>, int)> {(EntityFactory.LightningScroll, 25), (EntityFactory.Sword, 5)} },
            {6, new List<(Func<Entity>, int)> {(EntityFactory.FireballScroll, 25), (EntityFactory.ChainMail, 15)} },
        };

        public static Dictionary<int, List<(Func<Entity>, int)>> EnemyChances = new Dictionary<int, List<(Func<Entity>, int)>>{
            {0, new List<(Func<Entity>, int)> {(EntityFactory.Orc, 80)} },
            {3, new List<(Func<Entity>, int)> {(EntityFactory.Troll, 15)} },
            {5, new List<(Func<Entity>, int)> {(EntityFactory.Troll, 30)} },
            {7, new List<(Func<Entity>, int)> {(EntityFactory.Troll, 60)} },
        };

        public static GameMap GenerateDungeon(int mapWidth, int mapHeight, int maxRooms, int roomMinSize, int roomMaxSize, Engine engine)
        {
            var map = new GameMap((mapWidth, mapHeight), engine);
            var rooms = new List<RectangularRoom>();

            for (int i = 0; i < maxRooms; i++)
            {
                var roomWidth = GlobalRandom.DefaultRNG.Next(roomMinSize, roomMaxSize);
                var roomHeight = GlobalRandom.DefaultRNG.Next(roomMinSize, roomMaxSize);

                var x = GlobalRandom.DefaultRNG.Next(0, map.Width - roomWidth - 1);
                var y = GlobalRandom.DefaultRNG.Next(0, map.Height - roomHeight - 1);

                var newRoom = new RectangularRoom(x, y, roomWidth, roomHeight);

                //Check all previous rooms for overlap. If any intersect, throw out the room and move on
                if (rooms.Any(r => newRoom.Intersects(r)))
                    continue;

                map.CloneFill(newRoom.Inner, TileFactory.Floor);

                //If this is the first room, start the player here
                if (!rooms.Any())
                {
                    engine.Player.Place(newRoom.Center, map);
                }
                else //For all other rooms, tunnel to the previous
                {
                    map.CloneFill(TunnelBetween(newRoom.Center, rooms.Last().Center), TileFactory.Floor);
                }

                PlaceEntities(newRoom, map, engine.World.CurrentFloor);

                //Finally save the new room
                rooms.Add(newRoom);
            }

            map.DownStairsLocation = rooms.Last().Center;
            map.Tiles[map.DownStairsLocation] = TileFactory.DownStairs;

            return map;
        }

        private static void PlaceEntities(RectangularRoom room, GameMap dungeon, int floor)
        {
            int numberOfMonsters = GlobalRandom.DefaultRNG.Next(0, GetMaxValueForFloor(MaxMonstersByFloor, floor) + 1);
            int numberOfItems = GlobalRandom.DefaultRNG.Next(0, GetMaxValueForFloor(MaxItemsByFloor, floor) + 1);

            var monsters = GetEntitiesAtRandom(EnemyChances, numberOfMonsters, floor);
            var items = GetEntitiesAtRandom(ItemChances, numberOfItems, floor);

            foreach (var entity in monsters.Union(items))
            {
                var x = GlobalRandom.DefaultRNG.Next(room.MinExtent.X + 1, room.MaxExtent.X - 1);
                var y = GlobalRandom.DefaultRNG.Next(room.MinExtent.Y + 1, room.MaxExtent.Y - 1);
                var position = (x, y);

                if (!dungeon.Entities.Any(e => e.Position == position))
                {
                    entity.Place(position, dungeon);
                }
            }
        }

        private static IEnumerable<Point> TunnelBetween(Point start, Point end)
        {
            Point corner;

            //randomly decide movement direction
            corner = GlobalRandom.DefaultRNG.NextDouble() < 0.5 ? (end.X, start.Y) : (start.X, end.Y);

            foreach (var p in Lines.Get(start, corner))
            {
                yield return p;
            }
            foreach (var p in Lines.Get(corner, end))
            {
                yield return p;
            }
        }

        private static int GetMaxValueForFloor((int, int)[] maxValueByFloor, int floor)
        {
            var currentVal = 0;
            foreach ((var minFloor, var value) in maxValueByFloor)
            {
                if (minFloor > floor)
                    break;
                else
                    currentVal = value;
            }
            return currentVal;
        }

        private static List<Entity> GetEntitiesAtRandom(Dictionary<int, List<(Func<Entity>, int)>> weightedChancesByFloor, int numberOfEntities, int floor)
        {
            var entityWeightedChances = new Dictionary<Func<Entity>, int>();

            // Condense floor-by-floor additions of weighted entities to single map of entities to weights
            foreach ((var minFloor, var values) in weightedChancesByFloor)
            {
                if (minFloor > floor)
                    break;
                else
                {
                    foreach ((var entity, var weight) in values)
                    {
                        entityWeightedChances[entity] = weight;
                    }
                }
            }

            // Choose n values from that map.
            // For each choice, will get random number from 0 to total of weights
            // Walk array of (threshold, entity) pairs until you find lower, that's your choice

            var thresholdValues = new (int, Func<Entity>)[entityWeightedChances.Count];
            int i = 0;
            int sumOfWeights = 0;
            foreach ((var entity, var weight) in entityWeightedChances)
            {
                thresholdValues[i] = (sumOfWeights, entity);
                sumOfWeights += weight;
                i++;
            }

            var entities = new List<Entity>();

            for (int choice = 0; choice < numberOfEntities; choice++)
            {
                var rand = GlobalRandom.DefaultRNG.Next(sumOfWeights);
                Func<Entity> currentEntity = null;

                foreach ((var threshold, var entity) in thresholdValues)
                {
                    if (threshold > rand)
                        break;
                    else
                        currentEntity = entity;
                }
                entities.Add(currentEntity.Invoke());
            }
            return entities;
        }
    }
}
