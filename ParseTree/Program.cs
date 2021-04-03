using System;

namespace ParseTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до файла с арифметическим выражением:");
            var str = Console.ReadLine();
            var tree = new Tree(str);
            Console.WriteLine($"Выражение: {tree.Print()}");
            Console.WriteLine($"Значение выражения: {tree.Calculate()}");
        }
    }
}
