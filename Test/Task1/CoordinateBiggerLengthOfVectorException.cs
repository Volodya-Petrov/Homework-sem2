using System;
using System.Runtime.Serialization;

namespace Task1
{   
    /// <summary>
    /// кидается когда есть координата больше либо равна длине вектора
    /// </summary>
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
