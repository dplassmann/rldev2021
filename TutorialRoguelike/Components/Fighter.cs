using System;
using Newtonsoft.Json;
using TutorialRoguelike.Constants;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.Components
{
    public class Fighter : BaseComponent
    {
        public int MaxHp { get; set; }

        private int _hp;
        public int Hp
        {
            get => _hp;
            set
            {
                _hp = Math.Max(0, Math.Min(value, MaxHp));
                if (_hp <= 0 && Actor.IsAlive)
                {
                    Die();
                }
            }
        }

        public int BaseDefense { get; set; }
        public int BasePower { get; set; }

        [JsonIgnore]
        public int DefenseBonus => Actor?.Equipment?.DefenseBonus ?? 0;

        [JsonIgnore]
        public int PowerBonus => Actor?.Equipment?.PowerBonus ?? 0;

        [JsonIgnore]
        public int Defense => BaseDefense + DefenseBonus;

        [JsonIgnore]
        public int Power => BasePower + PowerBonus;
        
        [JsonIgnore]
        public Actor Actor { get => (Actor) Parent; }

        [JsonConstructor]
        public Fighter(int hp, int maxHp, int baseDefense, int basePower)
        {
            MaxHp = maxHp;
            _hp = hp;
            BaseDefense = baseDefense;
            BasePower = basePower;
        }

        public Fighter(int hp, int baseDefense, int basePower) : this(hp, hp, baseDefense, basePower) { }

        public int Heal(int amount)
        {
            if (Hp == MaxHp)
                return 0;

            var newHp = Math.Min(Hp + amount, MaxHp);
            var amountRecovered = newHp - Hp;
            Hp = newHp;
            return amountRecovered;            
        }

        public void TakeDamage(int amount)
        {
            Hp -= amount;
        }

        private void Die()
        {
            string deathMessage;
            var deathColor = Parent == Engine.Player ? Colors.PlayerDie : Colors.EnemyDie;
            if (Parent == Engine.Player)
            {
                deathMessage = "You died!";
            } else
            {
                deathMessage = $"{Parent.Name} is dead!";
            }
            Parent = EntityFactory.Corpse(Actor);
            Engine.MessageLog.Add(deathMessage, deathColor);

            Engine.Player.Level.AddXp(Actor.Level.XpGiven);
        }

    }
}
