namespace ParseTree
{   
    /// <summary>
    /// класс для оператора "-"
    /// </summary>
    class MultiplicationOperator : Operator
    {
        public MultiplicationOperator(INode left, INode right) : base(left, right) { }

        public override double Calculate() => LeftChild.Calculate() * RightChild.Calculate();

        public override string Print() => "( *" + base.Print();
    }
}
