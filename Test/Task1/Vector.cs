using System;
using System.Collections.Generic;

namespace Task1
{
    /// <summary>
    ///  разреженный вектор
    /// </summary>
    public class Vector
    {   
        public Vector(Dictionary<int, int> dict, int length)
        {   
            foreach (var key in dict.Keys)
            {
                if (key >= length || key < 0)
                {
                    throw new CoordinateBiggerLengthOfVectorException();
                }
            }
            this.dict = dict;
            Length = length;
        }
        Dictionary<int, int> dict;
        public int Length { get; }

        public int this[int i]
        {
            get => dict[i];
            set => dict[i] = value;
        }

        /// <summary>
        /// метод сложения векторов
        /// </summary>
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new VectorsLengthsAreNotEqualException();
            }
            var resultDict = new Dictionary<int, int>();
            foreach (var key in vector1.dict.Keys)
            {
                if (vector2.dict.ContainsKey(key))
                {   
                    if (vector2.dict[key] + vector1.dict[key] != 0)
                    {
                        resultDict.Add(key, vector2.dict[key] + vector1.dict[key]);
                        continue;
                    }
                }
                resultDict.Add(key, vector1.dict[key]);
            }
            foreach (var key in vector2.dict.Keys)
            {
                if (!vector1.dict.ContainsKey(key))
                {
                    resultDict.Add(key, vector2.dict[key]);
                    continue;
                }
            }
            return new Vector(resultDict, vector1.Length);
        }

        /// <summary>
        /// скалярное умножение векторов
        /// </summary>
        public static int operator *(Vector vector1, Vector vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new VectorsLengthsAreNotEqualException();
            }
            var result = 0;
            foreach (var key in vector1.dict.Keys)
            {
                if (vector2.dict.ContainsKey(key))
                {
                    result += vector1.dict[key] * vector2.dict[key];
                }
            }
            return result;
        }

        /// <summary>
        /// проверяет нулевой ли вектор
        /// </summary>
        /// <returns></returns>
        public bool IsNull()
        {
            return dict.Count == 0;
        }
    }
}
