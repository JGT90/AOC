using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day13 : AdventOfCode.AdventOfCode{
        const string mPath = @"..\..\InputFiles\Input12_13_1.txt";
        List<string> mValues;
        List<string> mFold;
        public override void ReadIn() {
            mFold = new List<string>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            mValues = new List<string>();
            bool NowFillFoldList = false;
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                if (lLine == string.Empty) {
                    NowFillFoldList = true;
                    continue;
                }
                if (NowFillFoldList) {
                    mFold.Add(lLine);
                } else {
                    mValues.Add(lLine);
                }
            }
            Console.WriteLine($"{mValues.Count} values found in file, {mPath}");
            return;
        }

        public override string DoPartA() {
            return $"{CountDotsAfterFold(mValues, mFold, 1)}";
        }

        public override string DoPartB() {
            return $"{CountDotsAfterFold(mValues, mFold, 12)}";
        }

        private int CountDotsAfterFold(List<string> aValues, List<string> aFoldValues, int aFoldCount) {
            int xsize = 0;
            int ysize = 0;
            for (int i = 0; i < 2; i++) {
                string[] lSplit = aFoldValues[i].Split(new char[] { '=' }, 2);
                if (lSplit[0].Contains("x")) {
                    xsize = int.Parse(lSplit[1]);
                } else {
                    ysize = int.Parse(lSplit[1]);
                }
            }
            int[,] DotArray = new int[xsize * 2 + 1, ysize * 2 + 1];
            foreach (string aValue in aValues) {
                string[] lSplit = aValue.Split(',');
                int x = int.Parse(lSplit[0]);
                int y = int.Parse(lSplit[1]);
                DotArray[x, y] = 1;
            }
            int DotCount = 0;
            for (int i = 0; i < aFoldCount; i++) {
                string[] lSplit = aFoldValues[i].Split(new char[] { '=' }, 2);
                int x = -1;
                int y = -1;
                if (lSplit[0].Contains("x")) {
                    x = int.Parse(lSplit[1]);
                } else {
                    y = int.Parse(lSplit[1]);
                }
                int[,] lDotArray = FoldArray(DotArray, out DotCount, x, y);
                DotArray = lDotArray;
            }
            var row = GetRow(DotArray, 0);
            if (aFoldCount > 11) {
                for (int j = 0; j < row.Length; j++) {
                    for (int ix = 0; ix < DotArray.Length / row.Length; ix++) {
                        if (DotArray[ix,j] == 1) {
                            Console.ForegroundColor = ConsoleColor.Red;
                        } else {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write(string.Format("{0} ", DotArray[ix, j]));
                    }
                    Console.Write(Environment.NewLine /*+ Environment.NewLine*/);
                }
                //Console.ReadLine();

            }
            return DotCount;
        }

        public static int[] GetRow(int[,] matrix, int rowNumber) {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }

        private static int[,] FoldArray(int[,] aFoldArray, out int aDotCount, int x = -1, int y = -1) {
            int[,] lFold = new int[0, 0];
            aDotCount = 0;
            if (x > 0) {
                // Fold vertical
                int ysize = aFoldArray.Length / (x * 2 + 1);
                lFold = new int[x, ysize];
                for (int ix = 0; ix < x; ix++) {
                    for (int iy = 0; iy < ysize; iy++) {
                        if (aFoldArray[ix, iy] > 0 || aFoldArray[x * 2 - ix, iy] > 0) {
                            lFold[ix, iy] = 1;
                            aDotCount++;
                        }
                    }
                }
            } else if (y > 0) {
                // Fold horizontal
                int xsize = aFoldArray.Length / (y * 2 + 1);
                lFold = new int[xsize, y];
                for (int ix = 0; ix < xsize; ix++) {
                    for (int iy = 0; iy < y; iy++) {
                        if (aFoldArray[ix, iy] > 0 || aFoldArray[ix, y * 2 - iy] > 0) {
                            lFold[ix, iy] = 1;
                            aDotCount++;
                        }
                    }
                }
            }
            return lFold;
        }
    }
}
