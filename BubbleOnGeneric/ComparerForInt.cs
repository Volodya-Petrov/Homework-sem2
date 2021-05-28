using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BubbleOnGeneric
{   
    /// <summary>
    /// компаратор для целых чисел
    /// </summary>
    public class ComparerForInt : Comparer<int>
    {   
        /// <summary>
        /// сравнивает числа в обратном порядке
        /// </summary>
        public override int Compare([AllowNull] int x, [AllowNull] int y)
        {
            return x.CompareTo(y) * -1;
        }
    }
}
