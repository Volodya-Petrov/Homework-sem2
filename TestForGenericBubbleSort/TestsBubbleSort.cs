using BubbleOnGeneric;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestForGenericBubbleSort
{
    public class Tests
    {
        private List<string[]> stringsArrayList;
        private List<int> intList;

        [SetUp]
        public void Setup()
        {
            stringsArrayList = new List<string[]>() { new[] { "g", "a", "c" }, new[] { "a" }, new[] { "ab", "f" } };
            intList = new List<int>() { 3, 4, 1, 2, 5 };
        }

        [Test]
        public void TestBubbleSortOnArrayOfStrings()
        {
            Sort.BubbleSort(stringsArrayList, new ComparerOfArrayOfString());
            var listOfStrings = new List<string[]>() { new[] { "a" }, new[] { "ab", "f" }, new[] { "g", "a", "c" } };
            for (int i = 0; i < listOfStrings.Count; i++)
            {
                for (int j = 0; j < listOfStrings[i].Length; j++)
                {
                    Assert.AreEqual(listOfStrings[i][j], stringsArrayList[i][j]);
                }
            }
        }

        [Test]
        public void TestBubbleSortOnIntList()
        {
            Sort.BubbleSort(intList, new ComparerForInt());
            for (int i = 0; i < intList.Count; i++)
            {
                Assert.AreEqual(5 - i, intList[i]); 
            }
        }
    }
}