using SadRogue.Integration;
using SadRogue.Integration.Components;
using SadRogue.Primitives;
using SadConsole.Input;
using TutorialRoguelike.Actions;

namespace TutorialRoguelike.GoRogue
{
    public class Player : RogueLikeEntity
    {
        public Player(Point position) : base(position, Colors.Player, (char)25, false)
        {
            var motionControl = new PlayerControlsComponent();
            motionControl.AddKeyCommand(Keys.Escape, EscapeAction.Action);
            AllComponents.Add(motionControl);

            // Ensure player receives input
            IsFocused = true;
        }
    }
}
