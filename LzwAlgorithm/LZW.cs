using System;
using System.Collections.Generic;
using System.IO;

namespace LzwAlgorithm
{
    class LZW
    {   
        private static int GetCountOfSignificantBytes(byte[] byteArray)
        {
            for (int i = byteArray.Length - 1; i >= 0; i--)
            {
                if (byteArray[i] != 0)
                {
                    return i + 1;
                }
            }
            return 1;
        }

        public static void Lzw(string path)
        {
            var dict = new Dictionary();
            var output = new List<int>();
            var currentBytes = new List<byte>();
            using (FileStream baseFile = File.OpenRead(path))
            {
                for (int i = 0; i < baseFile.Length; i++)
                {
                    currentBytes.Add((byte)baseFile.ReadByte());
                    var currentBytesInArray = currentBytes.ToArray();
                    if (!dict.Contains(currentBytesInArray))
                    {
                        var subCurrentBytes = new byte[currentBytesInArray.Length - 1];
                        Array.Copy(currentBytesInArray, subCurrentBytes, currentBytesInArray.Length - 1);
                        output.Add(dict.GetCode(subCurrentBytes));
                        dict.Add(currentBytesInArray);
                        var helperList = new List<byte>();
                        helperList.Add(currentBytesInArray[currentBytesInArray.Length - 1]);
                        currentBytes = helperList;
                    }
                }
                output.Add(dict.GetCode(currentBytes.ToArray()));
            }
            path += ".zipped";
            var bytesForMaxCode = BitConverter.GetBytes(dict.Count);
            var countOfBytes = GetCountOfSignificantBytes(bytesForMaxCode);
            using (FileStream zippedFile = new FileStream(path, FileMode.OpenOrCreate))
            {
                zippedFile.WriteByte((byte)countOfBytes);
                for (int i = 0; i < output.Count; i++)
                {
                    var helpArray = BitConverter.GetBytes(output[i]);
                    zippedFile.Write(helpArray, 0, countOfBytes);
                }
            }
        }

        /*private static Hashtable InitHashtable(string str)
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
        }*/
    }
}
