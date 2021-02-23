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
        }

        private Node root;
        private int maxCode;

        public bool Contains(string str)
        {
            int index = 0;
            Node currentNode = root;
            while (index < str.Length)
            {
                if (!root.hashtable.Contains(str[index]))
                {
                    return false;
                }
                currentNode = (Node)root.hashtable[str[index]];
                index++;
            }
            return true;
        }

        public void Add(string str)
        {
            int index = 0;
            var currentNode = root;
            while (index != str[str.Length - 1])
            {
                currentNode = (Node)root.hashtable[str[index]];
                index++;
            }
            currentNode.hashtable.Add(str[index], new Node { symbol = str[index], code = maxCode++ });
        }
    }
}
