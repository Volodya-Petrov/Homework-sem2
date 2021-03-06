﻿namespace WorkWithLists
{   
    /// <summary>
    /// список без повторяющихся значений
    /// </summary>
    public class UniqueList : List
    {   
        /// <summary>
        /// добавляет элемент в список, если его там нет
        /// </summary>
        public override void Add(int value)
        {
            if (Contains(value))
            {
                throw new ValueAlreadyExistException();
            }
            base.Add(value);
        }

        /// <summary>
        /// вставляет элемент по заданной позиции в список, если его нет в списке
        /// </summary>
        public override void Insert(int index, int value)
        {
            if (Contains(value))
            {
                throw new ValueAlreadyExistException();
            }
            base.Insert(index, value);
        }

        protected override void ChangeValue(int index, int newValue)
        {   
            if (Contains(newValue) || this[index] != newValue)
            {
                throw new ValueAlreadyExistException();
            }
            base.ChangeValue(index, newValue);
        }
    }
}
