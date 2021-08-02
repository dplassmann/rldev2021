using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Components
{
    public class Sword : Equippable
    {
        public Sword() : base(EquipmentType.Weapon, powerBonus: 4)
        {
        }
    }
}
