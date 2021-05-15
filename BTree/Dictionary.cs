using System;

namespace BTree
{
    /// <summary>
    /// словарь хранящий ключ-значение на основе B дерева
    /// </summary>
    public class Dictionary
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

        /// <summary>
        /// проверяет есть ли ключ в словаре
        /// </summary>
        public bool Contains(string key)
        {
            var currentNode = FindNodeForInsert(key);
            return key == currentNode.Keys[FindIndexForInsert(currentNode, key)];
        }

        /// <summary>
        /// возвращает значение по ключу
        /// </summary>
        public string GetValue(string key)
        {
            var currentNode = FindNodeForInsert(key);
            var indexForInsert = FindIndexForInsert(currentNode, key);
            return key == currentNode.Keys[indexForInsert] ? currentNode.Values[indexForInsert] : null;
        }

        /// <summary>
        /// Меняет значение по ключу
        /// </summary>
        public void ChangeValue(string key, string newValue)
        {
            var currentNode = FindNodeForInsert(key);
            int indexForInsert = FindIndexForInsert(currentNode, key);
            if (currentNode.Keys[indexForInsert] != key)
            {
                throw new ArgumentException();
            }
            currentNode.Values[indexForInsert] = newValue;
        }

        /// <summary>
        /// добавляет ключ в словарь
        /// </summary>
        public void Insert(string key, string value)
        {
            var currentNode = FindNodeForInsert(key);
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

        /// <summary>
        /// удаляет ключ из словаря
        /// </summary>
        public void Remove(string key)
        {
            var node = FindNodeForInsert(key);
            if (FindIndexForInsert(node, key) == node.CountOfKeys)
            {
                return;
            }
            RemoveFromInternalNode(node, key);
        }

        private void RemoveFromInternalNode(Node node, string key)
        {
            if (node.Leaf)
            {
                RemoveFromLeaf(node, key);
                return;
            }
            var indexOfKey = FindIndexForInsert(node, key);
            node.Keys[indexOfKey] = node.Children[indexOfKey + 1].Keys[0];
            node.Values[indexOfKey] = node.Children[indexOfKey + 1].Values[0];
            RemoveFromInternalNode(node.Children[indexOfKey + 1], node.Children[indexOfKey + 1].Keys[0]);
        }

        private void RemoveFromLeaf(Node node, string key)
        {
            RemoveKeyInLeaf(key, node);
            if (node == root)
            {
                return;
            }
            Rebalancing(node);
        }

        private void Rebalancing(Node node)
        {
            if (node == root)
            {
                if (node.CountOfKeys == 0)
                {
                    root = node.Children[0];
                    root.Parent = null;
                }
                return;
            }
            if (node.CountOfKeys >= degreeOfTree - 1)
            {
                return;
            }
            var parent = node.Parent;
            var indexOfNode = FindNodeIndexInParent(node);
            if (indexOfNode > 0 && parent.Children[indexOfNode - 1].CountOfKeys > degreeOfTree - 1)
            {
                LeftRotation(parent, indexOfNode - 1);
                return;
            }
            if (indexOfNode != parent.CountOfKeys && parent.Children[indexOfNode + 1].CountOfKeys > degreeOfTree - 1)
            {
                RightRotation(parent, indexOfNode);
                return;
            }
            if (indexOfNode > 0)
            {
                MergeNodes(parent, indexOfNode - 1, indexOfNode, indexOfNode - 1);
                Rebalancing(parent);
                return;
            }
            MergeNodes(parent, indexOfNode, indexOfNode + 1, indexOfNode);
            Rebalancing(parent);
        }

        private int FindNodeIndexInParent(Node node)
        {
            var parent = node.Parent;
            for (int i = 0; i < parent.CountOfKeys; i++)
            {
                if (parent.Children[i] == node)
                {
                    return i;
                }
            }
            return parent.CountOfKeys;
        }

        private void MergeNodes(Node parent, int indexFirstNode, int indexSecondNode, int indexOfParent)
        {
            var leftNode = parent.Children[indexFirstNode];
            var rightNode = parent.Children[indexSecondNode];
            leftNode.Keys[leftNode.CountOfKeys] = parent.Keys[indexOfParent];
            leftNode.Values[leftNode.CountOfKeys] = parent.Values[indexOfParent];
            leftNode.CountOfKeys++;
            var startIndex = leftNode.CountOfKeys;
            for (int i = startIndex; i < startIndex + rightNode.CountOfKeys; i++)
            {
                leftNode.Keys[i] = rightNode.Keys[i - startIndex];
                leftNode.Values[i] = rightNode.Values[i - startIndex];
                leftNode.Children[i] = rightNode.Children[i - startIndex];
                if (!leftNode.Leaf)
                {
                    leftNode.Children[i].Parent = leftNode;
                }
                leftNode.CountOfKeys++;
            }
            leftNode.Children[startIndex + rightNode.CountOfKeys] = rightNode.Children[rightNode.CountOfKeys];
            if (!leftNode.Leaf)
            {
                leftNode.Children[startIndex + rightNode.CountOfKeys].Parent = leftNode;
            }
            for (int i = indexOfParent; i < parent.CountOfKeys - 1; i++)
            {
                parent.Keys[i] = parent.Keys[i + 1];
                parent.Values[i] = parent.Values[i + 1];
                parent.Children[i + 1] = parent.Children[i + 2];
            }
            parent.Keys[parent.CountOfKeys - 1] = null;
            parent.Values[parent.CountOfKeys - 1] = null;
            parent.Children[parent.CountOfKeys] = null;
            parent.CountOfKeys--;
        }

        private void LeftRotation(Node parent, int indexOfParent)
        {
            var leftChild = parent.Children[indexOfParent];
            var rightChild = parent.Children[indexOfParent + 1];
            for (int i = rightChild.CountOfKeys - 1; i >= 0; i--)
            {
                rightChild.Keys[i + 1] = rightChild.Keys[i];
                rightChild.Values[i + 1] = rightChild.Values[i];
                rightChild.Children[i + 2] = rightChild.Children[i + 1];
            }
            rightChild.Children[1] = rightChild.Children[0];
            rightChild.Keys[0] = parent.Keys[indexOfParent];
            rightChild.Values[0] = parent.Values[indexOfParent];
            rightChild.Children[0] = leftChild.Children[leftChild.CountOfKeys];
            if (!rightChild.Leaf)
            {
                rightChild.Children[0].Parent = rightChild;
            }
            rightChild.CountOfKeys++;
            parent.Keys[indexOfParent] = leftChild.Keys[leftChild.CountOfKeys - 1];
            parent.Values[indexOfParent] = leftChild.Values[leftChild.CountOfKeys - 1];
            leftChild.Keys[leftChild.CountOfKeys - 1] = null;
            leftChild.Values[leftChild.CountOfKeys - 1] = null;
            leftChild.Children[leftChild.CountOfKeys] = null;
            leftChild.CountOfKeys--;
        }

        private void RightRotation(Node parent, int indexOfParent)
        {
            var leftChild = parent.Children[indexOfParent];
            var rightChild = parent.Children[indexOfParent + 1];
            leftChild.Keys[leftChild.CountOfKeys] = parent.Keys[indexOfParent];
            leftChild.Values[leftChild.CountOfKeys] = parent.Values[indexOfParent];
            leftChild.Children[leftChild.CountOfKeys + 1] = rightChild.Children[0];
            if (!leftChild.Leaf)
            {
                leftChild.Children[leftChild.CountOfKeys + 1].Parent = leftChild;
            }
            leftChild.CountOfKeys++;
            parent.Keys[indexOfParent] = rightChild.Keys[0];
            parent.Values[indexOfParent] = rightChild.Values[0];
            for (int i = 0; i < rightChild.CountOfKeys - 1; i++)
            {
                rightChild.Keys[i] = rightChild.Keys[i + 1];
                rightChild.Values[i] = rightChild.Values[i + 1];
                rightChild.Children[i] = rightChild.Children[i + 1];
            }
            rightChild.Children[rightChild.CountOfKeys - 1] = rightChild.Children[rightChild.CountOfKeys];
            rightChild.Children[rightChild.CountOfKeys] = null;
            rightChild.Keys[rightChild.CountOfKeys - 1] = null;
            rightChild.Values[rightChild.CountOfKeys - 1] = null;
            rightChild.CountOfKeys--;
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
                if (!newNode.Leaf)
                {
                    newNode.Children[i].Parent = newNode;
                }
                newNode.CountOfKeys++;
            }
            newNode.Children[endIndex - beginIndex] = sourceNode.Children[endIndex];
            if (!newNode.Leaf)
            {
                newNode.Children[endIndex - beginIndex].Parent = newNode;
            }
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

        private Node FindNodeForInsert(string key)
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
