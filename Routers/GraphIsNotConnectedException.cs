using System;
using System.Runtime.Serialization;


namespace Routers
{   
    /// <summary>
    /// исключение, бросается, если граф не связанный
    /// </summary>
    [Serializable]
    public class GraphIsNotConnectedException : Exception
    {
        public GraphIsNotConnectedException() { }

        public GraphIsNotConnectedException(string message) : base(message) { }

        public GraphIsNotConnectedException(string message, Exception inner) : base(message, inner) { }

        protected GraphIsNotConnectedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
