using System;
using System.Collections.Generic;

namespace ToolsForList
{
    public class FunctionsForList
    {
        /// <summary>
        /// Возвращает список, полученный применением переданной функции к каждому элементу переданного списка
        /// </summary>
        public static List<int> Map(List<int> list, Func<int, int> function)
        {
            var newList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                newList.Add(function(list[i]));
            }
            return newList;
        }

        /// <summary>
        /// Filter принимает список и функцию, возвращающую булевое значение по элементу списка.
        /// Возвращаться должен список, составленный из тех элементов переданного списка,
        /// для которых переданная функция вернула true.
        /// </summary>
        public static List<int> Filter(List<int> list, Func<int, bool> function)
        {
            var newList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (function(list[i]))
                {
                    newList.Add(list[i]);
                }
            }
            return newList;
        }

        /// <summary>
        /// принимает список, начальное значение и функцию,
        /// которая берёт текущее накопленное значение и текущий элемент списка,
        /// и возвращает следующее накопленное значение, результат этой функции - накопленное значение
        /// после прохода всех элементов списка через function
        /// </summary>
        public static int Fold(List<int> list, int startValue, Func<int, int, int> function)
        {
            for (int i = 0; i < list.Count; i++)
            {
                startValue = function(startValue, list[i]);
            }
            return startValue;
        }
    }
}
