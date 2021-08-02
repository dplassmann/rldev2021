using TutorialRoguelike.Constants;

namespace TutorialRoguelike.Components
{
    public class Dagger : Equippable
    {
        public Dagger() : base(EquipmentType.Weapon, powerBonus: 2)
        {
        }
    }
}
