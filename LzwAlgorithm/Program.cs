using System;

namespace LzwAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("first");
            LZW.Lzw(Console.ReadLine());
            Console.WriteLine("second");
            LZW.ReverseLzw(Console.ReadLine());
            Console.WriteLine("Все ок братан");
        }
    }
}
