using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BWTmethod
{
    class BWT
    {   
        static char[] GetAlphabetArray(string str)
        {
            var alphabetOfString = "$";
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (alphabetOfString.IndexOf(str[i]) < 0)
                {
                    alphabetOfString += str[i];
                }
            }
            var resultArray = alphabetOfString.ToCharArray();
            Array.Sort(resultArray, 1, resultArray.Length - 1);
            return resultArray;
        }

        static int[] GetCountOfSymbols(char[] alphabetOfString, string str)
        {
            var countOfSymbols = new int[alphabetOfString.Length];
            countOfSymbols[0] = 1;
            for (int i = 1; i < alphabetOfString.Length; i++)
            {
                countOfSymbols[i] = str.Where(x => x == alphabetOfString[i]).Count();
            }
            return countOfSymbols;
        }

        static int[] GetNumeration(string str)
        {
            var alphabetOfString = GetAlphabetArray(str);
            var countOfSymbols = GetCountOfSymbols(alphabetOfString, str);
            var helperArrayOfNumeration = new int[countOfSymbols.Length];
            helperArrayOfNumeration[0] = 0;
            for (int i = 1; i < countOfSymbols.Length; i++)
            {
                helperArrayOfNumeration[i] = helperArrayOfNumeration[i - 1] + countOfSymbols[i - 1];
            }
            var arrayOfNumeration = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                int index = Array.IndexOf(alphabetOfString, str[i]);
                arrayOfNumeration[i] = helperArrayOfNumeration[index];
                helperArrayOfNumeration[index]++;
            }
            return arrayOfNumeration;
        }

        static string ReverseBWT(string str)
        {
            return "";
        }
    }
}
