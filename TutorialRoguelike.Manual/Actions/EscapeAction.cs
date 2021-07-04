using SadConsole;
using TutorialRoguelike.Manual;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Actions
{
    public class EscapeAction : IAction
    {
        public void Perform(Engine engine, Entity entity)
        {
            Game.Instance.MonoGameInstance.Exit();
        }
    }
}
