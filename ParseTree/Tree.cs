using System;
using System.IO;

namespace ParseTree
{   
    /// <summary>
    /// дерево разбора арифмитического выражения
    /// </summary>
    public class Tree
    {
        public Tree(string path)
        {
            var str = File.ReadAllText(path);
            var expression = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int index = 0;
            root = CreateTree(expression, ref index);
        }

        private INode root;

        /// <summary>
        ///  считает значение арифмитического выражения, записанного в дерево разбора
        /// </summary>
        public double Calculate()
        {
            return root.Calculate();
        }

        /// <summary>
        /// выдает арифмитическое выражения по его дереву разобра
        /// </summary>
        public string Print()
        {
            return root.Print();
        }

        private bool isOperator(string symbol) => symbol switch
        {
            "+" or "-" or "*" or "/" => true,
            _ => false
        };
        

        private Operator GetOperator(string symbol) => symbol switch
        {
            "+" => new PlusOperator(null, null),
            "-" => new MinusOperator(null, null),
            "*" => new MultiplicationOperator(null, null),
            "/" => new DivisionOperator(null, null),
            _ => throw new ArgumentException()
        };

        private INode CreateTree(string[] componentsOfExpression, ref int index)
        {
            if (double.TryParse(componentsOfExpression[index], out double value))
            {
                index++;
                return new Operand(value);
            }
            while (index < componentsOfExpression.Length && !isOperator(componentsOfExpression[index]))
            {
                if (componentsOfExpression[index] != ")" && componentsOfExpression[index] != "(")
                {
                    throw new ArgumentException();
                }
                index++;
            }
            if (index == componentsOfExpression.Length)
            {
                throw new ArgumentException();
            }
            var newNode = GetOperator(componentsOfExpression[index]);
            index++;
            newNode.LeftChild = CreateTree(componentsOfExpression, ref index);
            newNode.RightChild = CreateTree(componentsOfExpression, ref index);
            return newNode;
        }

    }
}
