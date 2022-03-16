using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day05 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_05_1.txt";
        int[,] mLines;
        int[,] mDiags;
        public override void ReadIn() {
            mLines = new int[1000, 1000];
            mDiags = new int[1000, 1000];
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                if (IsValidLine(lLine)) {
                    mLines = AddMatrix(mLines, GetLineSegment(lLine));
                }
                if (IsValidDiag(lLine)) {
                    mDiags = AddMatrix(mDiags, GetLineSegment(lLine));
                }
            }

            return;
        }

        private bool IsValidLine(string aLine) {
            string[] aSplit = aLine.Split(',', ' ');
            if (aSplit[0] == aSplit[3] || aSplit[1] == aSplit[4]) {
                return true;
            }
            return false;
        }
        private bool IsValidDiag(string aLine) {
            string[] aSplit = aLine.Split(',', ' ');
            if (aSplit[0] == aSplit[3] || aSplit[1] == aSplit[4]) {
                return true;
            }
            int xdiff = int.Parse(aSplit[0]) - int.Parse(aSplit[3]);
            int ydiff = int.Parse(aSplit[1]) - int.Parse(aSplit[4]);
            if (Math.Abs(xdiff) == Math.Abs(ydiff)) {
                return true;
            }
            return false;
        }

        public static int[,] GetLineSegment(string aLine) {
            int[,] aSegment = new int[1000, 1000];
            string[] aSplit = aLine.Split(',', ' ');
            if (aSplit[0] == aSplit[3]) {
                // vertical
                int x = int.Parse(aSplit[0]);
                int y1 = int.Parse(aSplit[1]);
                int y2 = int.Parse(aSplit[4]);
                int yStart = y1 < y2 ? y1 : y2;
                int yStop = y1 < y2 ? y2 : y1;
                for (int i = yStart; i <= yStop; i++) {
                    aSegment[x, i] = 1;
                }
            } else if (aSplit[1] == aSplit[4]) {
                // horizontal
                int y = int.Parse(aSplit[1]);
                int x1 = int.Parse(aSplit[0]);
                int x2 = int.Parse(aSplit[3]);
                int xStart = x1 < x2 ? x1 : x2;
                int xStop = x1 < x2 ? x2 : x1;
                for (int i = xStart; i <= xStop; i++) {
                    aSegment[i, y] = 1;
                }
            } else {
                int yStart = int.Parse(aSplit[1]);
                int yStop = int.Parse(aSplit[4]);
                bool yAdd = yStart < yStop ? true : false;
                int xStart = int.Parse(aSplit[0]);
                int xStop = int.Parse(aSplit[3]);
                bool xAdd = xStart < xStop ? true : false;
                if (xAdd) {
                    for (int i = xStart; i <= xStop; i++) {
                        if (yAdd) {
                            for (int y = yStart; y <= yStop; y++,i++) {
                                aSegment[i, y] = 1;
                            }
                        } else {
                            for (int y = yStart; y >= yStop; y--,i++) {
                                aSegment[i, y] = 1;
                            }
                        }
                    }
                }else {
                    for (int i = xStart; i >= xStop; i--) {
                        if (yAdd) {
                            for (int y = yStart; y <= yStop; y++,i--) {
                                aSegment[i, y] = 1;
                            }
                        } else {
                            for (int y = yStart; y >= yStop; y--,i--) {
                                aSegment[i, y] = 1;
                            }
                        }
                    }
                }
                for (int i = xStart; i <= xStop; i++) {
                    for (int y = yStart; y <= yStop; y++, i++) {
                        aSegment[i, y] = 1;
                    }
                }
            }
            return aSegment;
        }

        public static int[,] AddMatrix(int[,] aFirst, int[,] aSecond) {
            int[,] aResult = new int[1000, 1000];
            for (int i = 0; i < 1000; i++) {
                for (int y = 0; y < 1000; y++) {
                    aResult[i, y] = aFirst[i, y] + aSecond[i, y];
                }
            }
            return aResult;
        }
        public override string DoPartA() {
            return $"{DoWork(mLines)}";
        }

        public override string DoPartB() {
            return $"{DoWork(mDiags)}";
        }
        public int DoWork(int[,] aSegment) {
            List<string> lLines = new List<string>();
            int lResult = 0;
            for (int i = 0; i < 1000; i++) {
                string lLine = string.Empty;
                for (int y = 0; y < 1000; y++) {
                    lLine += $"{aSegment[i, y]}";
                    if (aSegment[i, y] > 1) {
                        lResult++;
                    }
                }
                lLines.Add(lLine);

            }
            //File.WriteAllLines("Output12_5", lLines.ToArray());
            return lResult;
        }
    }
}
