using System;
using System.Collections.Generic;
using System.Linq;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using GoRogue.Pathing;
using TutorialRoguelike.Manual.Actions;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Components.AI
{
    public class BaseAI : BaseComponent, IAction
    {
        Actor IAction.Entity => (Actor) Entity;

        public BaseAI(Actor entity) : base(entity)
        {
        }

        public virtual void Perform()
        {
            throw new NotImplementedException();
        }

        //Compute and return a path to the target position.
        //If there is no valid path, returns and empty list
        public IEnumerable<Point> GetPathTo(Point dest)
        {
            var walkability =Entity.Map.Walkable;

            var weights = new ArrayView<double>(Entity.Map.Width, Entity.Map.Height);
            weights.Fill(1);
            foreach (var entity in Entity.Map.Entities.Where(e => e.BlocksMovement))
            {
                // Multiply the cost of a blocked position.
                // A lower number means more enemies will crowd behind each other in
                // hallways.  A higher number means enemies will take longer paths in
                // order to surround the player.
                weights[entity.Position] = 10;
            }

            var pathfinder = new AStar(walkability, Distance.Chebyshev, weights, 1);
            var path = pathfinder.ShortestPath(Entity.Position, dest);

            return path?.Steps ?? new List<Point>();
        }
    }
}
