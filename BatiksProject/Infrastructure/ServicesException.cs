using System;

namespace BatiksProject.Infrastructure
{
    public class ServicesException : Exception
    {
        public ServicesException()
        {
        }

        public ServicesException(string message)
            : base(message)
        {
        }

        public ServicesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
