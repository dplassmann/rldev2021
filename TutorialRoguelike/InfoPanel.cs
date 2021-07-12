using System;
using SadConsole;
using SadRogue.Primitives;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike
{
    public class InfoPanel : SadConsole.Console
    {
        private int HealthBarWidth;
        private Actor Player;
        public MessageLog MessageLog;

        public InfoPanel(int width, int height, Actor player) : base(width*2, height) //Parameters width and height are using 16x16 fonts, but this is in 8x16
        {
            Player = player;
            Font = Game.Instance.EmbeddedFont;            
            HealthBarWidth = Width / 4;
            MessageLog = new MessageLog();
        }

        public void Render()
        {
            this.Clear(); //Dirty flag could be good here.
            RenderBar(1, 0, Player.Fighter.Hp, Player.Fighter.MaxHp);
            MessageLog.Render(this, HealthBarWidth + 5, 0, 40, Height);
        }

        private void RenderBar(int x, int y, int currentValue, int maxValue)
        {
            var filledWidth = (int)(((double) currentValue / maxValue) * HealthBarWidth);

            //Ensure that we don't zero out the bar until the value actually hits zero
            if (currentValue > 0 && filledWidth == 0)
            {
                filledWidth = 1;
            }

            this.DrawBox(new Rectangle(x, y, HealthBarWidth, 1), new ColoredGlyph(Colors.BarText, Colors.BarEmpty));
            if (filledWidth > 0) //A 0-width box isn't just nothing, so don't attempt to draw that.
            {               
                this.DrawBox(new Rectangle(x, y, filledWidth, 1), new ColoredGlyph(Colors.BarText, Colors.BarFilled));
            }
            this.Print(x, y, $"HP: {currentValue}/{maxValue}");
        }
    }
}
