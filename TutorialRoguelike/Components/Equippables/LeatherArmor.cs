using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Components
{
    public class LeatherArmor : Equippable
    {
        public LeatherArmor() : base(EquipmentType.Armor, defenseBonus: 1)
        {
        }
    }
}
