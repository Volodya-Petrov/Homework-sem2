﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace LzwAlgorithm
{   
    /// <summary>
    /// класс для сжатия файлов
    /// </summary>
    public static class LZW
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

        /// <summary>
        /// сжимает файл
        /// </summary>
        /// <param name="path">путь до файла, который надо сжать</param>
        public static void Lzw(string path)
        {
            var dict = new Dictionary();
            var output = new List<int>();
            using var baseFile = File.OpenRead(path);
            for (int i = 0; i < baseFile.Length; i++)
            {
                var currentByte = (byte)baseFile.ReadByte();
                var codeOfBytes = dict.Add(currentByte);
                if (codeOfBytes != -1)
                {
                    output.Add(codeOfBytes);
                    dict.Add(currentByte);
                }
            }
            output.Add(dict.GetCode());
            path += ".zipped";
            var bytesForMaxCode = BitConverter.GetBytes(dict.Count);
            var countOfBytes = GetCountOfSignificantBytes(bytesForMaxCode);
            using var zippedFile = new FileStream(path, FileMode.CreateNew);
            zippedFile.WriteByte((byte)countOfBytes);
            for (int i = 0; i < output.Count; i++)
            {
                var helpArray = BitConverter.GetBytes(output[i]);
                zippedFile.Write(helpArray, 0, countOfBytes);
            }
            // конечно сборщик их закроет, но мне кидает ошибку в обратном лзв, что есть потоки юзающие эти файлы
            zippedFile.Close();
            baseFile.Close();
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

        private static void AddLastSymbolToDictionary(int code, Hashtable hashtable)
        {
            var bytesArray = (byte[])hashtable[code];
            var maxCode = hashtable.Count;
            var lastAdded = (byte[])hashtable[maxCode - 1];
            lastAdded[lastAdded.Length - 1] = bytesArray[0];
        }

        /// <summary>
        /// востанавливает исходный файл, который был сжат
        /// </summary>
        /// <param name="path">путь до файла</param>
        public static void ReverseLzw(string path)
        {
            var hashtable = InitHashtable();
            int maxCode = hashtable.Count;
            using var baseFile = File.OpenRead(path);
            path = path.Substring(0, path.Length - 7);
            int period = baseFile.ReadByte();
            using var decompressedFile = new FileStream(path, FileMode.CreateNew);
            for (int i = 1; i < baseFile.Length; i += period)
            {   
                // BitConventer.ToInt32 не будет работать с массивом байт, длина которого = 3
                var codeInBytes = new byte[4];
                for (int j = 0; j < period; j++)
                {
                    codeInBytes[j] = (byte)baseFile.ReadByte();
                }
                var code = BitConverter.ToInt32(codeInBytes, 0);
                if (i != 1)
                {
                    AddLastSymbolToDictionary(code, hashtable);
                }
                var bytesArray = (byte[])hashtable[code];
                var copyOfBytesArray = new byte[bytesArray.Length + 1];
                Array.Copy(bytesArray, copyOfBytesArray, bytesArray.Length);
                hashtable.Add(maxCode, copyOfBytesArray);
                maxCode++;
                decompressedFile.Write(bytesArray);
            }
            baseFile.Close();
            decompressedFile.Close();
        }  
    }
}
