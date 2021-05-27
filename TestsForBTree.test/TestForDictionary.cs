using NUnit.Framework;
using BTree;

namespace TestsForBTree.test
{
    public class Tests
    {
        private Dictionary<int, string> dict1;
        private Dictionary<int, int> dict2;

        private void SetupForFirstDictionary()
        {
            dict1 = new Dictionary<int, string>(5);
            for (int i = 0; i < 50; i++)
            {
                dict1.Add(i, i.ToString());
            }
        }

        private void SetupForSecondDictionary()
        {
            dict2 = new Dictionary<int, int>(2);
            for (int i = 0; i < 50; i++)
            {
                dict2.Add(i, i);
            }
        }
        
        [Test]
        public void TestForInsert()
        {
            SetupForFirstDictionary();
            SetupForSecondDictionary();
            for (int i = 0; i < 50; i++)
            {
                var k = dict1[i];
                Assert.AreEqual(dict1[i], i.ToString());
                Assert.AreEqual(dict2[i], i);
            }
        }

        [Test]
        public void TestForEach()
        {
            SetupForSecondDictionary();
            foreach (var pair in dict2)
            {
                Assert.AreEqual(pair.Key, pair.Value);
                Assert.IsTrue(pair.Key < 50 && pair.Key >= 0);
            }
        }

        [Test]
        public void TestForArrayOfKeys()
        {
            SetupForFirstDictionary();
            SetupForSecondDictionary();
            var keys1 = dict1.Keys;
            var keys2 = dict2.Keys;
            Assert.AreEqual(50, keys1.Count);
            Assert.AreEqual(50, keys2.Count);
            for (int i = 0; i < 50; i++)
            {
                Assert.IsTrue(keys1.Contains(i) && keys2.Contains(i));
            }
        }

        [Test]
        public void TestForRemove()
        {
            SetupForFirstDictionary();
            SetupForSecondDictionary();
            for (int i = 0; i < 10; i++)
            {
                dict2.Remove(i);
                dict1.Remove(i);
            }
            for (int i = 10; i < 50; i++)
            {
                Assert.IsTrue(dict1.Contains(new (i, i.ToString())));
                Assert.IsTrue(dict2.Contains(new (i, i)));
            }
            Assert.AreEqual(40, dict1.Count);
            Assert.AreEqual(40, dict2.Count);
        }
    }
}