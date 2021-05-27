using System;
using System.Collections.Generic;

namespace BTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<int, int[]>(3);
            dict.Add(1, new[] { 0 });
            var check = dict.Contains(new (1, new[] { 0 }));
        }
    }
}
