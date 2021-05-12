using System;
using System.Runtime.Serialization;

namespace WorkWithLists
{
    /// <summary>
    /// исключения для UniqeList, бросается, если в списке уже есть добавляемый элемент
    /// </summary>
    [Serializable]
    public class ValueAlreadyExistException : Exception
    {
        public ValueAlreadyExistException() { }

        public ValueAlreadyExistException(string message) : base(message) { }

        public ValueAlreadyExistException(string message, Exception inner) : base(message, inner) { }

        protected ValueAlreadyExistException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
