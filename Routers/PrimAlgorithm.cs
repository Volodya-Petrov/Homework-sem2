using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Routers
{   
    /// <summary>
    /// класс для реализации алгоритма прима
    /// </summary>
    public class PrimAlgorithm
    {   
        private static List<string[]> GetSplittedStrings(string path)
        {
            var stringsFromFile = File.ReadAllLines(path);
            var spittedStrings = new List<string[]>();
            for (int i = 0; i < stringsFromFile.Length; i++)
            {
                stringsFromFile[i] = stringsFromFile[i].Replace(":", "");
                stringsFromFile[i] = stringsFromFile[i].Replace("(", "");
                stringsFromFile[i] = stringsFromFile[i].Replace(")", "");
                stringsFromFile[i] = stringsFromFile[i].Replace(",", "");
                spittedStrings.Add(stringsFromFile[i].Split(' '));
            }
            return spittedStrings;
        }

        private static int GetCountOfVertices(string path)
        {
            var splittedStrings = GetSplittedStrings(path);
            var max = -1;
            for (int i = 0; i < splittedStrings.Count; i++)
            {
                for (int j = 1; j < splittedStrings[i].Length; j += 2)
                {
                    if (int.Parse(splittedStrings[i][j]) > max)
                    {
                        max = int.Parse(splittedStrings[i][j]);
                    }
                }
                if (int.Parse(splittedStrings[i][0]) > max)
                {
                    max = int.Parse(splittedStrings[i][0]);
                }
            }
            return max;
        }

        private static int[,] GetMatrixGraph(string path)
        {
            var countOfVertices = GetCountOfVertices(path);
            var graph = new int[countOfVertices, countOfVertices];
            var splittedStrings = GetSplittedStrings(path);
            for (int i = 0; i < splittedStrings.Count; i++)
            {
                var index1 = int.Parse(splittedStrings[i][0]) - 1;
                for (int j = 1; j < splittedStrings[i].Length; j += 2)
                {
                    var index2 = int.Parse(splittedStrings[i][j]) - 1;
                    graph[index1, index2] = 1;
                    graph[index2, index1] = 1;
                }
            }
            return graph;
        }
        
        private static void DFS(int startVetrex, bool[] visited, int[,] matrix)
        {
            if (visited[startVetrex])
            {
                return;
            }
            visited[startVetrex] = true;
            for (int i = 0; i < visited.Length; i++)
            {
                if (matrix[startVetrex, i] == 1)
                {
                    DFS(i, visited, matrix);
                }
            }
        }

        private static bool CheckConnectedGraph(string path)
        {
            var graph = GetMatrixGraph(path);
            var countOfVertices = (int)System.Math.Sqrt(graph.Length);
            var visited = new bool[countOfVertices];
            DFS(0, visited, graph);
            for (int i = 0; i < visited.Length; i++)
            {
                if (!visited[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static void ReadEdgesFromString(string[] splittedString, List<Edge> edges)
        {
            var index = 1;
            while (index < splittedString.Length)
            {
                edges.Add(new Edge(int.Parse(splittedString[index + 1]), int.Parse(splittedString[0]), int.Parse(splittedString[index])));
                index += 2;
            }
        }

        private static List<Edge> GetEdges(string path)
        {
            var edges = new List<Edge>();
            var stringsFromFile = GetSplittedStrings(path);
            for (int i = 0; i< stringsFromFile.Count; i++)
            {
                ReadEdgesFromString(stringsFromFile[i], edges);
            }
            return edges;
        }

        private static void WriteIntoFile(string path, List<Edge> edges)
        {
            using var file = new StreamWriter(path, false);
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
                    file.Write($"{edge.Vertex1}: {edge.Vertex2} ({edge.Weight})");
                    currentFirstVertex = edge.Vertex1;
                }
                else
                {
                    file.Write($", {edge.Vertex2} ({edge.Weight})");
                }
            }
            file.Close();
        }

        /// <summary>
        /// реализация алгоритма прима
        /// </summary>
        public static void WriteMaximunSpanningTree(string sourcePath, string destinationPath)
        {   
            if (!CheckConnectedGraph(sourcePath))
            {
                throw new GraphIsNotConnectedException();
            }
            var maximumSpanningTree = new List<Edge>();
            var usedVertices = new List<int>();
            var notUsedVertices = new List<int>();
            var notUsedEdges = GetEdges(sourcePath);
            var countOfVertices = GetCountOfVertices(sourcePath);
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
