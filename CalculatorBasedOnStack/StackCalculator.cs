using System;

namespace CalculatorBasedOnStack
{
    /// <summary>
    /// калькулятор постфиксной формы
    /// </summary>
    public static class StackCalculator
    {   
        /// <summary>
        /// считает постфиксную форму записи
        /// </summary>
        public static double CalculatePostfixForm(string postFixForm, IStack stack)
        {
            var elemntsFromPostFixForm = postFixForm.Split(' ');
            for (int i = 0; i < elemntsFromPostFixForm.Length; i++)
            {
                var isNumber = double.TryParse(elemntsFromPostFixForm[i], out double number);
                if (isNumber)
                {
                    stack.Push(number);
                }
                else
                {
                    var number1 = stack.Pop();
                    var number2 = stack.Pop();
                    switch (elemntsFromPostFixForm[i])
                    {
                        case "+":
                            stack.Push(number1 + number2);
                            break;
                        case "-":
                            stack.Push(number2 - number1);
                            break;
                        case "*":
                            stack.Push(number2 * number1);
                            break;
                        case "/":
                            if (Math.Abs(number1) < double.Epsilon)
                            {
                                throw new DivideByZeroException();
                            }
                            stack.Push(number2 / number1);
                            break;
                        default:
                            throw new ArgumentException("Некорректная постфиксная форма");
                    }
                }
            }
            var result = stack.Pop();
            if (!stack.IsEmpty())
            {
                throw new ArgumentException("Некорректный ввод данных");
            }
            return result;
        }
    }
}
