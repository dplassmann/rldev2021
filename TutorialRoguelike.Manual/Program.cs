﻿using System.Collections.Generic;
using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Manual.Entities;
using Console = SadConsole.Console;

namespace TutorialRoguelike.Manual
{
    class Program
    {
        public const int Width = 80;
        public const int Height = 50;

        public static Engine Engine;

        static void Main(string[] args)
        {
            Game.Create(Width, Height);
            Settings.WindowTitle = "Yet Another Roguelike Tutorial - Manual Version";
            Game.Instance.OnStart = Init;
            Game.Instance.FrameUpdate += Instance_FrameUpdate;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Instance_FrameUpdate(object sender, GameHost e)
        {
            Engine.Render((Console) Game.Instance.Screen);
        }

        private static void Init()
        {
            var player = new Player((Width / 2, Height / 2));
            var npc = new Entity((Width / 2 - 5, Height / 2), 'n', Colors.Npc);
            var entities = new HashSet<Entity> { player, npc };
            var map = new GameMap((Width, Height));

            Engine = new Engine(entities, player, map);

            var startingConsole = (Console)GameHost.Instance.Screen;
            startingConsole.IsFocused = true;
            startingConsole.SadComponents.Add(new KeyboardHandler(Engine.HandleAction));
        }
    }
}
