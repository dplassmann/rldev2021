using SadConsole;
using SadConsole.Input;

namespace TutorialRoguelike.Manual.EventHandlers
{
    public abstract class EventHandler
    {
        protected EventHandler(Engine engine)
        {
            Engine = engine;
        }

        public Engine Engine { get; private set; }

        public abstract bool ProcessKeyboard(IScreenObject host, Keyboard keyboard);
    }
}
