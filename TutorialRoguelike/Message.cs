using SadRogue.Primitives;

namespace TutorialRoguelike
{
    public class Message
    {
        public string Text { get; set; }
        public Color Color { get; set; }
        public int Count { get; set; }

        public Message(string text, Color color)
        {
            Text = text;
            Color = color;
            Count = 1;
        }

        public string FullText => Count > 1 ? $"{Text} (x {Count})" : Text;
    }
}
