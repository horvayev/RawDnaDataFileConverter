using System;

namespace DnaFormatConverter.Exceptions
{
    public class InvalidFormatException : Exception
    {
        public InvalidFormatException() : base("Invalid format exception")
        {
        }

        public InvalidFormatException(string message) : base(message)
        {            
        }

        public InvalidFormatException(string message, Exception inner) : base(message, inner)
        {            
        }
    }
}