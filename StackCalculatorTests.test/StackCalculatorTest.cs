using NUnit.Framework;
using System.Collections.Generic;
using CalculatorBasedOnStack;

namespace StackCalculatorTests.test
{
    class StackCalculatorTest
    {
        [TestCaseSource(nameof(Stacks))]
        public void CalculatorShouldGiveCorrectAnswer(IStack stack)
        {
            var postfixForm = "9 6 - 1 2 + *";
            var answer = StackCalculator.CalculatePostfixForm(postfixForm, stack);
            Assert.AreEqual(9.0, answer);
        }

        [TestCaseSource(nameof(Stacks))]
        public void DivisionByZeroShouldThrowException(IStack stack)
        {
            var postfixForm = "20 0 /";
            Assert.Throws<System.DivideByZeroException>(() => StackCalculator.CalculatePostfixForm(postfixForm, stack));
        }

        [TestCaseSource(nameof(Stacks))]
        public void InvalidSymbolInPostfixFormShouldThrowException(IStack stack)
        {
            var postfixForm = "5 5 XD";
            Assert.Throws<System.ArgumentException>(() => StackCalculator.CalculatePostfixForm(postfixForm, stack));
        }

        [TestCaseSource(nameof(Stacks))]
        public void UncorrectPostfixFormShouldThrowException(IStack stack)
        {
            var postfixForm = "10 10 10 +";
            Assert.Throws<System.ArgumentException>(() => StackCalculator.CalculatePostfixForm(postfixForm, stack));
        }

        private static IEnumerable<TestCaseData> Stacks
           => new TestCaseData[]
           {
                new TestCaseData(new StackOnList()),
                new TestCaseData(new StackOnReferences()),
           };
    }
}
