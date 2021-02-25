using System;
using System.Collections;
using System.Text;

namespace LzwAlgorithm
{
    class LZW
    {
        public static string Lzw(string str)
        {
            var dict = new Dictionary();
            var resultString = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (!dict.Contains(str[i].ToString()))
                {
                    dict.Add(str[i].ToString());
                    resultString += str[i].ToString();
                }
            }
            resultString = dict.GetElementsCount().ToString() + " " + resultString;
            var currentString = "";
            for (int i = 0; i < str.Length; i++)
            {
                currentString += str[i];
                if (!dict.Contains(currentString))
                {
                    var substring = currentString.Substring(0, currentString.Length - 1);
                    resultString += dict.GetCode(substring).ToString() + " ";
                    dict.Add(currentString);
                    currentString = currentString[currentString.Length - 1].ToString();
                }
            }
            resultString += dict.GetCode(currentString).ToString();
            return resultString;
        }

        private static Hashtable InitHashtable(string str)
        {
            int startIndex = 0;
            int code = 0;
            while (str[startIndex] != ' ')
            {
                startIndex++;
            }
            int sizeOfAlphabet = int.Parse(str.Substring(0, startIndex));
            var hashtable = new Hashtable();
            for (int i = startIndex + 1; i < startIndex + sizeOfAlphabet + 1; i++)
            {
                hashtable.Add(code++, str[i].ToString());
            }
            return hashtable;
        }

        private static int GetStartIndex(string str)
        {
            int startIndex = 0;
            while (str[startIndex] != ' ')
            {
                startIndex++;
            }
            int sizeOfAlphabet = int.Parse(str.Substring(0, startIndex));
            return startIndex + sizeOfAlphabet + 1;
        }

        public static string reverseLzw(string str)
        {
            var hashtable = InitHashtable(str);
            int maxCode = hashtable.Count;
            var resultString = "";
            int startIndex = GetStartIndex(str);
            var array = str.Substring(startIndex, str.Length - startIndex).Split(' ');
            var codes = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                codes[i] = int.Parse(array[i]);
            }
            for (int i = 0; i < codes.Length; i++)
            {
                int code = codes[i];
                string helperString = (string)hashtable[code];
                if (i != 0)
                {
                    hashtable[maxCode - 1] = (string)hashtable[maxCode - 1] + helperString[0];
                }
                helperString = (string)hashtable[code];
                hashtable.Add(maxCode++, helperString);
                resultString += helperString;
            }
            return resultString;
        }
    }
}
