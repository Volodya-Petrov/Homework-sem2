using System;
using System.IO;

namespace LzwAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу:");
            var path = Console.ReadLine();
            Console.WriteLine("Введите ключ '-c', если хотите сжать файл. Введите ключ '-u', если хотите разжать файл");
            var key = Console.ReadLine();
            if (key == "-c")
            {
                LZW.Lzw(path);
                var basedFile = new FileInfo(path);
                var compressedFile = new FileInfo(path + ".zipped");
                Console.WriteLine("Файл сжат");
                Console.WriteLine("Коэффициент сжатия: {0}", (double)basedFile.Length / compressedFile.Length);
            }
            else if (key == "-u")
            {
                LZW.ReverseLzw(path);
                Console.WriteLine("Файл разжат");
            }
            else
            {
                Console.WriteLine("Введен неверный ключ");
            }
        }
    }
}
