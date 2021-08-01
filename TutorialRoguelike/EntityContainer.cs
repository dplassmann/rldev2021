using System.Collections.Generic;
using TutorialRoguelike.Entities;
using TutorialRoguelike.World;

namespace TutorialRoguelike
{
    // Somthing that holds entities
    public interface EntityContainer
    {
        public IList<Entity> Entities { get; }

        public GameMap Map { get; }
    }
}
