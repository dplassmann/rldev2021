﻿using System;
using SadConsole;
using TutorialRoguelike.Constants;
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

        public Actor Actor { get => (Actor) Parent; }

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
            var deathColor = Parent == Engine.Player ? Colors.PlayerDie : Colors.EnemyDie;
            if (Parent == Engine.Player)
            {
                deathMessage = "You died!";
                Engine.EventHandler = new GameOverEventHandler(Engine);
            } else
            {
                deathMessage = $"{Parent.Name} is dead!";
            }
            Parent = EntityFactory.Corpse(Actor);
            Engine.MessageLog.Add(deathMessage, deathColor);
        }

    }
}
