using SadConsole;
using SadRogue.Primitives;

namespace TutorialRoguelike.EventHandlers
{
    public class CharacterScreenEventHandler : AskUserEventHandler
    {
        private const string Title = "Character Information";
        private Console Console;

        public CharacterScreenEventHandler(Engine engine) : base(engine)
        {
            var width = (Title.Length + 4) * 2;
            var height = 7;
            var x = Engine.Player.Position.X <= 30 ? 80 : 0;
            var y = 0;

            Console = new Console(width, height);
            Console.Font = Game.Instance.EmbeddedFont;
            Console.Position = (x, y);
            Engine.Console.Children.Add(Console);

            Console.DrawBox(new Rectangle(0, 0, width, height), new ColoredGlyph(Color.White, Color.Black), new ColoredGlyph(Color.White, Color.Black), ICellSurface.ConnectedLineThin);
            Console.Print(1, 0, Title.Align(HorizontalAlignment.Center, width - 2, (char)ICellSurface.ConnectedLineThin[(int)ICellSurface.ConnectedLineIndex.Top]), Color.White, Color.Black);

            Console.Print(1, 1, $"Level: {Engine.Player.Level.CurrentLevel}");
            Console.Print(1, 2, $"XP: {Engine.Player.Level.CurrentXp}");
            Console.Print(1, 3, $"XP for next level: {Engine.Player.Level.ExperienceToNextLevel}");
            Console.Print(1, 4, $"Attack: {Engine.Player.Fighter.Power}");
            Console.Print(1, 5, $"Defense: {Engine.Player.Fighter.Defense}");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Console.Parent = null;
        }
    }
}
