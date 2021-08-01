using SadConsole;

namespace TutorialRoguelike.EventHandlers
{
    public class CharacterScreenEventHandler : DialogBoxEventHandler
    {
        public CharacterScreenEventHandler(Engine engine) : base(engine, 25, 7, "Character Information")
        {
            Console.Print(1, 1, $"Level: {Engine.Player.Level.CurrentLevel}");
            Console.Print(1, 2, $"XP: {Engine.Player.Level.CurrentXp}");
            Console.Print(1, 3, $"XP for next level: {Engine.Player.Level.ExperienceToNextLevel}");
            Console.Print(1, 4, $"Attack: {Engine.Player.Fighter.Power}");
            Console.Print(1, 5, $"Defense: {Engine.Player.Fighter.Defense}");
        }
    }
}
