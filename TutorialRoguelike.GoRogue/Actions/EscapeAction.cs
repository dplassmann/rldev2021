using System;
using SadConsole;

namespace TutorialRoguelike.Actions
{
    public class EscapeAction : IAction
    {
        public static Action Action = Game.Instance.MonoGameInstance.Exit;
    }
}
