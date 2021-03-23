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
            if (degree < 2)
            {
                throw new ArgumentException("Степень словаря должна быть больше 1");
            }
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

        public bool Contains(string key)
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

        public void Remove(string key)
        {

        }

        private void RemoveLeafCase(string key, Node node)
        {
            RemoveKeyInLeaf(key, node);
            if (node.CountOfKeys == degreeOfTree)
            {
                var parentNode = node.Parent;
                var indexOfParent = FindIndexForInsert(parentNode, key);
                var oldParentKey = parentNode.Keys[indexOfParent];
                var oldParentValue = parentNode.Values[indexOfParent];
                AddKeyToNode(node, oldParentKey, oldParentValue, null, null);
                string newParentKey;
                string newParentValue;
                if (parentNode.Children[indexOfParent + 1].CountOfKeys > degreeOfTree - 1)
                {
                    newParentKey = parentNode.Children[indexOfParent + 1].Keys[0];
                    newParentValue = parentNode.Children[indexOfParent + 1].Values[0];
                    RemoveKeyInLeaf(newParentKey, parentNode.Children[indexOfParent + 1]);
                    parentNode.Keys[indexOfParent] = newParentKey;
                    parentNode.Values[indexOfParent] = newParentValue;
                    return;
                }
                var mergedNode = MergeLeaves(node, parentNode.Children[indexOfParent + 1]);
                newParentKey = mergedNode.Keys[0];
                newParentValue = mergedNode.Values[0];
                RemoveKeyInLeaf(newParentKey, mergedNode);
                parentNode.Keys[indexOfParent] = newParentKey;
                parentNode.Values[indexOfParent] = newParentValue;
                parentNode.Children[indexOfParent] = null;
                parentNode.Children[indexOfParent + 1] = mergedNode;
            }
        }
        
        private Node MergeLeaves(Node firstLeaf, Node secondLeaf)
        {
            var newNode = new Node(degreeOfTree);
            newNode.Leaf = true;
            newNode.Parent = firstLeaf.Parent;
            for (int i  = 0; i < firstLeaf.CountOfKeys; i++)
            {
                newNode.Keys[i] = firstLeaf.Keys[i];
                newNode.Values[i] = firstLeaf.Values[i];
            }
            for (int i = firstLeaf.CountOfKeys; i < firstLeaf.CountOfKeys + secondLeaf.CountOfKeys; i++)
            {
                newNode.Keys[i] = secondLeaf.Keys[i - firstLeaf.CountOfKeys];
                newNode.Values[i] = secondLeaf.Values[i - firstLeaf.CountOfKeys];
            }
            return newNode;
        }

        private void RemoveKeyInLeaf(string key, Node node)
        {
            var index = FindIndexForInsert(node, key);
            for (int i = index; i < node.CountOfKeys - 1; i++)
            {
                node.Keys[i] = node.Keys[i + 1];
                node.Values[i] = node.Values[i + 1];
            }
            node.Keys[node.CountOfKeys - 1] = null;
            node.Values[node.CountOfKeys - 1] = null;
            node.CountOfKeys--;
            return;
        }

        private Node CopyNode(Node sourceNode, int beginIndex, int endIndex)
        {
            var newNode = new Node(degreeOfTree);
            newNode.Leaf = sourceNode.Leaf;
            newNode.Parent = sourceNode.Parent;
            for (int i = 0; i + beginIndex < endIndex; i++)
            {
                newNode.Keys[i] = sourceNode.Keys[beginIndex + i];
                newNode.Values[i] = sourceNode.Values[beginIndex + i];
                newNode.Children[i] = sourceNode.Children[beginIndex + i];
                newNode.CountOfKeys++;
            }
            newNode.Children[endIndex - beginIndex] = sourceNode.Children[endIndex];
            return newNode;
        }
        private SplittedNode SplitNode(Node node)
        {
            var separator = (node.CountOfKeys - 1) / 2;
            var firstPart = CopyNode(node, 0, separator);
            var secondPart = CopyNode(node, separator + 1, node.CountOfKeys);
            return new SplittedNode(firstPart, secondPart, node.Keys[separator], node.Values[separator]);
        }

        private void AddKeyToNode(Node node, string key, string value, Node firstPart, Node secondPart)
        {
            var indexForInsert = FindIndexForInsert(node, key);
            if (node.Keys[indexForInsert] == key)
            {
                node.Values[indexForInsert] = value;
                return;
            }
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
                if (currentNode.Keys[indexForInsert] == key)
                {
                    return currentNode;
                }
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
