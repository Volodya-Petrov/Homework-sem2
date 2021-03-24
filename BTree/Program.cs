using System;

namespace BTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary(2);
            dict.Insert("2", "2");
            dict.Insert("1", "1");
            dict.Insert("3", "3");
            dict.Insert("4", "4");
            dict.Insert("5", "5");
            dict.Insert("6", "6");
            dict.Insert("7", "7");
            dict.Insert("8", "8");
            dict.Insert("9", "9");
            dict.Remove("2");
        }
    }
}
