using System;
using System.IO;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Constants;
using TutorialRoguelike.EventHandlers;
using TutorialRoguelike.Exceptions;
using Console = SadConsole.Console;
using EventHandler = TutorialRoguelike.EventHandlers.EventHandler;

namespace TutorialRoguelike.UI
{
    public class MainMenu : EventHandler
    {
        private Console MenuConsole;

        public MainMenu() : base(null)
        {
            var mainConsole = (Console)GameHost.Instance.Screen;
            var width = 2 * mainConsole.Width;
            var height = mainConsole.Height;
            var backgroundTexture = GameHost.Instance.GetTexture("UI/menu_background.png");
            var backgroundSurface = backgroundTexture.ToSurface(TextureConvertMode.Background, width, height);

            MenuConsole = new Console(backgroundSurface);
            MenuConsole.Font = Game.Instance.EmbeddedFont;
            MenuConsole.Position = (0, 0);
            MenuConsole.DefaultBackground = Color.Transparent;
            mainConsole.Children.Add(MenuConsole);

            MenuConsole.Print(0, MenuConsole.Height / 2 - 4, "TOMBS OF THE ANCIENT KINGS".Align(HorizontalAlignment.Center, MenuConsole.Width), Colors.MenuTitle);
            MenuConsole.Print(0, MenuConsole.Height - 2, "By David Plassmann".Align(HorizontalAlignment.Center, MenuConsole.Width), Colors.MenuTitle);

            var menuWidth = 24;
            var options = new string[] { "[N] Play a new game", "[C] Continue last game", "[Q] Quit" };
            for (int i = 0; i < options.Length; i++)
            {
                MenuConsole.Print(
                    0,
                    MenuConsole.Height / 2 - 2 + i,
                    options[i].Align(HorizontalAlignment.Left, menuWidth).Align(HorizontalAlignment.Center, MenuConsole.Width),
                    Colors.MenuText);
            }
        }

        public override void Render()
        {

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            MenuConsole.Parent = null;
        }

        public override IActionOrEventHandler ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            if (keyboard.IsKeyPressed(Keys.Q) || keyboard.IsKeyPressed(Keys.Escape))
                throw new SystemExit();
            else if (keyboard.IsKeyPressed(Keys.C))
            {
                try
                {
                    return new MainGameEventHandler(Initialization.LoadGame());
                }
                catch (FileNotFoundException)
                {
                    return new PopupMessage("No saved game to load", this, MenuConsole);
                }
                catch (Exception ex)
                {
                    return new PopupMessage($"Failed to load save:\n{ex.Message}", this, MenuConsole);
                }
            }
            else if (keyboard.IsKeyPressed(Keys.N))
            {
                return new MainGameEventHandler(Initialization.NewGame(Program.WindowWidth, Program.WindowHeight));
            }

            return null;
        }
        public override IActionOrEventHandler ProcessMouse(IScreenObject host, MouseScreenObjectState state) { return null; }
    }
}
