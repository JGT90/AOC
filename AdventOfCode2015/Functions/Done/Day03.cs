using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Functions {
    public class Day03 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_03_1.txt";
        string mString;
        public override string DoPartA() {
            int lVertical = 0;
            int lHorizontal = 0;
            bool[,] lDone = new bool[10000, 10000];
            int lOffset = 5000;
            lDone[lOffset, lOffset] = true;
            double lHousesVisited = 1;
            foreach (char c in mString) {
                if (c == 'v') lVertical--;
                else if (c == '^') lVertical++;
                else if (c == '<') lHorizontal--;
                else if (c == '>') lHorizontal++;
                if (!lDone[lOffset+lVertical, lOffset + lHorizontal]) {
                    lDone[lOffset + lVertical, lOffset + lHorizontal] = true;
                    lHousesVisited++;
                }
            }
            return $"{lHousesVisited}";
        }

        public override string DoPartB() {
            int lVertical = 0;
            int lHorizontal = 0;
            int lVerticalRobo = 0;
            int lHorizontalRobo = 0;
            bool[,] lDone = new bool[10000, 10000];
            int lOffset = 5000;
            lDone[lOffset, lOffset] = true;
            double lHousesVisited = 1;
            bool lSantaTurns = true;
            foreach (char c in mString) {
                if (lSantaTurns) {
                    if (c == 'v') lVerticalRobo--;
                    else if (c == '^') lVerticalRobo++;
                    else if (c == '<') lHorizontalRobo--;
                    else if (c == '>') lHorizontalRobo++;
                    if (!lDone[lOffset + lVerticalRobo, lOffset + lHorizontalRobo]) {
                        lDone[lOffset + lVerticalRobo, lOffset + lHorizontalRobo] = true;
                        lHousesVisited++;
                    }
                } else {
                    if (c == 'v') lVertical--;
                    else if (c == '^') lVertical++;
                    else if (c == '<') lHorizontal--;
                    else if (c == '>') lHorizontal++;
                    if (!lDone[lOffset + lVertical, lOffset + lHorizontal]) {
                        lDone[lOffset + lVertical, lOffset + lHorizontal] = true;
                        lHousesVisited++;
                    }
                }
                lSantaTurns = !lSantaTurns;
            }
            return $"{lHousesVisited}";
        }

        public override void ReadIn() {
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                mString = lLine;
            }
            return;
        }
    }
}
