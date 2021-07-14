using System;
using System.Collections.Generic;
using System.Linq;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using TutorialRoguelike.Constants;

namespace TutorialRoguelike.EventHandlers
{
    public class HistoryViewer : EventHandler
    {
        private int Width;
        private int Height;
        private SadConsole.Console MessageLog;
        private int LogLength;
        private int Cursor;

        public HistoryViewer(Engine engine) : base(engine)
        {
            Width = 2 * Engine.Console.Width - 6;
            Height = Engine.Console.Height - 10;

            LogLength = Engine.MessageLog.Messages.Count;
            Cursor = LogLength - 1;

            MessageLog = new SadConsole.Console(Width, Height);
            MessageLog.Font = Game.Instance.EmbeddedFont;
            MessageLog.Position = (3,3);
            MessageLog.DefaultBackground = Color.Transparent;
            Engine.Console.Children.Add(MessageLog);
        }

        public override void Render()
        {
            base.Render();
            MessageLog.Clear();
            MessageLog.DrawBox(new Rectangle(0, 0, Width, Height), new ColoredGlyph(Colors.ExtendedMessageLogTitleText, Colors.ExtendedMessageLogFrame));
            MessageLog.Print(0, 0, "┤Message history├".Align(HorizontalAlignment.Center, MessageLog.Width));

            Engine.MessageLog.Render(MessageLog, 1, 1, Width - 2, Height - 2, Engine.MessageLog.Messages.Take(Cursor + 1));
        }

        private Dictionary<Keys, int> UpDownKeys = new Dictionary<Keys, int> {
            { Keys.Up, -1 } ,
            { Keys.Down, 1 } ,
            { Keys.PageUp, -10 } ,
            { Keys.PageDown, 10 } ,
        };

        public override bool ProcessKeyboard(IScreenObject host, Keyboard keyboard)
        {
            foreach (var key in UpDownKeys)
            {
                if (keyboard.IsKeyPressed(key.Key))
                {
                    if (key.Value < 0 && Cursor == 0) //Only move from the top to the bottom when you're on the edge
                        Cursor = LogLength - 1;
                    else if (key.Value > 0 && Cursor == LogLength - 1) //Same with bottom to top movement
                        Cursor = 0;
                    else //Otherwise move while staying clamped to the bounds of the history log
                        Cursor = Math.Max(0, Math.Min(Cursor + key.Value, LogLength - 1));
                    return true;
                }
            }
            if (keyboard.IsKeyPressed(Keys.Home))
            {
                Cursor = 0; //Move directly to top message
                return true;
            }
            else if (keyboard.IsKeyPressed(Keys.End))
            {
                Cursor = LogLength - 1; //Move directly to last message
                return true;
            }
            else if (keyboard.KeysPressed.Any()) //Any other key exits
            {
                MessageLog.Parent = null;
                Engine.EventHandler = new MainGameEventHandler(Engine);
                Render();
            }     
            return true;
        }

        public override bool ProcessMouse(IScreenObject host, MouseScreenObjectState state)
        {
            Render();
            return false;
        }
    }
}
