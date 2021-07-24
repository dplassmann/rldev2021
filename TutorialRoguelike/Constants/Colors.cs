using SadRogue.Primitives;

namespace TutorialRoguelike.Constants
{
    public class Colors
    {
        //Entities
        public static readonly Color Player = Color.White;
        public static readonly Color Orc = Color.Green;
        public static readonly Color HealthPotion = Color.DarkViolet;
        public static readonly Color LightningScroll = Color.Yellow;
        public static readonly Color ConfusionScroll = Color.BlueViolet;
        public static readonly Color FireballScroll = Color.OrangeRed;

        //Terrain
        public static readonly Color Floor = new Color(50, 50, 50);
        public static readonly Color Wall = Color.Gray;

        //Text
        public static readonly Color PlayerAttack = new Color(0xE0, 0xE0, 0xE0);
        public static readonly Color EnemyAttack = new Color(0xFF, 0xC0, 0xC0);
        public static readonly Color NeedsTarget = new Color(0x3F, 0xFF, 0xFF);
        public static readonly Color StatusEffectApplied = new Color(0x3F, 0xFF, 0x3F);


        public static readonly Color PlayerDie = new Color(0xFF, 0x30, 0x30);
        public static readonly Color EnemyDie = new Color(0xFF, 0xA0, 0x30);

        public static readonly Color Invalid = Color.Yellow;
        public static readonly Color Impossible = Color.Gray;
        public static readonly Color Error = Color.OrangeRed;

        public static readonly Color WelcomeText = new Color(0x20, 0xA0, 0xFF);
        public static readonly Color HealthRecovered = Color.Lime;

        public static readonly Color BarText = Color.White;
        public static readonly Color BarFilled = Color.Green;
        public static readonly Color BarEmpty = Color.DarkRed;

        public static readonly Color ExtendedMessageLogFrame = Color.OliveDrab;
        public static readonly Color ExtendedMessageLogTitleText = Color.White;

        public static readonly Color MenuTitle = Color.Yellow;
        public static readonly Color MenuText = Color.White;

        //Other
        public static readonly Color TargetingOverlay = Color.Red;
        public static readonly Color TargetingAreaOverlay = Color.OrangeRed;


    }
}
