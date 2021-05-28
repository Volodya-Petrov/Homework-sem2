using System.Collections.Generic;

namespace BubbleOnGeneric
{
    /// <summary>
    /// класс сортировок
    /// </summary>
    public static class Sort
    {
        /// <summary>
        /// сортировка пузырьком
        /// </summary>
        public static void BubbleSort<T>(List<T> list, Comparer<T> comparer)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (comparer.Compare(list[j], list[j + 1]) >= 0)
                    {
                        var helper = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = helper;
                    }
                }
            }
        }
    }
}