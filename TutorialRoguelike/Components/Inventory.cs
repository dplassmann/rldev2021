using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Components
{
    public class Inventory : BaseComponent, EntityContainer
    {
        public int Capacity { get; private set; }
        public IList<Item> Items { get; private set; }

        [JsonIgnore]
        public IList<Entity> Entities { get { return (IList<Entity>) Items; } }

        [JsonConstructor]
        public Inventory(int capacity)
        {
            Capacity = capacity;
            Items = new List<Item>();
        }

        public Inventory(Inventory serializableInventory)
        {
            Capacity = serializableInventory.Capacity;
            Items = serializableInventory.Items.Select(i => new Item(i)).ToList();
            foreach (var item in Items)
            {
                item.Parent = this;
            }
        }

        // Removes an item from the inventory and returns it to the game map, at the player's location
        public void Drop(Item item)
        {
            Items.Remove(item);
            item.Parent = null;
            item.Place(Parent.Position, Parent.Map);

            Engine.MessageLog.Add($"{Parent.Name} dropped the {item.Name}.");
        }
    }
}
