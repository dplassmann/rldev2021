using System.Collections.Generic;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike
{
    // Somthing that holds entities
    public interface EntityContainer
    {
        public IList<Entity> Entities { get; }
    }
}
