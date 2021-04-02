using System;
using System.Runtime.Serialization;

namespace Task1
{
    [Serializable]
    public class VectorsLengthsAreNotEqualException : Exception
    {
        public VectorsLengthsAreNotEqualException() { }

        public VectorsLengthsAreNotEqualException(string message) : base(message) { }

        public VectorsLengthsAreNotEqualException(string message, Exception inner) : base(message, inner) { }

        protected VectorsLengthsAreNotEqualException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
