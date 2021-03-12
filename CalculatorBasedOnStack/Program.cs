using System;

namespace CalculatorBasedOnStack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку в постфикснов виде:");
            var postFixString = Console.ReadLine();
            Console.WriteLine("Через какой стек нужно посчитать?");
            Console.WriteLine("Введите '1' - стек на списках, '2' - стек на ссылках");
            var key = Console.ReadLine();
            IStack stack = key == "1" ? new StackOnList() : new StackOnReferences();
            var result = StackCalculator.CalculatePostfixForm(postFixString, stack);
            Console.WriteLine($"Результат работы: {result}");
        }
    }
}
