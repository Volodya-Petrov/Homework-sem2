using System;

namespace Routers
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PrimAlgorithm.WriteMaximunSpanningTree(args[0], args[1]);
            }
            catch (GraphIsNotConnectedException error)
            {
                Console.Error.WriteLine(error.Message);
            }
        }
    }
}
