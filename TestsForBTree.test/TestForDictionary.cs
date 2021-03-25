using NUnit.Framework;
using BTree;

namespace TestsForBTree.test
{
    public class Tests
    {
        private Dictionary dict;

        [SetUp]
        public void Setup()
        {
            dict = new Dictionary(2);
            dict.Insert("2", "2");
            dict.Insert("1", "1");
            dict.Insert("3", "3");
            dict.Insert("4", "4");
            dict.Insert("5", "5");
            dict.Insert("6", "6");
            dict.Insert("7", "7");
            dict.Insert("8", "8");
            dict.Insert("9", "9");
        }

        [Test]
        public void DictionaryShouldContainsInsertedKeys()
        {
            for (int i = 1; i < 10; i++)
            {
                Assert.IsTrue(dict.Contains(i.ToString()));
            }
        }

        [Test]
        public void GetValueTest()
        {
            for (int i = 1; i < 10; i++)
            {
                Assert.IsTrue(dict.GetValue(i.ToString()) == i.ToString()); 
            }
        }

        [Test]
        public void ChangeValueShouldChangeValue()
        {
            dict.ChangeValue("1", "2");
            Assert.IsTrue(dict.GetValue("1") == "2");
        }

        [Test]
        public void DeleteTestCaseWithMergingAndWithRoot()
        {
            var array = new string[] { "1", "3", "4", "5", "6", "7", "8", "9" };
            dict.Remove("2");
            foreach ( var key in array)
            {
                Assert.IsTrue(dict.Contains(key));
            }
            Assert.IsFalse(dict.Contains("2"));
        }

        [Test]
        public void DeleteTestCaseWithLeftRotation()
        {
            var array = new string[] { "1", "3", "4", "6", "7", "8", "9" };
            dict.Remove("2");
            dict.Remove("5");
            foreach (var key in array)
            {
                Assert.IsTrue(dict.Contains(key));
            }
            Assert.IsFalse(dict.Contains("5"));
        }

        [Test]
        public void DeleteTestCaseWithDeleteFromLeaf()
        {
            var array = new string[] { "1", "2", "3", "4", "5", "6", "7", "8" };
            dict.Remove("9");
            foreach (var key in array)
            {
                Assert.IsTrue(dict.Contains(key));
            }
            Assert.IsFalse(dict.Contains("9"));
        }

        [Test]
        public void DeleteTestCaseWithInternal()
        {
            var newDict = new Dictionary(2);
            var array = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
            foreach (var key in array)
            {   
                newDict.Insert(key, key);
            }
            newDict.Remove("b");
            foreach (var key in array)
            {
                if (key != "b")
                {
                    Assert.IsTrue(newDict.Contains(key));
                }
            }
            Assert.IsFalse(newDict.Contains("b"));
        }
    }
}