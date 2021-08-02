using System;
using Newtonsoft.Json;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Components
{
    public class Equipment : BaseComponent
    {
        public Item Weapon { get; set; }
        public Item Armor { get; set; }

        [JsonIgnore]
        public Actor Actor => (Actor) Parent;

        [JsonIgnore]
        public int DefenseBonus => (Weapon?.Equippable?.DefenseBonus ?? 0) + (Armor?.Equippable?.DefenseBonus ?? 0);

        [JsonIgnore]
        public int PowerBonus => (Weapon?.Equippable?.PowerBonus ?? 0) + (Armor?.Equippable?.PowerBonus ?? 0);

        public Equipment(Item weapon = null, Item armor = null)
        {
            Weapon = weapon;
            Armor = armor;
        }

        public bool IsItemEquipped(Item item) => Weapon == item || Armor == item;

        public void ToggleEquipment(Item equippableItem, bool addMessage = true)
        {
            if (equippableItem.Equippable == null)
                throw new InvalidOperationException("Can't equip non-equippable");

            if (GetItemInSlot(equippableItem.Equippable.EquipmentType) == equippableItem)
                UnequipFromSlot(equippableItem.Equippable.EquipmentType, addMessage);
            else
                EquipToSlot(equippableItem.Equippable.EquipmentType, equippableItem, addMessage);
        }

        private void EquipToSlot(EquipmentType slot, Item item, bool addMessage)
        {
            if (GetItemInSlot(slot) != null)
                UnequipFromSlot(slot, addMessage);

            switch (slot)
            {
                case EquipmentType.Weapon:
                    Weapon = item;
                    break;
                case EquipmentType.Armor:
                    Armor = item;
                    break;
            }

            if (addMessage)
                SendEquipMessage(item);
        }

        // Doesn't check slot is equipped, so don't expose externally without fixing that
        private void UnequipFromSlot(EquipmentType slot, bool addMessage)
        {
            var currentItem = GetItemInSlot(slot);

            if (addMessage)
                SendUnequipMessage(currentItem);

            switch (slot)
            {
                case EquipmentType.Weapon:
                    Weapon = null;
                    break;
                case EquipmentType.Armor:
                    Armor = null;
                    break;
            }
        }

        private Item GetItemInSlot(EquipmentType slot) => slot switch
        {
            EquipmentType.Weapon => Weapon,
            EquipmentType.Armor => Armor,
            _ => throw new ArgumentOutOfRangeException(nameof(slot), $"Unexpected equipment type: {slot}")
        };

        private void SendEquipMessage(Item item)
        {
            Parent.Map.Engine.MessageLog.Add($"You equip the {item.Name}");
        }
        private void SendUnequipMessage(Item item)
        {
            Parent.Map.Engine.MessageLog.Add($"You remove the {item.Name}");
        }

    }
}
