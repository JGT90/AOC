using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2021.Functions.Day09;

namespace AdventOfCode2021.Functions {

    public class Day15 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_15_1.txt";
        int[,] mValues;
        public override void ReadIn() {

            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            List<string> lLines = new List<string>();
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                if (lLine == string.Empty) {
                    break;
                }
                lLines.Add(lLine);
            }
            mValues = new int[lLines[0].Length, lLines.Count];
            for (int y = 0; y < lLines.Count; y++) {
                for (int x = 0; x < lLines[y].Length; x++) {
                    int lHeight = lLines[y][x] - 48;
                    mValues[y, x] = lHeight;
                }
            }
            Console.WriteLine($"{mValues.Length} values found in file, {mPath}");
            return;
        }
        private int[,] ManipulateMatrix(int[,] aMatrix) {
            int rowLength = (int)Math.Sqrt(aMatrix.Length);
            int newRowLength = rowLength * 5;
            int[,] NewMatrix = new int[newRowLength, newRowLength];
            for (int y = 0; y < newRowLength; y++) {
                for (int x = 0; x < newRowLength; x++) {
                    int xIndex = x % rowLength;
                    int xAdd = x / rowLength;
                    int yIndex = y % rowLength;
                    int yAdd = y / rowLength;
                    int newValue = aMatrix[yIndex, xIndex] + xAdd + yAdd;
                    if (newValue > 9) {
                        NewMatrix[y, x] = newValue % 10 + 1;
                    } else {
                        NewMatrix[y, x] = newValue;
                    }
                }
            }

            return NewMatrix;
        }

        public override string DoPartA() {
            DikstraGraph g = new DikstraGraph(mValues.Length);
            return $"{g.Test(mValues)}";
        }

        public override string DoPartB() {
            mValues = ManipulateMatrix(mValues);
            DikstraGraph g = new DikstraGraph(mValues.Length);
            return $"{g.Test(mValues)}";
        }


    }
    #region Dijkstra
    public class DikstraGraph {
        private int v;
        int[] firstrow;
        // adjacency list
        private List<int>[] adjList;

        // Constructor
        public DikstraGraph(int vertices) {
            // initialise vertex count
            this.v = vertices;

            // initialise adjacency list
            initAdjList();
        }

        // utility method to initialise
        // adjacency list
        private void initAdjList() {
            adjList = new List<int>[v];

            for (int i = 0; i < v; i++) {
                adjList[i] = new List<int>();
            }
        }

        // add edge from u to v
        public void addEdge(int u, int v) {
            // Add v to u's list.
            adjList[u].Add(v);
        }

        private int MinDistance(int[] aDist, bool[] aVisited) {
            int min = int.MaxValue;
            int min_index = -1;
            for (int i = 0; i < aDist.Length; i++) {
                if (aVisited[i] == false && aDist[i] <= min) {
                    min = aDist[i];
                    min_index = i;
                }
            }
            return min_index;
        }


        public int Test(int[,] aMatrix) {

            int source = 0;
            int rowLength = (int)Math.Sqrt(aMatrix.Length);
            int[] dist = new int[aMatrix.Length];
            bool[] visited = new bool[aMatrix.Length];
            for (int i = 0; i < aMatrix.Length; i++) {
                dist[i] = int.MaxValue;
            }
            DikstraGraph g = new DikstraGraph(aMatrix.Length);
            int index = 0;
            for (int row = 0; row < aMatrix.Length / rowLength; row++) {
                for (int column = 0; column < rowLength; column++) {
                    // below
                    if (row < aMatrix.Length / rowLength - 1) {
                        g.addEdge(index, index + rowLength);
                    }
                    // next
                    if (column < rowLength - 1) {
                        g.addEdge(index, index + 1);
                    }
                    // above
                    if (row > 0) {
                        g.addEdge(index, index - rowLength);
                    }
                    // before
                    if (column > 0) {
                        g.addEdge(index, index - 1);
                    }
                    index++;
                }
            }
            //visited[source] = true;
            dist[source] = 0;
            List<int> q = new List<int>();
            q.Add(source);
            while (q.Count != 0) {
                index = q[0];
                visited[index] = true;
                q.RemoveAt(0);
                int lTempValue = int.MaxValue;
                int IndexShortest = 0;
                //int AllVisited = 0;
                for (int i = 0; i < g.adjList[index].Count; i++) {
                    int NewIndex = g.adjList[index][i];
                    if (!visited[NewIndex]) {
                        //AllVisited++;
                        int y = NewIndex / rowLength;
                        if (lTempValue > aMatrix[y, NewIndex - y * rowLength]) {
                            lTempValue = dist[index] + aMatrix[y, NewIndex - y * rowLength];
                            IndexShortest = NewIndex;

                        }
                        //q.Add(NewIndex);
                        int lNewDist = aMatrix[y, NewIndex - y * rowLength] + dist[index];
                        if (dist[NewIndex] > lNewDist) {
                            dist[NewIndex] = lNewDist;
                        }
                    }
                }
                visited[IndexShortest] = true;
                index = IndexShortest;
                for (int i = 0; i < g.adjList[index].Count; i++) {
                    int NewIndex = g.adjList[index][i];
                    if (!visited[NewIndex]) {
                        //AllVisited++;
                        int y = NewIndex / rowLength;
                        if (lTempValue > aMatrix[y, NewIndex - y * rowLength]) {
                            lTempValue = dist[index] + aMatrix[y, NewIndex - y * rowLength];
                            IndexShortest = NewIndex;

                        }
                        //q.Add(NewIndex);
                        int lNewDist = aMatrix[y, NewIndex - y * rowLength] + dist[index];
                        if (dist[NewIndex] > lNewDist) {
                            dist[NewIndex] = lNewDist;
                        }
                    }
                }
                int minindex = MinDistance(dist, visited);
                if (minindex != -1) {
                    q.Add(minindex);
                }
                //q.Add(MinDistance(dist, visited));
                //if (AllVisited > 1) {
                //    q.Insert(0, index);
                //}
            }
            return dist[aMatrix.Length - 1];
        }

    }
    #endregion
}
