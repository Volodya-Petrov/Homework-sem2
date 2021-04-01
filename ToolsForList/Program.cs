using System;
using System.Collections.Generic;

namespace ToolsForList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>() { 1, 2, 3, 4 };
            var newList = FunctionsForList.Filter(list, x => (x % 2) == 0);
        }
    }
}
