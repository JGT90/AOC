using AdventOfCode2021.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Functions {
    public class Day09 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_09_1.txt";
        List<string> mStrings;

        public override string DoPartA() {
            bool[] lVisited = new bool[8];
            return "";
        }

        public override string DoPartB() {
            throw new NotImplementedException();
        }

        public override void ReadIn() {
            mStrings = new List<string>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                mStrings.Add(lLine);
            }
            return;
        }

        private class Graph {
            public static List<string> ResultList = new List<string>();
            // No. of vertices in graph
            private int v;

            // adjacency list
            private List<int>[] adjList;

            //private static bool aTwice = false;
            // Constructor
            public Graph(int vertices) {
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

            // Prints all paths from
            // 's' to 'd'
            public void printAllPaths(Dictionary<string, int> aDic, int s, int d, bool PartTwo) {
                bool[] isVisited = new bool[v];
                List<string> pathList = new List<string>();

                // add source to path[]
                pathList.Add(aDic.ElementAt(s).Key);

                // Call recursive utility
                printAllPathsUtil(aDic, s, d, isVisited, pathList, PartTwo);
            }

            // A recursive function to print
            // all paths from 'u' to 'd'.
            // isVisited[] keeps track of
            // vertices in current path.
            // localPathList<> stores actual
            // vertices in the current path
            private void printAllPathsUtil(Dictionary<string, int> aDic, int u, int d,
                                           bool[] isVisited,
                                           List<string> localPathList, bool PartTwo) {

                if (u.Equals(d)) {
                    ResultList.Add(string.Join(",", localPathList));
                    //Console.WriteLine(string.Join(",", localPathList));
                    // if match found then no need
                    // to traverse more till depth
                    //Result++;
                    return;
                }
                isVisited[aDic["start"]] = true;
                bool aTwice = false;
                if (PartTwo) {
                    foreach (string edge in localPathList) {
                        if (!IsAllUpper(edge) && localPathList.IndexOf(edge) != localPathList.LastIndexOf(edge)) {
                            aTwice = true;
                        }
                    }
                }
                if (!IsAllUpper(aDic.ElementAt(u).Key)) {
                    if (aTwice || !PartTwo) {
                        // Mark the current node
                        isVisited[u] = true;

                    }

                }

                // Recur for all the vertices
                // adjacent to current vertex
                foreach (int i in adjList[u]) {
                    if (!isVisited[i]) {
                        if (localPathList.Last() != aDic.ElementAt(i).Key) {
                            if (PartTwo) {
                                if (!IsAllUpper(aDic.ElementAt(i).Key) && aTwice && localPathList.Contains(aDic.ElementAt(i).Key)) {
                                    continue;
                                }

                            }
                            //if (!IsAllUpper(aDic.ElementAt(i).Key) && localPathList.Contains(aDic.ElementAt(i).Key) && aDic.ElementAt(i).Key != "start") {
                            //    //aTwice = true;
                            //}
                            //if (Result == 7) {
                            //    int a = 0;
                            //}
                            // store current node
                            // in path[]
                            localPathList.Add(aDic.ElementAt(i).Key);

                            printAllPathsUtil(aDic, i, d, isVisited,
                                              localPathList, PartTwo);

                        }

                        // remove current node
                        // in path[]
                        //localPathList.Remove(aDic.ElementAt(i).Key);
                        localPathList.RemoveAt(localPathList.Count - 1);


                    }
                }

                // Mark the current node
                isVisited[u] = false;
            }

            bool IsAllUpper(string input) {
                for (int i = 0; i < input.Length; i++) {
                    if (!Char.IsUpper(input[i]))
                        return false;
                }

                return true;
            }
        }
    }
}
