using System;
using System.Linq;
using SadConsole;
using SadConsole.Input;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;
using SadRogue.Primitives;

namespace TutorialRoguelike.EventHandlers
{
    public abstract class InventoryEventHandler : AskUserEventHandler
    {
        protected abstract string Title { get; }
        private SadConsole.Console Console { get; set; }

        public InventoryEventHandler(Engine engine) : base(engine)
        {
            var inventory = Engine.Player.Inventory.Items;
            var numberOfItemsInInventory = inventory.Count;
            var width = (Title.Length + 4) * 2;
            var height = Math.Max(3, numberOfItemsInInventory + 2);
            var x = Engine.Player.Position.X <= 30 ? 80 : 0;
            var y = 0;

            Console = new SadConsole.Console(width, height);
            Console.Font = Game.Instance.EmbeddedFont;
            Console.Position = (x, y);
            Engine.Console.Children.Add(Console);

            Console.DrawBox(new Rectangle(0, 0, width, height), new ColoredGlyph(Color.White, Color.Black), new ColoredGlyph(Color.White, Color.Black), ICellSurface.ConnectedLineThin);
            Console.Print(1, 0, Title.Align(HorizontalAlignment.Center, width-2, (char)ICellSurface.ConnectedLineThin[(int)ICellSurface.ConnectedLineIndex.Top]), Color.White, Color.Black);

            if (numberOfItemsInInventory > 0)
            {
                for (int i = 0; i < numberOfItemsInInventory; i++)
                {
                    var itemKey = (char)('a' + i);
                    Console.Print(1, i + 1, $"({itemKey}) {inventory[i].Name}");
                }
            }
            else
            {
                Console.Print(1, 1, "(Empty)");
            }
        }

        // Renders an inventory menu, which displays the items in the inventory and the letter to select them.
        // Will move to a different position based on where the player is located, so the player
        // can always see where they are.
        public override void Render()
        {
            base.Render();

        }

        public override void TransitionTo(EventHandler newHandler)
        {
            Console.Parent = null;
            base.TransitionTo(newHandler);
        }

        public override bool ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            var key = keyboard.KeysPressed.FirstOrDefault();
            if (key != null && key.Key >= Keys.A && key.Key <= Keys.Z)
            {
                try
                {
                    return HandleAction(ItemSelected(Engine.Player.Inventory.Items[key.Key - Keys.A]));
                }
                catch (ArgumentOutOfRangeException)
                {
                    Engine.MessageLog.Add("Invalid entry.", Colors.Invalid);
                }
            }

            return base.ProcessKeyboard(host, keyboard);
        }

        protected abstract IAction ItemSelected(Item item);
    }
}
