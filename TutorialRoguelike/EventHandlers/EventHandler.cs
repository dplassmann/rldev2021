using SadConsole;
using SadConsole.Input;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.EventHandlers
{
    public abstract class EventHandler
    {
        protected EventHandler(Engine engine)
        {
            Engine = engine;
        }

        public Engine Engine { get; private set; }

        public abstract bool ProcessKeyboard(IScreenObject host, Keyboard keyboard);

        public abstract bool ProcessMouse(IScreenObject host, MouseScreenObjectState state);

        public virtual bool HandleAction(IAction action)
        {
            if (action != null)
            {
                try
                {
                    action.Perform();
                }
                catch (ImpossibleException ex)
                {
                    Engine.MessageLog.Add(ex.Message, Colors.Impossible);
                    return false;
                }
            }
            Engine.HandleEnemyTurns();
            Engine.UpdateFov();

            return true;
        }

        public virtual void Render()
        {
            Engine.Render();
        }
    }
}
