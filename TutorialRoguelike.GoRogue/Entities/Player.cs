using SadRogue.Integration;
using SadRogue.Integration.Components;
using SadRogue.Primitives;
using SadConsole.Input;
using TutorialRoguelike.Actions;
using GoRogue.GameFramework;

namespace TutorialRoguelike.GoRogue.Entities
{
    public class Player : RogueLikeEntity
    {
        public int FOVRadius;
        public Player(Point position, int fovRadius = 8) : base(position, Colors.Player, (char)25, false)
        {
            FOVRadius = fovRadius;

            var motionControl = new PlayerControlsComponent();
            motionControl.AddKeyCommand(Keys.Escape, EscapeAction.Action);
            AllComponents.Add(motionControl);
            Moved += OnMoved;

            // Ensure player receives input
            IsFocused = true;
        }

        /// <summary>
        /// Calculate FOV if a player is part of a map.
        /// </summary>
        public void CalculateFOV()
        {
            CurrentMap?.PlayerFOV.Calculate(Position, FOVRadius, CurrentMap.DistanceMeasurement);
        }

        // If the player is added to a map, update the player FOV when the player moves
        private void OnMoved(object? sender, GameObjectPropertyChanged<Point> e)
        {
            CalculateFOV();
        }
    }
}
