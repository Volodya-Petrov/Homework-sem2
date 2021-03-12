using System;

namespace CalculatorBasedOnStack
{   
    /// <summary>
    /// Структура данных по типу first in last out
    /// </summary>
    class StackOnReferences : IStack
    {
        private class StackElement
        {
            public StackElement(double value) => Value = value;

            public StackElement Next { get; set; }

            public double Value { get; set; }
        }

        private StackElement head;

        /// <summary>
        /// добавляет элемент в стек
        /// </summary>
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

        /// <summary>
        /// снимает элемент со стека
        /// </summary>
        public double Pop()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Стек пустой");
            }
            var numberToReturn = head.Value;
            head = head.Next;
            return numberToReturn;
        }

        /// <summary>
        /// проверяет пуст ли стек
        /// </summary>
        public bool IsEmpty() => head == null;
    }
}
