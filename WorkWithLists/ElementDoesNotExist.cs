using System;
using System.Runtime.Serialization;

namespace WorkWithLists
{
    /// <summary>
    /// исключения для списков, бросается, при попытке удалить элемент на несуществующей позиции
    /// </summary>
    [Serializable]
    public class ElementDoesNotExist : Exception
    {
        public ElementDoesNotExist() { }

        public ElementDoesNotExist(string message) : base(message) { }

        public ElementDoesNotExist(string message, Exception inner) : base(message, inner) { }

        protected ElementDoesNotExist(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
