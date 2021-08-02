using System;
using System.Linq;
using SadConsole;
using SadConsole.Input;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.EventHandlers
{
    public abstract class InventoryEventHandler : DialogBoxEventHandler
    {
        public InventoryEventHandler(Engine engine, string title) : base(engine, (title.Length + 4) * 2, Math.Max(3, engine.Player.Inventory.Items.Count + 2), title)
        {
            var inventory = Engine.Player.Inventory.Items;
            var numberOfItemsInInventory = inventory.Count;
            if (numberOfItemsInInventory > 0)
            {
                for (int i = 0; i < numberOfItemsInInventory; i++)
                {
                    var isEquipped = Engine.Player.Equipment.IsItemEquipped(inventory[i]);

                    var itemKey = (char)('a' + i);
                    Console.Print(1, i + 1, $"({itemKey}) {inventory[i].Name}" + (isEquipped ? " (E)" : ""));
                }
            }
            else
            {
                Console.Print(1, 1, "(Empty)");
            }
        }

        public override IActionOrEventHandler ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            var key = keyboard.KeysPressed.FirstOrDefault();
            if (key != null && key.Key >= Keys.A && key.Key <= Keys.Z)
            {
                try
                {
                    return ItemSelected(Engine.Player.Inventory.Items[key.Key - Keys.A]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Engine.MessageLog.Add("Invalid entry.", Colors.Invalid);
                }
            }

            return base.ProcessKeyboard(host, keyboard);
        }

        protected abstract IActionOrEventHandler ItemSelected(Item item);
    }
}
