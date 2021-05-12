using System;
using System.Collections.Generic;
using System.Text;

namespace WorkWithLists
{   
    /// <summary>
    /// интерфейс для списка
    /// </summary>
    public interface IList
    {   
        /// <summary>
        /// получение и измение элемента по индексу
        /// </summary>
        public int this[int i] { get; set; }

        /// <summary>
        /// длина списка
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// добавляет элемент в список
        /// </summary>
        public void Add(int value);

        /// <summary>
        /// вставляет элемент в список по заданному индексу
        /// </summary>
        public void Insert(int index, int value);

        /// <summary>
        /// удаление элемента из списка
        /// </summary>
        public void Remove(int index);
    }
}
