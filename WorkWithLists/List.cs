﻿using System;

namespace WorkWithLists
{   
    /// <summary>
    /// стандартный список
    /// </summary>
    public class List : IList
    {   
        /// <summary>
        /// get выдает элемент по позиции в списке
        /// set меняет значение элемента по заданной позиции
        /// </summary>
        public int this[int i]
        {
            get => GetValue(i);
            set => ChangeValue(i, value);
        }

        private class Node
        {   
            public Node(int value, Node next)
            {
                Value = value;
                Next = next;
            }

            public int Value { get; set; }

            public Node Next { get; set; }
        }
        
        /// <summary>
        /// выдает кол-во элементов в списке
        /// </summary>
        public int Length { get => length; }

        private int length;
        private Node root;
        private Node tail;

        /// <summary>
        /// добавление элемента в конец списка
        /// </summary>
        public virtual void Add(int value)
        {
            length++;
            if (root == null)
            {
                root = new Node(value, null);
                tail = root;
                return;
            }
            tail.Next = new Node(value, null);
            tail = tail.Next;
        }

        /// <summary>
        /// вставка элемента по указанному индексу
        /// </summary>
        public virtual void Insert(int index, int value)
        {
            if (index < 0 || index > length)
            {
                throw new IndexOutOfRangeException();
            }
            if (index == 0)
            {
                var node = new Node(value, root);
                root = node;
                if (length == 0)
                {
                    tail = node;
                }
                length++;
                return;
            }
            var currentNode = root;
            for (int i = 0; i < index - 1; i++)
            {
                currentNode = currentNode.Next;
            }
            var newNode = new Node(value, currentNode.Next);
            currentNode.Next = newNode;
            if (currentNode == tail)
            {
                tail = newNode;
            }
            length++;
        }

        /// <summary>
        /// удаляет элемент по заданной позиции
        /// </summary>
        public void Remove(int index)
        {
            if (index < 0 || index >= length)
            {
                throw new ElementDoesNotExist();
            }
            if (index == 0)
            {
                if (length == 0)
                {
                    throw new ElementDoesNotExist();
                }
                if (length == 1)
                {
                    root = null;
                    tail = null;
                    length--;
                    return;
                }
                root = root.Next;
                length--;
                return;
            }
            var currentNode = root;
            for (int i = 0; i < index - 1; i++)
            {
                currentNode = currentNode.Next;
            }
            if (currentNode.Next == tail)
            {
                tail = currentNode;
                currentNode.Next = null;
                length--;
                return;
            }
            currentNode.Next = currentNode.Next.Next;
            length--;
        }

        private Node FindNode(int index)
        {
            if (index < 0 || index >= length)
            {
                throw new IndexOutOfRangeException();
            }
            var currentNode = root;
            int counter = 0;
            while (currentNode != null)
            {
                if (counter == index)
                {
                    return currentNode;
                }
                currentNode = currentNode.Next;
                counter++;
            }
            return null;
        }

        protected virtual void ChangeValue(int index, int newValue)
        {
            var node = FindNode(index);
            node.Value = newValue;
        }

        private int GetValue(int index)
        {
            var node = FindNode(index);
            return node.Value;
        }

        protected bool Contains(int value)
        {
            var currentNode = root;
            while (currentNode != null)
            {
                if (currentNode.Value == value)
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }
    }
}
