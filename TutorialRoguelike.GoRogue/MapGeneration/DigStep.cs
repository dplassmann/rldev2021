using System.Collections.Generic;
using GoRogue.MapGeneration;
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
            var roomsContext = context.GetFirst<List<RectangularRoom>>("Rooms");
            var tunnelsContext = context.GetFirst<List<IEnumerable<Point>>>("Tunnels");

            var wallFloor = context.GetFirstOrNew<ISettableGridView<TileTypes>>(() => new ArrayView<TileTypes>(context.Width, context.Height), "WallFloor");
            wallFloor.Fill(TileTypes.Wall);

            foreach (var room in roomsContext)
            {
                foreach (var p in room.InteriorPositions())
                    wallFloor[p] = TileTypes.Floor;
            }

            foreach (var tunnel in tunnelsContext)
            {
                foreach (var p in tunnel)
                    wallFloor[p] = TileTypes.Floor;
            }

            yield return null;
        }
    }
}
