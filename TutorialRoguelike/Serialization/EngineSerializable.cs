using TutorialRoguelike.Entities;
using TutorialRoguelike.World;

namespace TutorialRoguelike.Serialization
{
    public class EngineSerializable
    {
        public Actor Player { get; set; }
        public GameMapSerializable Map { get; set; }
        public GameWorld World { get; set; }

        public static implicit operator EngineSerializable(Engine engine)
        {
            return new EngineSerializable
            {
                Player = engine.Player,
                Map = engine.Map,
                World = engine.World
            };
        }
    }
}
