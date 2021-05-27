using System;

namespace Calculator
{   
    /// <summary>
    /// класс представляющий калькулятор
    /// </summary>
    public class CalculatorManager
    {
        public string CurrentValue { get => currentValue; }

        private string currentValue = "";
        private State state = 0;
        private int firstNumber;
        private string operatorSign;

        enum State
        {
            FirstNumberOrFirstSign,
            WaitForBeginOfSecondNumber,
            Calculation,
            WaitingForChooseState
        }

        private void CalculateAndCatchDivideByZero()
        {
            try
            {
                firstNumber = CalculateOperation(operatorSign, firstNumber, int.Parse(currentValue));
            }
            catch (DivideByZeroException)
            {
                state = State.WaitingForChooseState;
                currentValue = "Error: divide by zero";
            }
        }    

        private int CalculateOperation(string operationSign, int firstNumber, int secondNumber) => operatorSign switch
        {   
            "+" => firstNumber + secondNumber,
            "-" => firstNumber - secondNumber,
            "*" => firstNumber * secondNumber,
            "/" => firstNumber / secondNumber,
            _ => 0
        };

        /// <summary>
        /// сбрасывает состояние калькулятора в самое начальное
        /// </summary>
        public void Clear()
        {
            state = 0;
            currentValue = "";
        }

        /// <summary>
        /// обрабатывает нажатие на оператор
        /// </summary>
        public void OperatorClick(string nameOfOperator)
        {
            switch(state)
            {
                case State.FirstNumberOrFirstSign:
                    if (currentValue != "")
                    {
                        firstNumber = int.Parse(CurrentValue);
                        operatorSign = nameOfOperator;
                        state = State.WaitForBeginOfSecondNumber;
                    }
                    break;
                case State.WaitForBeginOfSecondNumber:
                    operatorSign = nameOfOperator;
                    break;
                case State.Calculation:
                    CalculateAndCatchDivideByZero();
                    if (currentValue == "Error: divide by zero")
                    {
                        break;
                    }
                    currentValue = firstNumber.ToString();
                    operatorSign = nameOfOperator;
                    state = State.WaitForBeginOfSecondNumber;
                    break;
                case State.WaitingForChooseState:
                    state = State.WaitForBeginOfSecondNumber;
                    operatorSign = nameOfOperator;
                    break;
            }
        }

        /// <summary>
        /// обрабатывает нажатие на число
        /// </summary>
        public void DigitClick(string digit)
        {
            switch (state)
            {
                case State.FirstNumberOrFirstSign:
                    if (currentValue != "0")
                    {
                        currentValue += digit;
                        break;
                    }
                    currentValue = digit;
                    break;
                case State.WaitForBeginOfSecondNumber:
                    currentValue = digit;
                    state = State.Calculation;
                    break;
                case State.Calculation:
                    if (currentValue != "0")
                    {
                        currentValue += digit;
                    }
                    break;
                case State.WaitingForChooseState:
                    currentValue = digit;
                    state = State.FirstNumberOrFirstSign;
                    break;
            }
        }

        /// <summary>
        /// выводит результат операции
        /// </summary>
        public void Equal()
        {
            switch (state)
            {
                case State.WaitForBeginOfSecondNumber:
                    state = State.WaitingForChooseState;
                    break;
                case State.Calculation:
                    CalculateAndCatchDivideByZero();
                    if (currentValue == "Error: divide by zero")
                    {
                        break;
                    }
                    currentValue = firstNumber.ToString();
                    state = State.WaitingForChooseState;
                    break;
            }
        }
    }
}
