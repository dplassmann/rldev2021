﻿using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;
using SadRogue.Integration.Maps;

namespace TutorialRoguelike
{
    class Program
    {
        public const int Width = 80;
        public const int Height = 50;

        private static Player Player;
        private static RogueLikeMap Map;

        static void Main(string[] args)
        {
            Game.Create(Width, Height);
            Settings.WindowTitle = "Yet Another Roguelike Tutorial - Framework Version";
            Game.Instance.OnStart = Init;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            Map = GenerateMap();
            Player = GeneratePlayer();
            Map.AddEntity(Player);

            Game.Instance.Screen = Map;
        }

        private static Player GeneratePlayer()
        {
            return new Player((Width / 2, Height / 2));
        }

        private static RogueLikeMap GenerateMap()
        {
            return new RogueLikeMap(Width, Height, 1, Distance.Manhattan);
        }

        public static void HandleAction(IAction action)
        {
            if (action is EscapeAction)
            {
                Game.Instance.MonoGameInstance.Exit();
            }
            if (action is MovementAction move)
            {
                Player.Position = Player.Position.Add(move.Delta);
            }
        }
    }
}
