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
            var alphabetOfString = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (alphabetOfString.IndexOf(str[i]) < 0)
                {
                    alphabetOfString += str[i];
                }
            }
            var resultArray = alphabetOfString.ToCharArray();
            Array.Sort(resultArray);
            return resultArray;
        }

        static int[] GetCountOfSymbols(string str)
        {
            char[] alphabetOfString = GetAlphabetArray(str);
            var countOfSymbols = new int[alphabetOfString.Length];
            for (int i = 0; i < alphabetOfString.Length; i++)
            {
                countOfSymbols[i] = str.Where(x => x == alphabetOfString[i]).Count();
            }
            return countOfSymbols;
        }

        static int[] GetNumeration(string str)
        {
            var alphabetOfString = GetAlphabetArray(str);
            var countOfSymbols = GetCountOfSymbols(str);
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

        public static string ReverseBWT(string str)
        {
            var numerationArray = GetNumeration(str);
            string resultStr = "";
            int currentIndex = 0;
            while (currentIndex != Array.IndexOf(numerationArray, 0))
            {
                resultStr += str[currentIndex];
                currentIndex = numerationArray[currentIndex];
            }
            resultStr += str[Array.IndexOf(numerationArray, 0)];
            var charArrayOfString = resultStr.ToCharArray();
            Array.Reverse(charArrayOfString);
            return new string(charArrayOfString);
        }

        public static bool TestForReverseBWT()
        {
            string testString = "ркккрр$ааа";
            return "каркаркар$" == ReverseBWT(testString);
        }

        public static string BWTransformation(string str)
        {
            var suffixArray = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                suffixArray[i] = i;
            }
            for (int i = 1; i < suffixArray.Length; i++)
            {
                for (int j = 0; j < suffixArray.Length - i; j++)
                {
                    int index1 = suffixArray[j];
                    int index2 = suffixArray[j + 1];
                    string str1 = str.Substring(index1) + str.Substring(0, index1);
                    string str2 = str.Substring(index2) + str.Substring(0, index2);
                    if (String.Compare(str1, str2) > 0)
                    {
                        int helperForSwap = suffixArray[j];
                        suffixArray[j] = suffixArray[j + 1];
                        suffixArray[j + 1] = helperForSwap;
                    }
                }
            }
            string resultString = "";
            for (int i = 0; i < suffixArray.Length; i++)
            {
                if (suffixArray[i] == 0)
                {
                    resultString += str[str.Length - 1];
                }
                else
                {
                    resultString += str[suffixArray[i] - 1];
                }
            }
            return resultString;
        }
    }
}
