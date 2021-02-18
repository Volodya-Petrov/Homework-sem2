using System;

namespace BWTmethod
{
    class Program
    {
        static void Main(string[] args)
        {   
            /*if (!BWT.TestForReverseBWT())
            {
                Console.WriteLine("Тесты провалены");
            }*/
            Console.WriteLine("Тесты успешно пройдены");
            string str = Console.ReadLine();
            Console.WriteLine(BWT.BWTransformation(str));
            Console.WriteLine(BWT.ReverseBWT(BWT.BWTransformation(str)));
        }
    }
}
