using System;
using System.Collections;
using System.Text;

namespace LzwAlgorithm
{
    class Dictionary
    {   
        private class Node
        {
            public char symbol;
            public int code;
            public Hashtable hashtable;

            public Node()
            {
                this.hashtable = new Hashtable();
            }

            public Node(char symbol, int code) : this()
            {
                this.symbol = symbol;
                this.code = code;
            }
        }

        private int maxCode;
        private Node root = new Node();

        public bool Contains(string str)
        {
            int index = 0;
            Node currentNode = root;
            while (index < str.Length)
            {
                if (!currentNode.hashtable.Contains(str[index]))
                {
                    return false;
                }
                currentNode = (Node)currentNode.hashtable[str[index]];
                index++;
            }
            return true;
        }

        public void Add(string str)
        {
            int index = 0;
            var currentNode = root;
            while (index != str.Length - 1)
            {
                currentNode = (Node)currentNode.hashtable[str[index]];
                index++;
            }
            currentNode.hashtable.Add(str[index], new Node(str[index], maxCode++));
        }

        public int GetCode(string str)
        {
            int index = 0;
            var currentNode = root;
            while (index < str.Length)
            {
                currentNode = (Node)currentNode.hashtable[str[index]];
                index++;
            }
            return currentNode.code;
        }

        public int GetElementsCount()
        {
            return this.maxCode;
        }
    }
}
