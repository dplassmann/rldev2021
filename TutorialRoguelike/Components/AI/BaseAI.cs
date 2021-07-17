using System;
using System.Collections.Generic;
using System.Linq;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using GoRogue.Pathing;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Components.AI
{
    public class BaseAI : IAction
    {
        public Actor Entity { get; private set; }

        public Engine Engine => Entity.Map.Engine;

        public BaseAI(Actor entity)
        {
            Entity = entity;
        }

        public virtual void Perform()
        {
            throw new NotImplementedException();
        }

        //Compute and return a path to the target position.
        //If there is no valid path, returns and empty list
        public IEnumerable<Point> GetPathTo(Point dest)
        {
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

            var pathfinder = new AStar(Entity.Map.Walkable, Distance.Chebyshev, weights, 1);
            var path = pathfinder.ShortestPath(Entity.Position, dest);

            return path?.Steps ?? new List<Point>();
        }
    }
}
