using System;

namespace Core
{
    [Serializable]
    public class CustomException : Exception
    {
        public CustomException(string message)
            : base(message)
        {
        }
    }
}
