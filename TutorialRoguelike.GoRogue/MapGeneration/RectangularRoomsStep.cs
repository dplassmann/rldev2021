using System.Collections.Generic;
using System.Linq;
using GoRogue.MapGeneration;
using GoRogue.Random;
using SadRogue.Primitives;

namespace TutorialRoguelike.GoRogue.MapGeneration
{
    public class RectangularRoomsStep : GenerationStep
    {
        private int MaxRooms;
        private int RoomMinSize;
        private int RoomMaxSize;

        public RectangularRoomsStep(int maxRooms, int roomMinSize, int roomMaxSize)
        {
            MaxRooms = maxRooms;
            RoomMinSize = roomMinSize;
            RoomMaxSize = roomMaxSize;
        }

        protected override IEnumerator<object> OnPerform(GenerationContext context)
        {
            var roomsContext = context.GetFirstOrNew(() => new List<RectangularRoom>(), "Rooms");
            var rooms = new List<Rectangle>();

            for (int i = 0; i < MaxRooms; i++)
            {
                var width = GlobalRandom.DefaultRNG.Next(RoomMinSize, RoomMaxSize + 1);
                var height = GlobalRandom.DefaultRNG.Next(RoomMinSize, RoomMaxSize + 1);

                var x = GlobalRandom.DefaultRNG.Next(0, context.Width - width);
                var y = GlobalRandom.DefaultRNG.Next(0, context.Height - height);

                var perimeter = new Rectangle(x, y, width, height);

                if (roomsContext.Any(r => perimeter.Intersects(r.Perimeter)))
                    continue;

                roomsContext.Add(new RectangularRoom(perimeter));
            }

            yield return null;
        }
    }
}
