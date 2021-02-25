using System;

namespace LzwAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = Console.ReadLine();
            var str = LZW.Lzw(test);
            var newStr = LZW.reverseLzw(str);
            if (newStr == test)
            {
                Console.WriteLine("%$##$, чотко");
            }
            else
            {
                Console.WriteLine("Переделывай пацан");
            }
        }
    }
}
