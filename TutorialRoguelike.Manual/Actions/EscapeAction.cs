﻿using SadConsole;
using TutorialRoguelike.Manual.Entities;

namespace TutorialRoguelike.Manual.Actions
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
