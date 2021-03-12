using System;
using System.IO;

namespace LzwAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[1] == "-c")
            {
                LZW.Lzw(args[0]);
                var basedFile = new FileInfo(args[0]);
                var compressedFile = new FileInfo(args[0] + ".zipped");
                Console.WriteLine("Файл сжат");
                Console.WriteLine($"Коэффициент сжатия: {(double)basedFile.Length / compressedFile.Length}");
            }
            else if (args[1] == "-u")
            {
                LZW.ReverseLzw(args[0]);
                Console.WriteLine("Файл разжат");
            }
            else
            {
                Console.WriteLine("Введен неверный ключ");
            }
        }
    }
}
