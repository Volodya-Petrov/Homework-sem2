using NUnit.Framework;
using System.Collections.Generic;
using WorkWithLists;

namespace TestForLists.test
{
    public class Tests
    {
        private static IList Setup(IList list)
        {
            list.Add(1);
            list.Add(2);
            list.Add(3);
            return list;
        }

        [TestCaseSource(nameof(Lists))]
        public void TestForAdd(IList list)
        {
            list.Add(4);
            Assert.AreEqual(list.Length, 4);
            Assert.AreEqual(list[3], 4);
        }

        [TestCaseSource(nameof(Lists))]
        public void TestForRemove(IList list)
        {
            list.Remove(0);
            list.Remove(1);
            Assert.AreEqual(list.Length, 1);
            Assert.AreEqual(list[0], 2);
        }

        [TestCaseSource(nameof(Lists))]
        public void TestForInsert(IList list)
        {
            list.Insert(0, 5);
            list.Insert(4, 11);
            list.Insert(3, 21);
            Assert.AreEqual(list[0], 5);
            Assert.AreEqual(list[3], 21);
            Assert.AreEqual(list[5], 11);
        }

        [TestCaseSource(nameof(Lists))]
        public void TestShouldThrowExceptionWhenRemoveIndexNotExisting(IList list)
        {
            Assert.Throws<WorkWithLists.ElementDoesNotExist>(() => list.Remove(5));
        }

        private static IEnumerable<TestCaseData> Lists
            => new TestCaseData[]
            {
                new TestCaseData(Setup(new UniqueList())),
                new TestCaseData(Setup(new WorkWithLists.List()))
                
            };
    }
}