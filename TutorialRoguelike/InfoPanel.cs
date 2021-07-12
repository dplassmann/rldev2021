using System;
using System.Linq;
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
        private Engine Engine => Player.Map.Engine;
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
            MessageLog.Render(this, HealthBarWidth + 5, 1, 40, Height-1);
            RenderNamesAtMouseLocation(HealthBarWidth + 5, 0);
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

        private void RenderNamesAtMouseLocation(int x, int y)
        {
            this.Print(x, y, GetNamesAtLocation(Engine.MouseLocation, Player.Map));
        }


        private static string GetNamesAtLocation(Point position, GameMap map)
        {
            if (!map.InBounds(position) || !map.Visible[position])
                return string.Empty;

            var names = string.Join(", ", map.Entities.Where(e => e.Position == position).Select(e => e.Name));
            if (!string.IsNullOrEmpty(names))
            {
                names = char.ToUpper(names[0]) + (names.Length > 1 ? names[1..] : string.Empty);
            }
            return names;
        }
    }
}
