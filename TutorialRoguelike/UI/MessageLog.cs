using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using SadRogue.Primitives;

namespace TutorialRoguelike.UI
{
    public class MessageLog
    {
        public IList<Message> Messages { get; private set; }

        public MessageLog()
        {
            Messages = new List<Message>();
        }

        public void Add(string text, bool stack = true)
        {
            Add(text, Color.White, stack);
        }

        public void Add(string text, Color color, bool stack = true)
        {
            if (stack && text.Equals(Messages.LastOrDefault()?.Text))
                Messages.Last().Count += 1;
            else
                Messages.Add(new Message(text, color));
        }
        public void Render(SadConsole.Console console, int x, int y, int width, int height)
        {
            Render(console, x, y, width, height, Messages);
        }

        public void Render(SadConsole.Console console, int x, int y, int width, int height, IEnumerable<Message> messages)
        {
            int yOffset = height - 1;

            //Render messages from last to first, until we run out of space
            foreach (var message in messages.Reverse())
            {
                foreach (var line in Wrap(message.FullText, width).Reverse())
                {
                    console.Print(x, y + yOffset, line, message.Color);
                    yOffset -= 1;
                    if (yOffset < 0) //No more space
                        return;
                }
            }
        }

        private static IEnumerable<string> Wrap(string s, int width)
        {
            var result = new List<string>();
            while (s != null && s.Length > 0)
            {
                if (s.Length <= width)
                {
                    result.Add(s);
                    break;
                }
                var cutSpace = s.Substring(0, width).LastIndexOf(' ');
                var cutPoint = cutSpace == -1 ? width : cutSpace;
                result.Add(s.Substring(0, cutPoint));
                s = s.Substring(s[cutPoint] == ' ' ? cutPoint + 1 : cutPoint);
            }
            return result;
        }
    }
}
