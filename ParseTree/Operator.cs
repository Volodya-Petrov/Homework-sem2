using System;

namespace ParseTree
{   
    /// <summary>
    /// класс для операторов в арифмитическом выражении
    /// </summary>
    abstract class Operator : INode
    {
        public Operator(INode left, INode right)
        {
            LeftChild = left;
            RightChild = right;
        }

        public INode LeftChild { get; set; }

        public INode RightChild { get; set; }

        public abstract double Calculate();

        public virtual string Print()
            => " " + LeftChild.Print() + " " + RightChild.Print() + " )";
    }
}
