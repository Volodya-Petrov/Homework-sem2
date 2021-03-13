using NUnit.Framework;
using System.Collections.Generic;
using CalculatorBasedOnStack;

namespace StackCalculatorTests.test
{
    public class StackTest
    {
        [TestCaseSource(nameof(Stacks))]
        public void StackAfterPushShouldBeNotEmpty(IStack stack)
        {
            stack.Push(5.0);
            Assert.IsFalse(stack.IsEmpty());
        }

        [TestCaseSource(nameof(Stacks))]
        public void PopAfterPushShouldReturnSameValue(IStack stack)
        {
            stack.Push(5);
            Assert.AreEqual(5, stack.Pop());
        }

        [TestCaseSource(nameof(Stacks))]
        public void EmptyStackIsEmpty(IStack stack)
        {
            stack.Push(5);
            stack.Pop();
            Assert.IsTrue(stack.IsEmpty());
        }

        [TestCaseSource(nameof(Stacks))]
        public void PopShouldThrowExceptionIfStackIsEmpty(IStack stack)
           => Assert.Throws<System.InvalidOperationException>(() => stack.Pop());

        private static IEnumerable<TestCaseData> Stacks
            => new TestCaseData[]
            {
                new TestCaseData(new StackOnList()),
                new TestCaseData(new StackOnReferences()),
            };
    }
}