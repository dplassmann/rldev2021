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
                    Handler.OnDestroy();
                    Handler = (EventHandler) handler;
                }
            }
            catch (QuitWithoutSaving)
            {
                Game.Instance.MonoGameInstance.Exit();
            }
            catch (SystemExit)
            {
                //TODO Add save
                Game.Instance.MonoGameInstance.Exit();
            }
            catch (Exception)
            {
                //TODO Add save
                Game.Instance.MonoGameInstance.Exit();
            }

            Handler.Render();
        }
    }
}
