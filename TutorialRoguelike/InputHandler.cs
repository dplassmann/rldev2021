using System;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;
using TutorialRoguelike.Exceptions;
using EventHandler = TutorialRoguelike.EventHandlers.EventHandler;

namespace TutorialRoguelike
{
    public class InputHandler : MouseConsoleComponent, IComponent
    {
        public bool IsKeyboard => true;
        private EventHandler Handler;

        public InputHandler(EventHandler handler)
        {
            Handler = handler;
        }

        public override void ProcessMouse(IScreenObject host, MouseScreenObjectState state, out bool handled)
        {
            if (state.IsOnScreenObject)
            {
                handled = true;
                ProcessEvents(host, null, state);
            }
            else
                handled = false;
        }

        public void ProcessKeyboard(IScreenObject host, Keyboard keyboard, out bool handled)
        {
            handled = true;
            ProcessEvents(host, keyboard, null);
        }

        private void ProcessEvents(IScreenObject host, Keyboard keyboard, MouseScreenObjectState mouseState)
        {
            IActionOrEventHandler handler;
            try
            {
                handler = Handler.ProcessEvents(host, keyboard, mouseState);
                if (handler is EventHandler && handler != Handler)
                {
                    if (!((EventHandler)handler).IsTemporary)
                        Handler.OnDestroy();
                    Handler = (EventHandler) handler;
                }
            }
            catch (QuitWithoutSaving)
            {
                Exit();
            }
            catch (SystemExit)
            {
                SaveGame();
                Exit();
            }
            catch (Exception)
            {
                SaveGame();
                Exit();
            }

            Handler.Render();
        }

        private void SaveGame()
        {
            if (Handler.Engine != null)
            {
                Initialization.SaveAs(Handler.Engine);
            }
        }

        private void Exit()
        {
            Game.Instance.MonoGameInstance.Exit();
        }
    }
}
