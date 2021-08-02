using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Actions
{
    public class EquipAction : BaseAction
    {
        Item Item;

        public EquipAction(Actor entity, Item item) : base(entity)
        {
            Item = item;
        }

        public override void Perform()
        {
            Entity.Equipment.ToggleEquipment(Item);
        }
    }
}
