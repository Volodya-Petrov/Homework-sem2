namespace ParseTree
{
    class Operand : INode
    {
        public Operand(double value) => Value = value;

        public double Value { get; set; }

        /// <summary>
        /// считает значение поддерева, корень которого данный узел
        /// </summary>
        public double Calculate() => Value;

        /// <summary>
        /// выводит арифмитическое выражение поддерева, корень которого данный узел
        /// </summary>
        public string Print() => Value.ToString();
    }
}
