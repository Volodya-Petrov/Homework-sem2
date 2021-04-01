using NUnit.Framework;
using System.Collections.Generic;
using ToolsForList;

namespace TestForToolsForList.test
{
    public class Tests
    {
        private List<int> list;

        [SetUp]
        public void Setup()
        {
            list = new List<int>() { 1, 2, 3, 4};
        }

        [Test]
        public void TestForMap()
        {
            var expectedList = new List<int>() { 1, 4, 9, 16 };
            var resultList = FunctionsForList.Map(list, x => x * x);
            Assert.AreEqual(expectedList.Count, resultList.Count);
            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], resultList[i]);
            }
        }

        [Test]
        public void TestForFilter()
        {
            var expectedList = new List<int>() { 2, 4 };
            var resultList = FunctionsForList.Filter(list, x => (x % 2) == 0);
            Assert.AreEqual(expectedList.Count, resultList.Count);
            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], resultList[i]);
            }
        }

        [Test]
        public void TestForFold()
        {
            var result = FunctionsForList.Fold(list, 2, (x, y) => x * y);
            Assert.AreEqual(48, result);
        }
    }
}