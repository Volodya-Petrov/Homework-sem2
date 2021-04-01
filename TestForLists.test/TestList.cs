using NUnit.Framework;

namespace TestForLists.test
{
    public class Tests
    {
        private WorkWithLists.List list; 

        [SetUp]
        public void Setup()
        {
            list = new WorkWithLists.List();
            list.Add(1);
            list.Add(2);
            list.Add(3);
        }

        [Test]
        public void TestForAdd()
        {
            list.Add(4);
            Assert.IsTrue(list.Length == 4);
            Assert.IsTrue(list[3] == 4);
        }

        [Test]
        public void TestForRemove()
        {
            list.Remove(0);
            list.Remove(1);
            Assert.IsTrue(list.Length == 1);
            Assert.IsTrue(list[0] == 2);
        }

        [Test]
        public void TestForInsert()
        {
            list.Insert(0, 5);
            list.Insert(4, 11);
            list.Insert(3, 21);
            Assert.IsTrue(list[0] == 5);
            Assert.IsTrue(list[3] == 21);
            Assert.IsTrue(list[5] == 11);
        }

        [Test]
        public void TestShouldThrowExceptionWhenRemoveIndexNotExisting()
        {
            Assert.Throws<WorkWithLists.ElementDoesNotExist>(() => list.Remove(5));
        }
    }
}