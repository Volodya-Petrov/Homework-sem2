using System;

namespace ParseTree
{
    class Operator : INode
    {
        public Operator(string symbol, INode left, INode right)
        {
            Symbol = symbol;
            LeftChild = left;
            RightChild = right;
        }

        public INode LeftChild { get; set; }

        public INode RightChild { get; set; }

        public string Symbol { get; set; }

        /// <summary>
        /// считает значение поддерева, корень которого данный узел
        /// </summary>
        public double Calculate()
        {
            var leftValue = LeftChild.Calculate();
            var rightValue = RightChild.Calculate();
            switch(Symbol)
            {
                case "+":
                    return leftValue + rightValue;
                case "-":
                    return leftValue - rightValue;
                case "*":
                    return leftValue * rightValue;
                case "/":
                    if (Math.Abs(rightValue) < double.Epsilon)
                    {
                        throw new DivideByZeroException();
                    }
                    return leftValue / rightValue;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// выводит арифмитическое выражение поддерева, корень которого данный узел
        /// </summary>
        public string Print()
        {
            return "( " + Symbol + " " + LeftChild.Print() + " " + RightChild.Print() + " )";
        }
    }
}
