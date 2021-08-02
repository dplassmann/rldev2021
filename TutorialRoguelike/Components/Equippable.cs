using Newtonsoft.Json;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Components
{
    public class Equippable : BaseComponent
    {
        public EquipmentType EquipmentType { get; set; }
        public int PowerBonus { get; set; }
        public int DefenseBonus { get; set; }

        [JsonIgnore]
        public Item Item => (Item)Parent;

        public Equippable(EquipmentType equipmentType, int powerBonus = 0, int defenseBonus = 0)
        {
            EquipmentType = equipmentType;
            PowerBonus = powerBonus;
            DefenseBonus = defenseBonus;
        }
    }
}
