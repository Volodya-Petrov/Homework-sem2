using System;
using System.Runtime.Serialization;

namespace Task1
{
    [Serializable]
    public class CoordinateBiggerLengthOfVectorException : Exception
    {
        public CoordinateBiggerLengthOfVectorException() { }

        public CoordinateBiggerLengthOfVectorException(string message) : base(message) { }

        public CoordinateBiggerLengthOfVectorException(string message, Exception inner) : base(message, inner) { }

        protected CoordinateBiggerLengthOfVectorException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
