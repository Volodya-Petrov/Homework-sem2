using System;

namespace BWTmethod
{
    class Program
    {
        static void Main(string[] args)
        {   
            if (!BWT.TestForBWT())
            {
                Console.WriteLine("Тесты провалены");
                return;
            }
            Console.WriteLine("Тесты успешно пройдены");
            Console.WriteLine("Введите строку:");
            string str = Console.ReadLine();
            Console.Write("Преобразованная строка: ");
            Console.WriteLine(BWT.BWTransformation(str).transformedString);
            Console.Write("Результат обратного BWT: ");
            Console.WriteLine(BWT.ReverseBWT(BWT.BWTransformation(str)));
        }
    }
}
