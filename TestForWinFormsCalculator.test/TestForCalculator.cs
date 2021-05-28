using NUnit.Framework;
using Calculator;

namespace TestForWinFormsCalculator.test
{
    public class Tests
    {
        private CalculatorManager calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new CalculatorManager();
        }

        [Test]
        public void TestForStandartOperations()
        {
            calculator.DigitClick("1");
            calculator.OperatorClick("+");
            calculator.DigitClick("1");
            calculator.Equal();
            Assert.AreEqual("2", calculator.CurrentValue);
            calculator.DigitClick("4");
            calculator.OperatorClick("-");
            calculator.DigitClick("2");
            calculator.Equal();
            Assert.AreEqual("2", calculator.CurrentValue);
            calculator.DigitClick("4");
            calculator.OperatorClick("*");
            calculator.DigitClick("2");
            calculator.Equal();
            Assert.AreEqual("8", calculator.CurrentValue);
        }

        [Test]
        public void TestForCheckValueForOperationAfterOperation()
        {
            calculator.DigitClick("2");
            calculator.OperatorClick("+");
            calculator.DigitClick("1");
            calculator.OperatorClick("+");
            Assert.AreEqual("3", calculator.CurrentValue);
            calculator.DigitClick("4");
            calculator.Equal();
            Assert.AreEqual("7", calculator.CurrentValue);
        }

        [Test]
        public void TestForOperationsWithNumbersNotOnlyDigits()
        {
            calculator.DigitClick("2");
            calculator.DigitClick("3");
            calculator.OperatorClick("+");
            calculator.DigitClick("1");
            calculator.DigitClick("1");
            calculator.Equal();
            Assert.AreEqual("34", calculator.CurrentValue);
        }

        [Test]
        public void TestForOperatorAfterOperator()
        {
            calculator.DigitClick("2");
            calculator.OperatorClick("+");
            calculator.OperatorClick("-");
            calculator.DigitClick("1");
            calculator.Equal();
            Assert.AreEqual("1", calculator.CurrentValue);
        }

        [Test]
        public void TestForDivideByZero()
        {
            calculator.DigitClick("2");
            calculator.OperatorClick("/");
            calculator.DigitClick("0");
            calculator.Equal();
            Assert.AreEqual("Error: divide by zero", calculator.CurrentValue);
        }

        [Test]
        public void TestMax1Unsignificant0()
        {
            calculator.DigitClick("0");
            calculator.DigitClick("0");
            Assert.AreEqual("0", calculator.CurrentValue);
            calculator.DigitClick("1");
            Assert.AreEqual("1", calculator.CurrentValue);
        }
    }
}