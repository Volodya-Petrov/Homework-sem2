using System;
using System.Collections.Generic;
using System.Collections;
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

        private static Hashtable InitHashtable()
        {
            var hashtable = new Hashtable();
            for (int i = 0; i < 256; i++)
            {
                hashtable.Add(i, new byte[] { (byte)i });
            }
            return hashtable;
        }

        public static void ReverseLzw(string path)
        {
            var hashtable = InitHashtable();
            int maxCode = hashtable.Count;
            using (FileStream baseFile = File.OpenRead(path))
            {
                path = path.Substring(0, path.Length - 7);
                int period = baseFile.ReadByte();
                using (FileStream decompressedFile = new FileStream(path, FileMode.OpenOrCreate))
                {
                    for (int i = 1; i < baseFile.Length; i += 3)
                    {   
                        var codeInBytes = new byte[period + 3];
                        for (int j = 0; j < period; j++)
                        {
                            codeInBytes[j] = (byte)baseFile.ReadByte();
                        }
                        var code = BitConverter.ToInt32(codeInBytes, 0);
                        byte[] bytesArray = (byte[])hashtable[code];
                        if (i != 1)
                        {
                            var lastAdded = (byte[])hashtable[maxCode - 1];
                            lastAdded[lastAdded.Length - 1] = bytesArray[0];
                            hashtable[maxCode - 1] = lastAdded;
                        }
                        bytesArray = (byte[])hashtable[code];
                        var copyOfBytesArray = new byte[bytesArray.Length + 1];
                        Array.Copy(bytesArray, copyOfBytesArray, bytesArray.Length);
                        hashtable.Add(maxCode++, copyOfBytesArray);
                        decompressedFile.Write(bytesArray);
                    }   
                }
            }
        }  
    }
}
