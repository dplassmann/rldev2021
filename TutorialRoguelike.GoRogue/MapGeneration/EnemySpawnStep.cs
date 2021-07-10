using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoRogue.MapGeneration;
using GoRogue.Random;
using SadRogue.Integration;
using TutorialRoguelike.GoRogue.Entities;

namespace TutorialRoguelike.GoRogue.MapGeneration
{
    public class EnemySpawnStep : GenerationStep
    {
        int MaxMonstersPerRoom;

        public EnemySpawnStep(int maxMonstersPerRoom) : base(null, (typeof(List<RectangularRoom>), "Rooms"))
        {
            MaxMonstersPerRoom = maxMonstersPerRoom;
        }

        protected override IEnumerator<object> OnPerform(GenerationContext context)
        {
            var rooms = context.GetFirst<List<RectangularRoom>>("Rooms");
            var entities = context.GetFirstOrNew(() => new HashSet<RogueLikeEntity>(),"Entities");

            foreach (var room in rooms)
            {
                int numberOfMonsters = GlobalRandom.DefaultRNG.Next(0, MaxMonstersPerRoom + 1);

                for (int i = 0; i < numberOfMonsters; i++)
                {
                    var x = GlobalRandom.DefaultRNG.Next(room.Interior.MinExtent.X + 1, room.Interior.MaxExtent.X - 1);
                    var y = GlobalRandom.DefaultRNG.Next(room.Interior.MinExtent.Y + 1, room.Interior.MaxExtent.Y - 1);
                    var position = (x, y);

                    if (!entities.Any(e => e.Position == position))
                    {
                        if (GlobalRandom.DefaultRNG.NextDouble() < 0.8)
                        {
                            entities.Add(EntityFactory.Orc(position));
                            continue;
                        }
                        else
                        {
                            entities.Add(EntityFactory.Troll(position));
                            continue;
                        }
                    }
                }
            }

            yield return null;
        }
    }
}
