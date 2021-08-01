using Newtonsoft.Json;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Components
{
    public class Level : BaseComponent
    {
        public int CurrentLevel { get; private set; }
        public int CurrentXp { get; private set; }
        public int LevelUpBase { get; private set; }
        public int LevelUpFactor { get; private set; }
        public int XpGiven { get; private set; }

        private Actor Actor => (Actor) Parent;

        public Level(int currentLevel = 1, int currentXp = 0, int levelUpBase = 0, int levelUpFactor = 150, int xpGiven = 0)
        {
            CurrentLevel = currentLevel;
            CurrentXp = currentXp;
            LevelUpBase = levelUpBase;
            LevelUpFactor = levelUpFactor;
            XpGiven = xpGiven;
        }

        [JsonIgnore]
        public int ExperienceToNextLevel => LevelUpBase + CurrentLevel * LevelUpFactor;

        [JsonIgnore]
        public bool RequiresLevelUp => CurrentXp > ExperienceToNextLevel;

        public void AddXp(int xp)
        {
            if (xp == 0 || LevelUpBase == 0)
                return;

            CurrentXp += xp;

            Engine.MessageLog.Add($"You gain {xp} experience points.");

            if (RequiresLevelUp)
            {
                Engine.MessageLog.Add($"You advance to level {CurrentLevel + 1}.");
            }
        }

        public void IncreaseLevel()
        {
            CurrentXp -= ExperienceToNextLevel;
            CurrentLevel += 1;
        }

        public void IncreaseMaxHp(int amount = 20) 
        {
            Actor.Fighter.MaxHp += amount;
            Actor.Fighter.Hp += amount;

            Engine.MessageLog.Add("Your health improves!");
            IncreaseLevel();
        }

        public void IncreasePower(int amount = 1)
        {
            Actor.Fighter.Power += amount;

            Engine.MessageLog.Add("You feel stornger!");
            IncreaseLevel();
        }

        public void IncreaseDefense(int amount = 1)
        {
            Actor.Fighter.Defense += amount;

            Engine.MessageLog.Add("Your movements are getting swifter!");
            IncreaseLevel();
        }
    }
}
