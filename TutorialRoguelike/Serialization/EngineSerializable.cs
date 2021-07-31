using Newtonsoft.Json;
using SadConsole;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Serialization
{
    public class EngineSerializable
    {
        public Actor Player { get; set; }
        public GameMapSerializable Map { get; set; }

        public static implicit operator EngineSerializable(Engine engine)
        {
            return new EngineSerializable
            {
                Player = engine.Player,
                Map = engine.Map
            };
        }
    }
}
