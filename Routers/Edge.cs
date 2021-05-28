namespace Routers
{
    class Edge
    {   
        public Edge(int weight, int vertex1, int vertex2)
        {
            this.weight = weight;
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
        }
        public int Vertex1 { get => vertex1; }

        public int Vertex2 { get => vertex2; }

        public int Weight { get => weight; }

        int weight;
        int vertex1;
        int vertex2;
    }
}
