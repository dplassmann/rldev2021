﻿using System;
using System.Linq;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.EventHandlers
{
    public class LevelUpEventHandler : AskUserEventHandler
    {
        private const string Title = "Level Up";
        private SadConsole.Console Console { get; set; }

        public LevelUpEventHandler(Engine engine) : base(engine)
        {
            var width = 35;
            var height = 8;
            var x = Engine.Player.Position.X <= 30 ? 80 : 0;
            var y = 0;

            Console = new SadConsole.Console(width, height);
            Console.Font = Game.Instance.EmbeddedFont;
            Console.Position = (x, y);
            Engine.Console.Children.Add(Console);

            Console.DrawBox(new Rectangle(0, 0, width, height), new ColoredGlyph(Color.White, Color.Black), new ColoredGlyph(Color.White, Color.Black), ICellSurface.ConnectedLineThin);
            Console.Print(1, 0, Title.Align(HorizontalAlignment.Center, width - 2, (char)ICellSurface.ConnectedLineThin[(int)ICellSurface.ConnectedLineIndex.Top]), Color.White, Color.Black);

            Console.Print(1, 1, "Congratulations! You level up!");
            Console.Print(1, 2, "Select an attribute to increase.");
            Console.Print(1, 4, $"a) Constitution (+20 HP, from {Engine.Player.Fighter.MaxHp})");
            Console.Print(1, 5, $"b) Strength (+1 attack, from {Engine.Player.Fighter.Power})");
            Console.Print(1, 6, $"c) Agility (+1 defense, from {Engine.Player.Fighter.Defense})");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Console.Parent = null;
        }

        public override IActionOrEventHandler ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            if (keyboard.HasKeysPressed)
            {
                var index = keyboard.KeysPressed.FirstOrDefault().Character - 'a';
                if (0 <= index && index <= 2)
                {
                    switch (index)
                    {
                        case 0:
                            Engine.Player.Level.IncreaseMaxHp();
                            break;
                        case 1:
                            Engine.Player.Level.IncreasePower();
                            break;
                        case 2:
                            Engine.Player.Level.IncreaseDefense();
                            break;
                    }
                }
                else
                {
                    Engine.MessageLog.Add("Invaid entry.", Colors.Invalid);
                    return null;
                }
            }

            return base.ProcessKeyboard(host, keyboard);
        }

        // Don't allow the player to click to exit the menu like normal
        public override IActionOrEventHandler ProcessMouse(IScreenObject host, MouseScreenObjectState state)
        {
            return null;
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
