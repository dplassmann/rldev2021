using System;
using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.EventHandlers
{
    public class DialogBoxEventHandler : AskUserEventHandler
    {
        public string Title { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        protected SadConsole.Console Console { get; set; }
        
        public DialogBoxEventHandler(Engine engine, int width, int height, string title) : base(engine)
        {
            Width = width;
            Height = height;
            Title = title;
            var x = Engine.Player.Position.X <= 30 ? 80 : 0;
            var y = 0;

            Console = new SadConsole.Console(width, height);
            Console.Font = Game.Instance.EmbeddedFont;
            Console.Position = (x, y);
            Engine.Console.Children.Add(Console);

            Console.DrawBox(new Rectangle(0, 0, width, height), new ColoredGlyph(Color.White, Color.Black), new ColoredGlyph(Color.White, Color.Black), ICellSurface.ConnectedLineThin);
            Console.Print((Width - Title.Length) / 2 - 1, 0, " "+Title+" ", Color.Black, Color.White);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Console.Parent = null;
        }
    }
}
