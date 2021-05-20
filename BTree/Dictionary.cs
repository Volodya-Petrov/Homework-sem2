using System;
using System.Collections;
using System.Collections.Generic;

namespace BTree
{
    /// <summary>
    /// словарь хранящий ключ-значение на основе B дерева
    /// </summary>
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : IComparable
        where TValue : IComparable
    {   
        private struct SplittedNode
        {   
            public SplittedNode(Node firstPart, Node secondPart, TKey key, TValue value)
            {
                firstPartOfNode = firstPart;
                secondPartOfNode = secondPart;
                keyForInsert = key;
                valueForInsert = value;
            }

            public Node firstPartOfNode;
            public Node secondPartOfNode;
            public TKey keyForInsert;
            public TValue valueForInsert;
        }

        private class Node
        {
            public Node(int degree)
            {
                Keys = new TKey[2 * degree];
                Values = new TValue[2 * degree];
                Children = new Node[2 * degree + 1];
            }

            public Node Parent { get; set; }

            public bool Leaf { get; set; }

            public int Count { get; set; }

            public TKey[] Keys { get; set; }

            public TValue[] Values { get; set; }

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

        public TValue this[TKey key]
        {
            get
            {
                if (TryGetValue(key, out TValue value))
                {
                    return value;
                }
                throw new KeyNotFoundException();
            }
            set
            {
                if (ContainsKey(key))
                {
                    ChangeValue(key, value);
                    return;
                }
                throw new KeyNotFoundException();
            }
        }

        public int Count => count;

        public ICollection<TKey> Keys
        {
            get
            {
                var listKeyValue = DFS();
                var keyList = new List<TKey>();
                for (int i = 0; i < listKeyValue.Count; i++)
                {
                    keyList.Add(listKeyValue[i].Key);
                }
                return keyList;
            }
        }

        public ICollection<TValue> Values
        { 
            get
            {
                var listKeyValue = DFS();
                var valueList = new List<TValue>();
                for (int i = 0; i < listKeyValue.Count; i++)
                {
                    valueList.Add(listKeyValue[i].Value);
                }
                return valueList;
            }
        }

        public bool IsReadOnly => false;

        private int count;
        private int degreeOfTree;
        private Node root;
        private int version;

        /// <summary>
        /// проверяет есть ли ключ в словаре
        /// </summary>
        public bool ContainsKey(TKey key)
        {
            var currentNode = FindNodeForInsert(key);
            return key.CompareTo(currentNode.Keys[FindIndexForInsert(currentNode, key)]) == 0;
        }

        /// <summary>
        /// возвращает значение по ключу
        /// </summary>
        public bool TryGetValue(TKey key, out TValue value)
        {
            var currentNode = FindNodeForInsert(key);
            var indexForInsert = FindIndexForInsert(currentNode, key);
            if (key.CompareTo(currentNode.Keys[indexForInsert]) == 0)
            {
                value = currentNode.Values[indexForInsert];
                return true;
            }
            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Меняет значение по ключу
        /// </summary>
        public void ChangeValue(TKey key, TValue newValue)
        {
            var currentNode = FindNodeForInsert(key);
            int indexForInsert = FindIndexForInsert(currentNode, key);
            if (currentNode.Keys[indexForInsert].CompareTo(key) != 0)
            {
                throw new ArgumentException();
            }
            currentNode.Values[indexForInsert] = newValue;
            version++;
        }

        /// <summary>
        /// добавляет ключ в словарь
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            version++;
            if (!ContainsKey(key))
            {
                count++;
            }
            var currentNode = FindNodeForInsert(key);
            AddKeyToNode(currentNode, key, value, null, null);
            while (currentNode.Count > 2 * degreeOfTree - 1)
            {
                var splittedNode = SplitNode(currentNode);
                currentNode = splittedNode.firstPartOfNode.Parent;
                if (currentNode == null)
                {
                    currentNode = new Node(degreeOfTree);
                    currentNode.Keys[0] = splittedNode.keyForInsert;
                    currentNode.Values[0] = splittedNode.valueForInsert;
                    currentNode.Count++;
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
        public bool Remove(TKey key)
        {
            var node = FindNodeForInsert(key);
            if (FindIndexForInsert(node, key) == node.Count)
            {
                return false;
            }
            RemoveFromInternalNode(node, key);
            count--;
            version++;
            return true;
        }

        private void RemoveFromInternalNode(Node node, TKey key)
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

        private void RemoveFromLeaf(Node node, TKey key)
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
                if (node.Count == 0)
                {
                    root = node.Children[0];
                    root.Parent = null;
                }
                return;
            }
            if (node.Count >= degreeOfTree - 1)
            {
                return;
            }
            var parent = node.Parent;
            var indexOfNode = FindNodeIndexInParent(node);
            if (indexOfNode > 0 && parent.Children[indexOfNode - 1].Count > degreeOfTree - 1)
            {
                LeftRotation(parent, indexOfNode - 1);
                return;
            }
            if (indexOfNode != parent.Count && parent.Children[indexOfNode + 1].Count > degreeOfTree - 1)
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
            for (int i = 0; i < parent.Count; i++)
            {
                if (parent.Children[i] == node)
                {
                    return i;
                }
            }
            return parent.Count;
        }

        private void MergeNodes(Node parent, int indexFirstNode, int indexSecondNode, int indexOfParent)
        {
            var leftNode = parent.Children[indexFirstNode];
            var rightNode = parent.Children[indexSecondNode];
            leftNode.Keys[leftNode.Count] = parent.Keys[indexOfParent];
            leftNode.Values[leftNode.Count] = parent.Values[indexOfParent];
            leftNode.Count++;
            var startIndex = leftNode.Count;
            for (int i = startIndex; i < startIndex + rightNode.Count; i++)
            {
                leftNode.Keys[i] = rightNode.Keys[i - startIndex];
                leftNode.Values[i] = rightNode.Values[i - startIndex];
                leftNode.Children[i] = rightNode.Children[i - startIndex];
                if (!leftNode.Leaf)
                {
                    leftNode.Children[i].Parent = leftNode;
                }
                leftNode.Count++;
            }
            leftNode.Children[startIndex + rightNode.Count] = rightNode.Children[rightNode.Count];
            if (!leftNode.Leaf)
            {
                leftNode.Children[startIndex + rightNode.Count].Parent = leftNode;
            }
            for (int i = indexOfParent; i < parent.Count - 1; i++)
            {
                parent.Keys[i] = parent.Keys[i + 1];
                parent.Values[i] = parent.Values[i + 1];
                parent.Children[i + 1] = parent.Children[i + 2];
            }
            parent.Keys[parent.Count - 1] = default(TKey);
            parent.Values[parent.Count - 1] = default(TValue);
            parent.Children[parent.Count] = null;
            parent.Count--;
        }

        private void LeftRotation(Node parent, int indexOfParent)
        {
            var leftChild = parent.Children[indexOfParent];
            var rightChild = parent.Children[indexOfParent + 1];
            for (int i = rightChild.Count - 1; i >= 0; i--)
            {
                rightChild.Keys[i + 1] = rightChild.Keys[i];
                rightChild.Values[i + 1] = rightChild.Values[i];
                rightChild.Children[i + 2] = rightChild.Children[i + 1];
            }
            rightChild.Children[1] = rightChild.Children[0];
            rightChild.Keys[0] = parent.Keys[indexOfParent];
            rightChild.Values[0] = parent.Values[indexOfParent];
            rightChild.Children[0] = leftChild.Children[leftChild.Count];
            if (!rightChild.Leaf)
            {
                rightChild.Children[0].Parent = rightChild;
            }
            rightChild.Count++;
            parent.Keys[indexOfParent] = leftChild.Keys[leftChild.Count - 1];
            parent.Values[indexOfParent] = leftChild.Values[leftChild.Count - 1];
            leftChild.Keys[leftChild.Count - 1] = default(TKey);
            leftChild.Values[leftChild.Count - 1] = default(TValue);
            leftChild.Children[leftChild.Count] = null;
            leftChild.Count--;
        }

        private void RightRotation(Node parent, int indexOfParent)
        {
            var leftChild = parent.Children[indexOfParent];
            var rightChild = parent.Children[indexOfParent + 1];
            leftChild.Keys[leftChild.Count] = parent.Keys[indexOfParent];
            leftChild.Values[leftChild.Count] = parent.Values[indexOfParent];
            leftChild.Children[leftChild.Count + 1] = rightChild.Children[0];
            if (!leftChild.Leaf)
            {
                leftChild.Children[leftChild.Count + 1].Parent = leftChild;
            }
            leftChild.Count++;
            parent.Keys[indexOfParent] = rightChild.Keys[0];
            parent.Values[indexOfParent] = rightChild.Values[0];
            for (int i = 0; i < rightChild.Count - 1; i++)
            {
                rightChild.Keys[i] = rightChild.Keys[i + 1];
                rightChild.Values[i] = rightChild.Values[i + 1];
                rightChild.Children[i] = rightChild.Children[i + 1];
            }
            rightChild.Children[rightChild.Count - 1] = rightChild.Children[rightChild.Count];
            rightChild.Children[rightChild.Count] = null;
            rightChild.Keys[rightChild.Count - 1] = default(TKey);
            rightChild.Values[rightChild.Count - 1] = default(TValue);
            rightChild.Count--;
        }

        private void RemoveKeyInLeaf(TKey key, Node node)
        {
            var index = FindIndexForInsert(node, key);
            for (int i = index; i < node.Count - 1; i++)
            {
                node.Keys[i] = node.Keys[i + 1];
                node.Values[i] = node.Values[i + 1];
            }
            node.Keys[node.Count - 1] = default(TKey);
            node.Values[node.Count - 1] = default(TValue);
            node.Count--;
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
                newNode.Count++;
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
            var separator = (node.Count - 1) / 2;
            var firstPart = CopyNode(node, 0, separator);
            var secondPart = CopyNode(node, separator + 1, node.Count);
            return new SplittedNode(firstPart, secondPart, node.Keys[separator], node.Values[separator]);
        }

        private void AddKeyToNode(Node node, TKey key, TValue value, Node firstPart, Node secondPart)
        {
            var indexForInsert = FindIndexForInsert(node, key);
            if (node.Keys[indexForInsert].CompareTo(key) == 0)
            {
                node.Values[indexForInsert] = value;
                return;
            }
            for (int i = node.Count - 1; i >= indexForInsert; i--)
            {
                node.Keys[i + 1] = node.Keys[i];
                node.Values[i + 1] = node.Values[i];
                node.Children[i + 2] = node.Children[i + 1];
            }
            node.Keys[indexForInsert] = key;
            node.Values[indexForInsert] = value;
            node.Children[indexForInsert] = firstPart;
            node.Children[indexForInsert + 1] = secondPart;
            node.Count++;
            return;
        }

        private Node FindNodeForInsert(TKey key)
        {
            var currentNode = root;
            while (!currentNode.Leaf)
            {
                var indexForInsert = FindIndexForInsert(currentNode, key);
                if (currentNode.Keys[indexForInsert].CompareTo(key) == 0)
                {
                    return currentNode;
                }
                currentNode = currentNode.Children[indexForInsert];
            }
            return currentNode;
        }

        private int FindIndexForInsert(Node node, TKey key)
        {
            for (int i = 0; i < node.Count; i++)
            {
                if (key.CompareTo(node.Keys[i]) <= 0)
                {
                    return i;
                }
            }
            return node.Count;
        }

        private List<KeyValuePair<TKey, TValue>> DFS()
        {
            if (root == null)
            {
                return null;
            }
            var list = new List<KeyValuePair<TKey, TValue>>();
            RecursiveDFS(list, root);
            return list;
        }

        private void RecursiveDFS(List<KeyValuePair<TKey, TValue>> list, Node currentNode)
        {
            if (currentNode == null)
            {
                return;
            }
            for (int i = 0; i < currentNode.Count; i++)
            {
                var keyValue = new KeyValuePair<TKey, TValue>(currentNode.Keys[i], currentNode.Values[i]);
                list.Add(keyValue);
                RecursiveDFS(list, currentNode.Children[i]);
            }
            RecursiveDFS(list, currentNode.Children[currentNode.Count]);
        }

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            count = 0;
            root = null;
            version++;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (TryGetValue(item.Key, out TValue value))
            {
                return value.CompareTo(item.Value) == 0;
            }
            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {   
            if (array.Length - arrayIndex < count || arrayIndex < 0)
            {
                throw new ArgumentException();
            }
            foreach (var item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (TryGetValue(item.Key, out TValue value))
            {
                if (value.CompareTo(item.Value) == 0)
                {
                    return Remove(item.Key);
                }
            }
            return false;
        }

        private class DictionaryEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private int currentVersion;
            private List<KeyValuePair<TKey, TValue>> list;
            private int currentIndex;
            private Dictionary<TKey, TValue> bTree;

            public DictionaryEnumerator(Dictionary<TKey, TValue> dict)
            {
                currentVersion = dict.version;
                bTree = dict;
                currentIndex = 0;
                list = dict.DFS();
            }

            public KeyValuePair<TKey, TValue> Current => list[currentIndex];

            object IEnumerator.Current => list[currentIndex];

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (currentVersion != bTree.version)
                {
                    throw new InvalidOperationException();
                }
                if (currentIndex < list.Count)
                {
                    currentIndex++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                currentIndex = 0;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => new DictionaryEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
