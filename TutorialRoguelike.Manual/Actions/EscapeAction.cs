using SadConsole;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
{
    public class EscapeAction : GameAction
    {
        public EscapeAction(Entity entity) : base(entity)
        {
        }

        public override void Perform()
        {
            Game.Instance.MonoGameInstance.Exit();
        }
    }
}
