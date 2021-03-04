using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorBasedOnStack
{
    class StackOnList
    {
        public StackOnList() => stack = new List<double>();
        private List<double> stack;

        public void Push(double number) => stack.Insert(0, number);

        public double Pop()
        {
            if (IsEmpty())
            {

            }
            var numberToReturn = stack[0];
            stack.RemoveAt(0);
            return numberToReturn;
        }

        public bool IsEmpty() => stack.Count == 0;
    }
}
