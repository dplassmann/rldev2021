using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Actions
{
    public class PickupAction : BaseAction
    {
        public PickupAction(Actor entity) : base(entity)
        {
        }

        public override void Perform()
        {
            var inventory = Entity.Inventory;
            var item = Engine.Map.Items.FirstOrDefault(item => item.Position == Entity.Position);
            if (item == null)
            {
                throw new ImpossibleException("There is nothing here to pick up.");
            }

            if (inventory.Items.Count >= inventory.Capacity)
            {
                throw new ImpossibleException("Your inventory is full.");
            }

            Engine.Map.Entities.Remove(item);
            item.Parent = inventory;
            inventory.Items.Add(item);

            Engine.MessageLog.Add($"You picked up the {item.Name}!");
        }
    }
}
