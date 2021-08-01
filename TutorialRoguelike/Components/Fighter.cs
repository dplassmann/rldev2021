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

        public int Defense { get; set; }

        public int Power { get; set; }

        public Actor Actor { get => (Actor) Parent; }

        [JsonConstructor]
        public Fighter(int hp, int maxHp, int defense, int power)
        {
            MaxHp = maxHp;
            _hp = hp;
            Defense = defense;
            Power = power;
        }

        public Fighter(int hp, int defense, int power) : this(hp, hp, defense, power) { }

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
