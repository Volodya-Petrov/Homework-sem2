using NUnit.Framework;

namespace TestForLists.test
{
    class TestForUniqueList
    {
        private WorkWithLists.UniqueList list;

        [SetUp]
        public void Setup()
        {
            list = new WorkWithLists.UniqueList();
            list.Add(1);
            list.Add(2);
            list.Add(3);
        }

        [Test]
        public void TestShouldThrowExceptionWhenAddExistingValue()
        {
            Assert.Throws<WorkWithLists.ValueAlreadyExistException>(() => list.Add(2));
        }

        [Test]
        public void TestShouldThrowExceptionWhenInsertExistingValue()
        {
            Assert.Throws<WorkWithLists.ValueAlreadyExistException>(() => list.Insert(1, 3));
        }

        [Test]
        public void TestShoudThrowExceptionWhenChangeValueToExistingValue()
        {
            Assert.Throws<WorkWithLists.ValueAlreadyExistException>(() => list[1] = 3);
        }
    }
}
