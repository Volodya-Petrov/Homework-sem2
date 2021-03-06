﻿namespace ParseTree
{   
    /// <summary>
    /// интерфейс узла дерева
    /// </summary>
    interface INode
    {   
        /// <summary>
        /// считает значение узла
        /// </summary>
        public double Calculate();

        /// <summary>
        /// печает выражение в узле на консоль
        /// </summary>
        public string Print();
    }
}
