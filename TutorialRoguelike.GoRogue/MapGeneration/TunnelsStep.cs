using System.Collections.Generic;
using GoRogue;
using GoRogue.MapGeneration;
using GoRogue.Random;
using SadRogue.Primitives;

namespace TutorialRoguelike.GoRogue.MapGeneration
{
    public class TunnelsStep : GenerationStep
    {
        public TunnelsStep() : base(null, (typeof(List<RectangularRoom>), "Rooms")) { }

        protected override IEnumerator<object> OnPerform(GenerationContext context)
        {
            var roomsContext = context.GetFirst<List<RectangularRoom>>("Rooms");
            var tunnelsContext = context.GetFirstOrNew(() => new List<IEnumerable<Point>>(), "Tunnels");

            for (int i = 1; i < roomsContext.Count; i++)
            {
                tunnelsContext.Add(TunnelBetween(roomsContext[i].Center, roomsContext[i - 1].Center));
            }

            yield return null;
        }

        private static IEnumerable<Point> TunnelBetween(Point start, Point end)
        {
            Point corner;

            //randomly decide movement direction
            corner = (GlobalRandom.DefaultRNG.NextDouble() < 0.5) ? (end.X, start.Y) : (start.X, end.Y);

            foreach (var p in Lines.Get(start, corner))
            {
                yield return p;
            }
            foreach (var p in Lines.Get(corner, end))
            {
                yield return p;
            }
        }
    }
}
