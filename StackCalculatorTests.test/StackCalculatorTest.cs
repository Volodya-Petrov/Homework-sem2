using NUnit.Framework;
using System.Collections.Generic;
using CalculatorBasedOnStack;
using System;
using System.Linq;

namespace StackCalculatorTests.test
{
    class StackCalculatorTest
    {
        [TestCaseSource(nameof(StacksForUncorrectData))]
        public void DivisionByZeroShouldThrowException(IStack stack)
        {
            var postfixForm = "20 0 /";
            Assert.Throws<System.DivideByZeroException>(() => StackCalculator.CalculatePostfixForm(postfixForm, stack));
        }

        [TestCaseSource(nameof(StacksForUncorrectData))]
        public void InvalidSymbolInPostfixFormShouldThrowException(IStack stack)
        {
            var postfixForm = "5 5 XD";
            Assert.Throws<System.ArgumentException>(() => StackCalculator.CalculatePostfixForm(postfixForm, stack));
        }

        [TestCaseSource(nameof(StacksForUncorrectData))]
        public void UncorrectPostfixFormShouldThrowException(IStack stack)
        {
            var postfixForm = "10 10 10 +";
            Assert.Throws<System.ArgumentException>(() => StackCalculator.CalculatePostfixForm(postfixForm, stack));
        }

        private static IEnumerable<TestCaseData> StacksForUncorrectData
           => new TestCaseData[]
           {
                new TestCaseData(new StackOnList()),
                new TestCaseData(new StackOnReferences()),
           };

        [TestCaseSource(nameof(CorrectDataForTest))]
        public void CalculatorShouldGiveCorrectAnswer(IStack stack, string data, double result)
            => Assert.IsTrue(Math.Abs(StackCalculator.CalculatePostfixForm(data, stack) - result) < double.Epsilon);

        private static (string Test, double Result)[] testData = new[]
        {
            ("555 20 /", 27.75),
            ("1234 56 +", 1290),
            ("54 798 -", -744),
            ("56 32 *", 1792),
            ("1 10000 /", 0.0001),
            ("2356 67894 *", 159958264),
            ("23456 999958264 +", 999981720),
            ("0,1 9994527999 -", -9994527998.9),
            ("5 3 - 6 * 9 -", 3),
            ("5 6 + 4 7 - * 5 /", -6.6),
            ("1 2 + 3 * 25 /", 0.36),
            ("5 1 + 8 - 2 *", -4),
            ("1 4 + 1 5 + 4 + /", 0.5),
        };

        private static IEnumerable<TestCaseData> CorrectDataForTest
            =>
                testData.SelectMany(data => new TestCaseData[]
                {
                    new TestCaseData(new StackOnList(), data.Test, data.Result),
                    new TestCaseData(new StackOnReferences(), data.Test, data.Result)
                });
    }
}
