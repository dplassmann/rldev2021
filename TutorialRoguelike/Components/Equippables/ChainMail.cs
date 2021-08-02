using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Components
{
    public class ChainMail : Equippable
    {
        public ChainMail() : base(EquipmentType.Armor, defenseBonus: 3)
        {
        }
    }
}
