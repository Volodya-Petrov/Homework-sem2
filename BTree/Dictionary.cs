using System;

namespace BTree
{
    class Dictionary
    {   
        private struct SplittedNode
        {   
            public SplittedNode(Node firstPart, Node secondPart, string key, string value)
            {
                firstPartOfNode = firstPart;
                secondPartOfNode = secondPart;
                keyForInsert = key;
                valueForInsert = value;
            }
            public Node firstPartOfNode;
            public Node secondPartOfNode;
            public string keyForInsert;
            public string valueForInsert;
        }

        public Dictionary(int degree)
        {
            degreeOfTree = degree;
            root = new Node(degree);
            root.Leaf = true;
        }
        private int degreeOfTree;
        private Node root;

        private class Node
        {
            public Node(int degree)
            {
                Keys = new string[2 * degree];
                Values = new string[2 * degree];
                Children = new Node[2 * degree + 1];
            }

            public Node Parent { get; set; }

            public bool Leaf { get; set; }

            public int CountOfKeys { get; set; }

            public string[] Keys { get; set; }

            public string[] Values { get; set; }

            public Node[] Children { get; set; }
        }

        public bool KeyExist(string key)
        {
            var currentNode = root;
            int indexForInsert;
            while (!currentNode.Leaf)
            {
                indexForInsert = FindIndexForInsert(currentNode, key);
                if (currentNode.Keys[indexForInsert] == key)
                {
                    return true;
                }
                currentNode = currentNode.Children[indexForInsert];
            }
            indexForInsert = FindIndexForInsert(currentNode, key);
            return key == currentNode.Keys[indexForInsert];
        }

        public string GetValue(string key)
        {
            var currentNode = root;
            int indexForInsert;
            while (!currentNode.Leaf)
            {
                indexForInsert = FindIndexForInsert(currentNode, key);
                if (currentNode.Keys[indexForInsert] == key)
                {
                    return currentNode.Values[indexForInsert];
                }
                currentNode = currentNode.Children[indexForInsert];
            }
            indexForInsert = FindIndexForInsert(currentNode, key);
            return key == currentNode.Keys[indexForInsert] ? currentNode.Values[indexForInsert] : null;
        }

        public void ChangeValue(string key, string newValue)
        {
            var currentNode = root;
            int indexForInsert;
            while (!currentNode.Leaf)
            {
                indexForInsert = FindIndexForInsert(currentNode, key);
                if (currentNode.Keys[indexForInsert] == key)
                {
                    currentNode.Values[indexForInsert] = newValue;
                    return;
                }
                currentNode = currentNode.Children[indexForInsert];
            }
            indexForInsert = FindIndexForInsert(currentNode, key);
            if (currentNode.Keys[indexForInsert] == key)
            {
                currentNode.Values[indexForInsert] = newValue;
            }
        }

        public void Insert(string key, string value)
        {
            var currentNode = FindNodeForInsert(root, key);
            AddKeyToNode(currentNode, key, value, null, null);
            while (currentNode.CountOfKeys > 2 * degreeOfTree - 1)
            {
                var splittedNode = SplitNode(currentNode);
                currentNode = splittedNode.firstPartOfNode.Parent;
                if (currentNode == null)
                {
                    currentNode = new Node(degreeOfTree);
                    currentNode.Keys[0] = splittedNode.keyForInsert;
                    currentNode.Values[0] = splittedNode.valueForInsert;
                    currentNode.CountOfKeys++;
                    currentNode.Children[0] = splittedNode.firstPartOfNode;
                    currentNode.Children[0].Parent = currentNode;
                    currentNode.Children[1] = splittedNode.secondPartOfNode;
                    currentNode.Children[1].Parent = currentNode;
                    root = currentNode;
                    return;
                }
                AddKeyToNode(currentNode, splittedNode.keyForInsert, splittedNode.valueForInsert, splittedNode.firstPartOfNode, splittedNode.secondPartOfNode);
            }

        }

        private SplittedNode SplitNode(Node node)
        {
            var firstPart = new Node(degreeOfTree);
            var secondPart = new Node(degreeOfTree);
            firstPart.Leaf = node.Leaf;
            firstPart.Parent = node.Parent;
            secondPart.Parent = node.Parent;
            secondPart.Leaf = node.Leaf;
            int separator = (node.CountOfKeys - 1) / 2;
            for (int i = 0; i < separator; i++)
            {
                firstPart.Values[i] = node.Values[i];
                firstPart.Keys[i] = node.Keys[i];
                firstPart.Children[i] = node.Children[i];
                firstPart.CountOfKeys++;
            }
            firstPart.Children[separator] = node.Children[separator];
            for (int i = 0; i < node.CountOfKeys - separator - 1; i++)
            {
                secondPart.Values[i] = node.Values[separator + 1 + i];
                secondPart.Keys[i] = node.Keys[separator + 1 + i];
                secondPart.Children[i] = node.Children[separator + 1 + i];
                secondPart.CountOfKeys++;
            }
            secondPart.Children[node.CountOfKeys - separator - 1] = node.Children[node.CountOfKeys];
            return new SplittedNode(firstPart, secondPart, node.Keys[separator], node.Values[separator]);
        }

        private void AddKeyToNode(Node node, string key, string value, Node firstPart, Node secondPart)
        {
            var indexForInsert = FindIndexForInsert(node, key);
            for (int i = node.CountOfKeys - 1; i >= indexForInsert; i--)
            {
                node.Keys[i + 1] = node.Keys[i];
                node.Values[i + 1] = node.Values[i];
                node.Children[i + 2] = node.Children[i + 1];
            }
            node.Keys[indexForInsert] = key;
            node.Values[indexForInsert] = value;
            node.Children[indexForInsert] = firstPart;
            node.Children[indexForInsert + 1] = secondPart;
            node.CountOfKeys++;
            return;
        }

        private Node FindNodeForInsert(Node root, string key)
        {
            var currentNode = root;
            while (!currentNode.Leaf)
            {
                var indexForInsert = FindIndexForInsert(currentNode, key);
                currentNode = currentNode.Children[indexForInsert];
            }
            return currentNode;
        }

        private int FindIndexForInsert(Node node, string key)
        {
            for (int i = 0; i < node.CountOfKeys; i++)
            {
                if (String.Compare(key, node.Keys[i]) <= 0)
                {
                    return i;
                }
            }
            return node.CountOfKeys;
        }
    }
}
