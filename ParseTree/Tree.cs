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
        INode root;

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

        private bool isOperator(string symbol)
        {
            switch(symbol)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                    return true;
                default:
                    return false;
            }
        }

        private INode CreateTree(string[] componentsOfExpression, ref int index)
        {
            double value;
            if (double.TryParse(componentsOfExpression[index], out value))
            {
                index++;
                return new Operand(value);
            }
            while (index < componentsOfExpression.Length && !isOperator(componentsOfExpression[index]))
            {
                index++;
            }
            if (index == componentsOfExpression.Length)
            {
                return null;
            }
            var newNode = new Operator(componentsOfExpression[index], null, null);
            index++;
            newNode.LeftChild = CreateTree(componentsOfExpression, ref index);
            newNode.RightChild = CreateTree(componentsOfExpression, ref index);
            return newNode;
        }

    }
}
