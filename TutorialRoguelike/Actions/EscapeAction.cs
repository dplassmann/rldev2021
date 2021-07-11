using SadConsole;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Actions
{
    public class EscapeAction : BaseAction
    {
        public EscapeAction(Actor entity) : base(entity)
        {
        }

        public override void Perform()
        {
            Game.Instance.MonoGameInstance.Exit();
        }
    }
}
