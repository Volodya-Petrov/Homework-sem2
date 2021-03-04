using System;

namespace CalculatorBasedOnStack
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new StackOnReferences();
            var check = stack.IsEmpty();
            for (int i = 0; i < 5; i++)
            {
                stack.Push(i);
            }
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(stack.Pop());
            }
            check = stack.IsEmpty();
            Console.WriteLine(stack.Pop());
            check = stack.IsEmpty();
        }
    }
}
