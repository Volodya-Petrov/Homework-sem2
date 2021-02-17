using System;

namespace BWTmethod
{
    class Program
    {
        static void Main(string[] args)
        {   
            if (!BWT.TestForReverseBWT())
            {
                Console.WriteLine("Тесты провалены");
            }
            Console.WriteLine("Тесты успешно пройдены");
            Console.WriteLine(BWT.ReverseBWT("$"));
        }
    }
}
