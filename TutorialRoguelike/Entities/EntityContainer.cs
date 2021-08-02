using System.Collections.Generic;
using TutorialRoguelike.World;

namespace TutorialRoguelike.Entities
{
    // Somthing that holds entities
    public interface EntityContainer
    {
        public IList<Entity> Entities { get; }

        public GameMap Map { get; }
    }
}
