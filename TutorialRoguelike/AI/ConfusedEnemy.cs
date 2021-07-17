using System;
using System.Linq;
using GoRogue.Random;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.AI
{
    /* A confused enemy will stumble around aimlessly for a given number of turns,
     * then revert back to its previous AI.
     * If an actor occupies a tile it is randomly moving into, it will attack. 
     */
    public class ConfusedEnemy : BaseAI
    {
        private int TurnsRemaining { get; set; }
        private BaseAI PreviousAI { get; set; }
        public ConfusedEnemy(Actor entity, int turnsRemaining) : base(entity)
        {
            TurnsRemaining = turnsRemaining;
            PreviousAI = entity.AI;
        }

        public override void Perform()
        {
            if (TurnsRemaining <= 0)
            {
                Engine.MessageLog.Add($"The {Entity.Name} is no longer confused.");
                Entity.AI = PreviousAI;
            } 
            else
            {
                var directions = Enum.GetValues(typeof(Direction.Types)).OfType<Direction.Types>().Where(d => d != Direction.Types.None).ToArray();
                var index = GlobalRandom.DefaultRNG.Next(directions.Length);
                var direction = directions[index];
                TurnsRemaining -= 1;

                // The actor will either try to move or attack in the chosen random direction.
                // It's possible the actor will just bump into the wall, wasting a turn.
                new BumpAction(Entity, direction).Perform();
            }
        }
    }
}
