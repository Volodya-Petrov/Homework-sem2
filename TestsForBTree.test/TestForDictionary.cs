/*using NUnit.Framework;
using BTree;

namespace TestsForBTree.test
{
    public class Tests
    {
        private Dictionary Setup()
        {
            var dict = new Dictionary(2);
            dict.Insert("2", "2");
            dict.Insert("1", "1");
            dict.Insert("3", "3");
            dict.Insert("4", "4");
            dict.Insert("5", "5");
            dict.Insert("6", "6");
            dict.Insert("7", "7");
            dict.Insert("8", "8");
            dict.Insert("9", "9");
            return dict;
        }

        private Dictionary SetupForBigDictionary()
        {
            var dictionary = new Dictionary(5);
            var array = new string[]
            { 
                "01", "02", "03", "04",
                "05", "06", "07", "08", 
                "09", "10", "11", "12",
                "13", "14", "15", "16",
                "17", "18", "19", "20"
            };
            foreach (var str in array)
            {
                dictionary.Insert(str, str);
            }
            return dictionary;
        }

        [Test]
        public void DictionaryShouldContainInsertedKeys()
        {
            var dict = Setup();
            for (int i = 1; i < 10; i++)
            {
                Assert.IsTrue(dict.Contains(i.ToString()));
            }
        }

        [Test]
        public void GetValueTest()
        {
            var dict = Setup();
            for (int i = 1; i < 10; i++)
            {
                Assert.IsTrue(dict.GetValue(i.ToString()) == i.ToString()); 
            }
        }

        [Test]
        public void ChangeValueShouldChangeValue()
        {
            var dict = Setup();
            dict.ChangeValue("1", "2");
            Assert.IsTrue(dict.GetValue("1") == "2");
        }

        [Test]
        public void DeleteTestCaseWithMergingAndWithRoot()
        {
            var dict = Setup();
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
            var dict = Setup();
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
            var dict = Setup();
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

        [Test]
        public void InsertShouldBeCorrectOnBigData()
        {
            var dict = SetupForBigDictionary();
            var array = new string[]
            {
                "01", "02", "03", "04",
                "05", "06", "07", "08",
                "09", "10", "11", "12",
                "13", "14", "15", "16",
                "17", "18", "19", "20"
            };
            foreach (var str in array)
            {
                Assert.IsTrue(dict.Contains(str));
            }
        }

        [Test]
        public void DeleteShouldBeCorrectOnBigData()
        {
            var dict = SetupForBigDictionary();
            dict.Remove("09");
            dict.Remove("03");
            dict.Remove("01");
            var existArray = new string[]
            {
                "02", "04",
                "05", "06", "07", "08",
                "10", "11", "12",
                "13", "14", "15", "16",
                "17", "18", "19", "20"
            };
            var removedArray = new string[] { "09", "03", "01" };
            foreach (var str in existArray)
            {
                Assert.IsTrue(dict.Contains(str));
            }
            foreach (var str in removedArray)
            {
                Assert.IsFalse(dict.Contains(str));
            }
        }

        [Test]
        public void GetValueShouldBeCorrectOnBigData()
        {
            var dict = SetupForBigDictionary();
            var array = new string[]
            {
                "01", "02", "03", "04",
                "05", "06", "07", "08",
                "09", "10", "11", "12",
                "13", "14", "15", "16",
                "17", "18", "19", "20"
            };
            foreach (var key in array)
            {
                Assert.AreEqual(key, dict.GetValue(key));
            }
        }

        [Test]
        public void ChangeValueShouldBeCorrectOnBigData()
        {
            var dict = SetupForBigDictionary();
            dict.ChangeValue("02", "draaain gaaaang");
            Assert.AreEqual("draaain gaaaang", dict.GetValue("02"));
        }
    }
}*/