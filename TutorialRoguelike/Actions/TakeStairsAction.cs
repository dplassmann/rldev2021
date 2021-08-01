using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;
using TutorialRoguelike.Exceptions;

namespace TutorialRoguelike.Actions
{
    public class TakeStairsAction : BaseAction
    {
        public TakeStairsAction(Actor entity) : base(entity)
        {
        }

        // Take the stairs, if any exist at the entity's location
        public override void Perform()
        {
            if (Entity.Position == Engine.Map.DownStairsLocation)
            {
                Engine.World.GenerateFloor();
                Engine.MessageLog.Add("You descend the staircase.", Colors.Descend);
            }
            else
            {
                throw new ImpossibleException("There are no stairs here.");
            }
        }
    }
}
