using System;
using SadConsole;
using TutorialRoguelike.Entities;
using TutorialRoguelike.EventHandlers;

namespace TutorialRoguelike.Components
{
    public class Fighter : BaseComponent
    {
        public int MaxHp { get; private set; }

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

        public int Defense { get; private set; }

        public int Power { get; private set; }

        public Actor Actor { get => (Actor) Entity; }

        public Fighter(int hp, int defense, int power) : base()
        {
            MaxHp = hp;
            _hp = hp;
            Defense = defense;
            Power = power;
        }

        private void Die()
        {
            string deathMessage;
            if (Entity == Engine.Player)
            {
                deathMessage = "You died!";
                Engine.EventHandler = new GameOverEventHandler(Engine);
            } else
            {
                deathMessage = $"{Entity.Name} is dead!";
            }
            Entity = EntityFactory.Corpse(Actor);
            System.Console.WriteLine(deathMessage);
        }

    }
}
