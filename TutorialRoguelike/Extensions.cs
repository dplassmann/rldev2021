using System;
using GoRogue.Random;

namespace TutorialRoguelike
{
    public static class Extensions
    {
        public static T RandomChoice<T>() where T : Enum
        {
            var directions = Enum.GetValues(typeof(T));
            var index = GlobalRandom.DefaultRNG.Next(directions.Length);
            return (T) directions.GetValue(index);
        }
    }
}
