using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BubbleOnGeneric
{   
    /// <summary>
    /// компаратор для массива строк
    /// </summary>
    public class ComparerOfArrayOfString : Comparer<string[]>
    {   
        /// <summary>
        /// Сравнивает массивы строк. Больше тот массив, у которого больше длина.
        /// </summary>
        public override int Compare([AllowNull] string[] x, [AllowNull] string[] y)
        {
            return x.Length.CompareTo(y.Length);
        }
    }
}
