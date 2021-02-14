﻿using System;

namespace quadraticSorting
{
    class Program
    {
        static void Sort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int min = i;
                for (int j = i; j < array.Length; j++)
                {
                    if (array[min] > array[j])
                    {
                        min = j;
                    }
                }
                int helperSwap = array[i];
                array[i] = array[min];
                array[min] = helperSwap;
            }
        }
        static bool Test()
        {
            int[] array = { 4, 1, 6, 2, 7 };
            int[] sortedArray = { 1, 2, 4, 6, 7 };
            if (array.Length != sortedArray.Length)
            {
                return false;
            }
            Sort(array);
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != sortedArray[i])
                {
                    return false;
                }
            }
            return true;
        }
        
        static int[] readArray()
        {
            var str = Console.ReadLine();
            var numbers = str.Split(' ');
            var array = new int[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                array[i] = int.Parse(numbers[i]);
            }
            return array;
        }
        static void Main(string[] args)
        {   
            if (!Test())
            {
                Console.WriteLine("Тест провален!");
            }
            Console.WriteLine("Тест пройден успешно!");
            Console.WriteLine("Введите массив натуральных чисел: ");
            var array = readArray();
            Sort(array);
            Console.WriteLine("Отсортированный массив:");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
                Console.Write(" ");
            }
        }
    }
}
