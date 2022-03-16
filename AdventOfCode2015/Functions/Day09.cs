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
            Dictionary<string, int> lDic = new Dictionary<string, int>();
            int i = 0;
            foreach (string aValue in mStrings) {
                string[] lsplit = aValue.Split('-');
                foreach (string lvalue in lsplit) {
                    if (!lDic.TryGetValue(lvalue, out int val)) {
                        lDic.Add(lvalue, i);
                        i++;
                    }
                }
            }

            // Create a sample graph
            Graph g = new Graph(lDic.Count);

            foreach (string aValue in aValues) {
                string[] lsplit = aValue.Split('-');
                if (lDic.TryGetValue(lsplit[0], out int val1)) {
                    if (lDic.TryGetValue(lsplit[1], out int val2)) {
                        g.addEdge(val1, val2);
                        g.addEdge(val2, val1);
                    }
                }
            }

            // arbitrary source
            int s = -1;
            lDic.TryGetValue("start", out s);
            //s = 2;

            // arbitrary destination
            int d = -1;
            lDic.TryGetValue("end", out d);
            //d = 3;
            //Console.WriteLine("Following are all different"
            //                  + " paths from " + s + " to " + d);
            g.printAllPaths(lDic, s, d, PartTwo);
            //return g
            return Graph.ResultList.Distinct().Count();
            return Result;
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
    }
}
