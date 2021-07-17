using System;

namespace TutorialRoguelike
{
    public class ImpossibleException : Exception
    {
        public ImpossibleException(string message) : base(message) {  }
    }
}
