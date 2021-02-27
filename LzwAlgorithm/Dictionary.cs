using System;
using System.Collections;

namespace LzwAlgorithm
{
    class Dictionary
    {   
        public Dictionary()
        {
            for (int i = 0; i < 256; i++)
            {
                var newByte = new byte[] { (byte)i };
                Console.WriteLine(i);
                Add(newByte);
                
            }
        }

        private class Node
        {
            private int code;
            private Hashtable hashtable;
            
            public int Code
            {
                get
                {
                    return code;
                }

                set
                {
                    code = value;
                }
            }

            public Node()
            {
                hashtable = new Hashtable();
            }

            public Node(int code) : this()
            {
                this.code = code;
            }

            public void AddChild(byte child, int childsCode)
            {
                hashtable.Add(child, new Node(childsCode));
            }

            public bool ContainsChild(byte child)
            {
                return hashtable.Contains(child);
            }

            public Node GetChild(byte child)
            {
                return (Node)hashtable[child];
            }
        }

        private int count;
        private Node root = new Node();

        public int Count
        {
            get
            {
                return count;
            }
        }
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