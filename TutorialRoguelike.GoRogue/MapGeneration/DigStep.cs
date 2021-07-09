using System.Collections.Generic;
using System.Linq;
using GoRogue.MapGeneration;
using SadRogue.Integration;
using SadRogue.Integration.FieldOfView.Memory;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using TutorialRoguelike.GoRogue.Terrain;

namespace TutorialRoguelike.GoRogue.MapGeneration
{
    public class DigStep : GenerationStep
    {
        public DigStep() : base(null, (typeof(List<RectangularRoom>), "Rooms"), (typeof(List<IEnumerable<Point>>), "Tunnels")) { }

        protected override IEnumerator<object> OnPerform(GenerationContext context)
        {
            var rooms = context.GetFirst<List<RectangularRoom>>("Rooms");
            var tunnels = context.GetFirst<List<IEnumerable<Point>>>("Tunnels");

            var wallFloor = context.GetFirstOrNew<ISettableGridView<MemoryAwareRogueLikeCell>>(() => new ArrayView<MemoryAwareRogueLikeCell>(context.Width, context.Height), "WallFloor");
            foreach(var p in wallFloor.Positions())
                wallFloor[p] = TileFactory.Wall(p);

            foreach (var room in rooms)
            {
                foreach (var p in room.InteriorPositions())
                    wallFloor[p] = TileFactory.Floor(p);
            }

            foreach (var tunnel in tunnels)
            {
                foreach (var p in tunnel)
                    wallFloor[p] = TileFactory.Floor(p);
            }

            yield return null;
        }
    }
}
