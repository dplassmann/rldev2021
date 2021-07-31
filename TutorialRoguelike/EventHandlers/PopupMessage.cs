using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;

namespace TutorialRoguelike.EventHandlers
{
    public class PopupMessage : EventHandler
    {
        public Console Console;
        private Console ParentConsole;
        EventHandler Parent;

        public PopupMessage(string text, EventHandler parent, Console parentConsole) : base(parent.Engine, true)
        {
            Parent = parent;
            ParentConsole = parentConsole;

            var fontWidthScale = (Game.Instance.EmbeddedFont.GlyphWidth * 1.0) / parentConsole.Font.GlyphWidth;
            var fontHeightScale = (Game.Instance.EmbeddedFont.GlyphHeight * 1.0) / parentConsole.Font.GlyphHeight;

            var width = (int) fontWidthScale * parentConsole.Width;
            var height = (int) fontHeightScale * parentConsole.Height;

            Console = new Console(width, height);
            Console.Font = Game.Instance.EmbeddedFont;
            Console.Position = (0, 0);
            Console.DefaultBackground = Color.Transparent;
            parentConsole.Children.Add(Console);
            parentConsole.Renderer.Opacity = 50;

            Console.Print(0, height / 2, text.Align(HorizontalAlignment.Center, width));
        }

        public override IActionOrEventHandler ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            if (keyboard.HasKeysPressed)
            {
                Console.Parent = null;
                return Parent;
            } else
            {
                return null;
            }
        }

        public override void Render()
        {
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            ParentConsole.Renderer.Opacity = 255;
            Console.Parent = null;
        }
    }
}
