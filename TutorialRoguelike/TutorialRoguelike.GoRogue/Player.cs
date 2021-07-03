using SadRogue.Integration;
using SadRogue.Integration.Components;
using SadRogue.Primitives;

namespace TutorialRoguelike
{
    public class Player : RogueLikeEntity
    {
        public Player(Point position) : base(position, '@', false)
        {
            var motionControl = new PlayerControlsComponent();
            AllComponents.Add(motionControl);

            // Ensure player receives input
            IsFocused = true;
        }
    }
}
