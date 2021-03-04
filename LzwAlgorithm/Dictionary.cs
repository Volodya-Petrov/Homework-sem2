using System;
using System.Collections;

namespace LzwAlgorithm
{   
    /// <summary>
    /// генерит и хранит коды байтовых массивов
    /// </summary>
    class Dictionary
    {   
        public Dictionary()
        {
            currentNode = root;
            for (int i = 0; i < 256; i++)
            {
                Add((byte)i);
            }
        }

        private class Node
        {
            private Hashtable hashtable;
            
            public int Code { get; set; }

            public Node()
                => hashtable = new Hashtable();

            public Node(int code) : this()
                => Code = code;

            public void AddChild(byte child, int childsCode)
                => hashtable.Add(child, new Node(childsCode));

            public bool ContainsChild(byte child)
                => hashtable.Contains(child);

            public Node GetChild(byte child)
                => (Node)hashtable[child];
        }

        private int count;
        private Node root = new Node();
        private Node currentNode;

        /// <summary>
        /// выдает кол-во элементов в словаре
        /// </summary>
        public int Count => count;

       /// <summary>
       ///  добавляет ключ в словарь
       /// </summary>
       /// <param name="byteForAdd">байтовый ключ</param>
       /// <returns>возращает -1, если ключ есть уже в словаре, код предыдущего байта, если его нет</returns>
        public int Add(byte byteForAdd)
        {
            if (currentNode.ContainsChild(byteForAdd))
            {
                currentNode = currentNode.GetChild(byteForAdd);
                return -1;
            }
            currentNode.AddChild(byteForAdd, count);
            count++;
            var codeForReturn = currentNode.Code;
            currentNode = root;
            return codeForReturn;
        }

        /// <summary>
        /// возвращает код текущих байтов
        /// </summary>
        public int GetCode() => currentNode.Code;
    }
}