using System;

namespace LzwAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary dict = new Dictionary();
            dict.Add("a");
            dict.Add("b");
            dict.Add("ab");
            dict.Add("abc");
            var code = dict.GetCode("ab");
            var contains = dict.Contains("abc");
            contains = dict.Contains("bc");
        }
    }
}
