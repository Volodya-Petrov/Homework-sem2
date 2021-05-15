using System;
using System.Collections.Generic;

namespace CalculatorBasedOnStack
{   
    /// <summary>
    /// структура данных first in last out
    /// </summary>
    public class StackOnList : IStack
    {
        public StackOnList() => stack = new List<double>();
        private List<double> stack;

        /// <summary>
        /// добавляет элемент в стек
        /// </summary>
        public void Push(double number) => stack.Insert(0, number);

        /// <summary>
        /// берет элемент со стека
        /// </summary>
        public double Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Стек пустой");
            }
            var numberToReturn = stack[0];
            stack.RemoveAt(0);
            return numberToReturn;
        }

        /// <summary>
        /// проверяет пуст ли стек
        /// </summary>
        public bool IsEmpty() => stack.Count == 0;
    }
}
