using System;
using System.Windows.Forms;

namespace Calculator
{
    class CalculatorManager
    {
        public string CurrentValue { get => currentValue; }

        private string currentValue;
        private int state = 0;
        private int firstNumber;
        private string operatorSign;

        private int CalculateOperation(string operationSign, int firstNumber, int secondNumber)
        {
            switch (operatorSign)
            {
                case "+":
                    return firstNumber + secondNumber;
                case "-":
                    return firstNumber - secondNumber;
                case "*":
                    return firstNumber * secondNumber;
                case "/":
                    return firstNumber / secondNumber;
                default:
                    return 0;
            }
        }

        public void ButtonClick(string nameOfButton)
        {   
            if (nameOfButton == "C")
            {
                state = 0;
                currentValue = "";
                return;
            }
            int helper;
            switch (state)
            {
                case 0:
                    if (int.TryParse(nameOfButton, out helper))
                    {   
                        if (currentValue != "0")
                        {
                            currentValue += nameOfButton;
                            break;
                        }
                        currentValue = nameOfButton;
                    }
                    if (nameOfButton != "=")
                    {
                        firstNumber = int.Parse(currentValue);
                        operatorSign = nameOfButton;
                        state = 1;
                    }
                    break;
                case 1:
                    if (int.TryParse(nameOfButton, out helper))
                    {
                        currentValue = nameOfButton;
                        state = 2;
                        break;
                    }
                    if (nameOfButton == "=")
                    {
                        state = 3;
                    }
                    operatorSign = nameOfButton;
                    break;
                case 2:
                    if (int.TryParse(nameOfButton, out helper))
                    {
                        if (currentValue != "0")
                        {
                            currentValue += nameOfButton;
                        }
                        break;
                    }
                    try
                    {
                        firstNumber = CalculateOperation(operatorSign, firstNumber, int.Parse(currentValue));
                    }
                    catch (DivideByZeroException)
                    {
                        state = 3;
                        currentValue = "Error: divide by zero";
                        break;
                    }
                    currentValue = firstNumber.ToString();
                    if (nameOfButton == "=")
                    {
                        state = 3;
                    }
                    else
                    {
                        state = 1;
                        operatorSign = nameOfButton;
                    }
                    break;
                case 3:
                    if (int.TryParse(nameOfButton, out helper))
                    {
                        currentValue = nameOfButton;
                        state = 0;
                        break;
                    }
                    if (nameOfButton != "=")
                    {
                        operatorSign = nameOfButton;
                        state = 1;
                    }
                    break;
            }
        }
    }
}
