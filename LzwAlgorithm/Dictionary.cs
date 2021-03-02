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
            for (int i = 0; i < 256; i++)
            {
                var newByte = new byte[] { (byte)i };
                Add(newByte);
            }
        }

        private class Node
        {
            private int code;
            private Hashtable hashtable;
            
            public int Code
            {
                get => code;

                set => code = value;
            }

            public Node()
                => hashtable = new Hashtable();

            public Node(int code) : this()
                => this.code = code;

            public void AddChild(byte child, int childsCode)
                => hashtable.Add(child, new Node(childsCode));

            public bool ContainsChild(byte child)
                => hashtable.Contains(child);

            public Node GetChild(byte child)
                => (Node)hashtable[child];
        }

        private int count;
        private Node root = new Node();

        /// <summary>
        /// выдает кол-во элементов в словаре
        /// </summary>
        public int Count
        {
            get => count;
        }

        /// <summary>
        /// проверяет содержит ли словарь данный байтовый ключ
        /// </summary>
        public bool Contains(byte[] bytes)
        {
            int index = 0;
            Node currentNode = root;
            while (index < bytes.Length)
            {
                if (!currentNode.ContainsChild(bytes[index]))
                {
                    return false;
                }
                currentNode = currentNode.GetChild(bytes[index]);
                index++;
            }
            return true;
        }

        /// <summary>
        /// добавляет ключ в словарь
        /// </summary>
        /// <param name="bytes">байтовый ключ</param>
        public void Add(byte[] bytes)
        {
            int index = 0;
            var currentNode = root;
            while (index != bytes.Length - 1)
            {
                currentNode = currentNode.GetChild(bytes[index]);
                index++;
            }
            currentNode.AddChild(bytes[index], count++);
        }

        /// <summary>
        /// возвращает код по ключу
        /// </summary>
        /// <param name="bytes">ключ</param>
        public int GetCode(byte[] bytes)
        {
            int index = 0;
            var currentNode = root;
            while (index < bytes.Length)
            {
                currentNode = currentNode.GetChild(bytes[index]);
                index++;
            }
            return currentNode.Code;
        }
    }
}