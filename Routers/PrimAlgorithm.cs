using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Routers
{
    class PrimAlgorithm
    {

        private static int ReadNumber(string str, ref int index)
        {
            var endIndex = index;
            int number;
            while (endIndex != str.Length)
            {
                if (!int.TryParse(str[endIndex].ToString(), out number))
                {
                    number = int.Parse(str.Substring(index, endIndex - index));
                    index = endIndex;
                    return number;
                }
                endIndex++;
            }
            number = int.Parse(str.Substring(index, endIndex - index));
            index = endIndex;
            return number;
        }

        private static void ReadEdgesFromString(string str, List<Edge> edges)
        {
            var index = 0;
            int tryParse;
            var lastNumber = 0;
            var firstVertex = 0;
            var secondVertex = 0;
            while (index != str.Length)
            {
                if (int.TryParse(str[index].ToString(), out tryParse))
                {
                    lastNumber = ReadNumber(str, ref index);
                    continue;
                }
                else if (str[index] == ':')
                {
                    firstVertex = lastNumber;
                }
                else if (str[index] == '(')
                {
                    secondVertex = lastNumber;
                }
                else if (str[index] == ')')
                {
                    edges.Add(new Edge(lastNumber, firstVertex, secondVertex));
                }
                index++;
            }
        }

        private static (List<Edge> edges, int countOfVertices) GetEdges(string path)
        {
            var edges = new List<Edge>();
            var stringsFromFile = File.ReadAllLines(path);
            for (int i = 0; i< stringsFromFile.Length; i++)
            {
                ReadEdgesFromString(stringsFromFile[i], edges);
            }
            var max = 0;
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].Vertex1 > max)
                {
                    max = edges[i].Vertex1;
                }
                if (edges[i].Vertex2 > max)
                {
                    max = edges[i].Vertex2;
                }
            }
            return (edges, max);
        }

        private static void WriteIntoFile(string path, List<Edge> edges)
        {
            using var file = new StreamWriter(path, true);
            var sortedEdges = edges.OrderBy(edge => edge.Vertex1.ToString() + edge.Vertex2.ToString());
            int currentFirstVertex = -1;
            foreach(Edge edge in sortedEdges)
            {
                if (edge.Vertex1 != currentFirstVertex)
                {
                    if (currentFirstVertex != -1)
                    {
                        file.Write("\n");
                    }
                    file.Write($"{edge.Vertex1}: {edge.Vertex2}({edge.Weight})");
                    currentFirstVertex = edge.Vertex1;
                }
                else
                {
                    file.Write($", {edge.Vertex2}({edge.Weight})");
                }
            }
            file.Close();
        }

        public static void WriteMaximunSpanningTree(string sourcePath, string destinationPath)
        {
            var maximumSpanningTree = new List<Edge>();
            var usedVertices = new List<int>();
            var notUsedVertices = new List<int>();
            var edgesAndVetices = GetEdges(sourcePath);
            var notUsedEdges = edgesAndVetices.edges;
            var countOfVertices = edgesAndVetices.countOfVertices;
            for (int i = 1; i < countOfVertices + 1; i++)
            {
                notUsedVertices.Add(i);
            }
            usedVertices.Add(1);
            notUsedVertices.Remove(1);
            while (notUsedVertices.Count > 0)
            {
                int max = -1;
                for (int i = 0; i < notUsedEdges.Count; i++)
                {
                    if ((usedVertices.IndexOf(notUsedEdges[i].Vertex1) != -1 && notUsedVertices.IndexOf(notUsedEdges[i].Vertex2) != -1) ||
                        (usedVertices.IndexOf(notUsedEdges[i].Vertex2) != -1 && notUsedVertices.IndexOf(notUsedEdges[i].Vertex1) != -1))
                    {
                        if (max == -1)
                        {
                            max = i;
                        }
                        else
                        {
                            if (notUsedEdges[i].Weight > notUsedEdges[max].Weight)
                            {
                                max = i;
                            }
                        }
                    }
                }
                if (usedVertices.IndexOf(notUsedEdges[max].Vertex1) != -1)
                {
                    usedVertices.Add(notUsedEdges[max].Vertex2);
                    notUsedVertices.Remove(notUsedEdges[max].Vertex2);
                }
                else
                {
                    usedVertices.Add(notUsedEdges[max].Vertex1);
                    notUsedVertices.Remove(notUsedEdges[max].Vertex1);
                }
                maximumSpanningTree.Add(notUsedEdges[max]);
                notUsedEdges.RemoveAt(max);
            }
            WriteIntoFile(destinationPath, maximumSpanningTree);
        }
    }
}
