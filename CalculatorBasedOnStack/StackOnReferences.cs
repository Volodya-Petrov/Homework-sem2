using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorBasedOnStack
{
    class StackOnReferences
    {
        private class StackElement
        {
            public StackElement(double value) => Value = value;

            public StackElement Next { get; set; }

            public double Value { get; set; }
        }

        private StackElement head;

        public void Push(double number)
        {
            if (head == null)
            {
                head = new StackElement(number);
                return;
            }
            var newElement = new StackElement(number);
            newElement.Next = head;
            head = newElement;
        }

        public double Pop()
        {
            if (head == null)
            {

            }
            var numberToReturn = head.Value;
            head = head.Next;
            return numberToReturn;
        }

        public bool IsEmpty() => head == null;
    }
}
