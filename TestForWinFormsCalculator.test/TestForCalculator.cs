using NUnit.Framework;
using Calculator;

namespace TestForWinFormsCalculator.test
{
    public class Tests
    {
        CalculatorManager calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new CalculatorManager();
        }

        [Test]
        public void TestForStandartOperations()
        {
            calculator.ButtonClick("1");
            calculator.ButtonClick("+");
            calculator.ButtonClick("1");
            calculator.ButtonClick("=");
            Assert.AreEqual("2", calculator.CurrentValue);
            calculator.ButtonClick("4");
            calculator.ButtonClick("-");
            calculator.ButtonClick("2");
            calculator.ButtonClick("=");
            Assert.AreEqual("2", calculator.CurrentValue);
            calculator.ButtonClick("4");
            calculator.ButtonClick("*");
            calculator.ButtonClick("2");
            calculator.ButtonClick("=");
            Assert.AreEqual("8", calculator.CurrentValue);
        }

        [Test]
        public void TestForCheckValueForOperationAfterOperation()
        {
            calculator.ButtonClick("2");
            calculator.ButtonClick("+");
            calculator.ButtonClick("1");
            calculator.ButtonClick("+");
            Assert.AreEqual("3", calculator.CurrentValue);
            calculator.ButtonClick("4");
            calculator.ButtonClick("=");
            Assert.AreEqual("7", calculator.CurrentValue);
        }

        [Test]
        public void TestForOperationsWithNumbersNotOnlyDigits()
        {
            calculator.ButtonClick("2");
            calculator.ButtonClick("3");
            calculator.ButtonClick("+");
            calculator.ButtonClick("1");
            calculator.ButtonClick("1");
            calculator.ButtonClick("=");
            Assert.AreEqual("34", calculator.CurrentValue);
        }

        [Test]
        public void TestForOperatorAfterOperator()
        {
            calculator.ButtonClick("2");
            calculator.ButtonClick("+");
            calculator.ButtonClick("-");
            calculator.ButtonClick("1");
            calculator.ButtonClick("=");
            Assert.AreEqual("1", calculator.CurrentValue);
        }

        [Test]
        public void TestForDivideByZero()
        {
            calculator.ButtonClick("2");
            calculator.ButtonClick("/");
            calculator.ButtonClick("0");
            calculator.ButtonClick("=");
            Assert.AreEqual("Error: divide by zero", calculator.CurrentValue);
        }

        [Test]
        public void TestMax1Unsignificant0()
        {
            calculator.ButtonClick("0");
            calculator.ButtonClick("0");
            Assert.AreEqual("0", calculator.CurrentValue);
            calculator.ButtonClick("1");
            Assert.AreEqual("1", calculator.CurrentValue);
        }
    }
}