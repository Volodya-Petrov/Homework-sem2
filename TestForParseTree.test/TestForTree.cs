using NUnit.Framework;
using ParseTree;

namespace TestForParseTree.test
{
    public class Tests
    {
        Tree tree;

        [SetUp]
        public void Setup()
        {
            tree = new Tree("../../../Expression.txt");
        }

        [Test]
        public void TestForRightPrint()
        {
            var expected = "( / ( + 5 ( * 3 ( + -1 16 ) ) ) ( * 2 -5 ) )";
            Assert.AreEqual(expected, tree.Print());
        }

        [Test]
        public void TestForRightCalculate()
        {
            var expected = -5;
            Assert.AreEqual(expected, tree.Calculate());
        }
    }
}