using System;

namespace ParseTree
{   
    /// <summary>
    /// класс для оператора "/"
    /// </summary>
    class DivisionOperator : Operator
    {
        public DivisionOperator(INode left, INode right) : base(left, right) { }

        public override double Calculate()
        {
            if (Math.Abs(RightChild.Calculate()) < double.Epsilon)
            {
                throw new DivideByZeroException();
            }
            return LeftChild.Calculate() / RightChild.Calculate();
        }

        public override string Print() => "( /" + base.Print();
    }
}