using SadRogue.Integration;
using SadRogue.Integration.Components;
using SadRogue.Primitives;
using System.Linq;
using GoRogue.GameFramework;

namespace TutorialRoguelike.GoRogue.Entities
{
    public class Player : RogueLikeEntity
    {
        public int FOVRadius;
        public Player(Point position, int fovRadius = 8) : base(position, Colors.Player, (char)25, false)
        {
            FOVRadius = fovRadius;
            Moved += OnMoved;
        }

        /// <summary>
        /// Calculate FOV if a player is part of a map.
        /// </summary>
        public void CalculateFOV()
        {
            CurrentMap?.PlayerFOV.Calculate(Position, FOVRadius, CurrentMap.DistanceMeasurement);
        }

        // Handle move command to a direction.
        // If able to move (or attack), do so and return true so we can run the rest of the turn.
        // If unable to move due to wall or other blockage, return false so time doesn't pass.
        public bool Move(Direction direction)
        {
            Point destination = Position + direction;
            if (CurrentMap == null)
            {
                //Can't move if we aren't in the world
                return false;
            }


            if (CurrentMap.Entities.Any(e => e.Position == destination))
            {
                var target = CurrentMap.Entities.FirstOrDefault(e => e.Position == destination);
                System.Console.WriteLine($"**You kick the {((RogueLikeEntity)target.Item).Name}, much to its annoyance.");
                return true;
            }

            if (CurrentMap.WalkabilityView[destination])
            {
                Position = destination;
                return true;
            }

            return false;
        }

        // If the player is added to a map, update the player FOV when the player moves
        private void OnMoved(object? sender, GameObjectPropertyChanged<Point> e)
        {
            CalculateFOV();
        }
    }
}
