using System;

namespace BTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary(2);
            dict.Insert("b", "wtf");
            dict.Insert("a", "flow");
            dict.Insert("c", "ok");
            dict.Insert("d", "jest");
            dict.Insert("e", "grau");
            dict.Insert("f", "pay");
            bool check = dict.KeyExist("d");
            check = dict.KeyExist("e");
            var str = dict.GetValue("d");
            dict.ChangeValue("d", "veryJest");
            str = dict.GetValue("d");
        }
    }
}
