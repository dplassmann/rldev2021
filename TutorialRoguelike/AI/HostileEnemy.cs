using System;
using System.Collections.Generic;
using System.Linq;
using SadRogue.Primitives;
using TutorialRoguelike.Actions;
using TutorialRoguelike.Entities;

namespace TutorialRoguelike.AI
{
    public class HostileEnemy : BaseAI
    {
        private IEnumerable<Point> Path { get; set; }

        public HostileEnemy(Actor entity) : base(entity)
        {
            Path = new List<Point>();
        }

        public override void Perform()
        {
            var target = Engine.Player;
            var delta = target.Position - Entity.Position;

            var distance = Distance.Chebyshev.Calculate(delta);

            if (Engine.Map.Visible[Entity.Position]) //Assume symmetrical visibility
            {
                if (distance <= 1)
                {
                    new MeleeAction(Entity, Direction.GetDirection(delta)).Perform();
                    return;
                }
                Path = GetPathTo(target.Position);
            }

            if (Path.Any())
            {
                new MovementAction(Entity, Direction.GetDirection(Entity.Position, Path.First())).Perform();
                return;
            }

            new WaitAction(Entity).Perform();
        }
    }
}
