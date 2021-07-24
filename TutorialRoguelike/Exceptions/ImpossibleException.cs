using System;

namespace TutorialRoguelike.Exceptions
{
    public class ImpossibleException : Exception
    {
        public ImpossibleException(string message) : base(message) { }
    }
}
