using System;

namespace Routers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до файла с графом");
            var path1 = Console.ReadLine();
            Console.WriteLine("Введите путь до файла куда нужно будеть записать результат");
            var path2 = Console.ReadLine();
            PrimAlgorithm.WriteMaximunSpanningTree(path1, path2);
        }
    }
}
