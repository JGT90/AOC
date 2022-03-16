using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day11 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_11_1.txt";
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

        public override string DoPartB() {
            return $"{DoWorkB(mValues, 1000)}";
        }
        private int DoWorkB(int[,] aValues, int aSteps) {
            int lFlashCount = 0;
            for (int i = 0; i < aSteps; i++) {
                bool[,] lFlashed = new bool[10, 10];
                aValues = Increase(aValues);
                while (Flashable(aValues, out int X, out int Y)) {
                    lFlashCount++;
                    aValues = Flash(aValues, lFlashed, X, Y);
                    lFlashed[Y, X] = true;
                }
                bool lAllFlashed = true;
                for (int y = 0; y < 10; y++) {
                    for (int x = 0; x < 10; x++) {
                        if (!lFlashed[y, x]) {
                            lAllFlashed = false;
                        }
                    }
                }
                if (lAllFlashed) {
                    return i+1;
                }
            }
            return lFlashCount;
        }

        public override string DoPartA() {
            return $"{DoWorkA(mValues, 100)}";
        }
        private int DoWorkA(int[,] aValues, int aSteps) {
            int lFlashCount = 0;
            for (int i = 0; i < aSteps; i++) {
                bool[,] lFlashed = new bool[10, 10];
                aValues = Increase(aValues);
                while (Flashable(aValues, out int X, out int Y)) {
                    lFlashCount++;
                    aValues = Flash(aValues, lFlashed, X, Y);
                    lFlashed[Y, X] = true;
                }
            }
            return lFlashCount;
        }
        private static int[,] Increase(int[,] aValues) {
            for (int y = 0; y < 10; y++) {
                for (int x = 0; x < 10; x++) {
                    aValues[y, x]++;
                }
            }
            return aValues;
        }

        private static bool Flashable(int[,] aValues, out int X, out int Y) {
            for (int y = 0; y < 10; y++) {
                for (int x = 0; x < 10; x++) {
                    if (aValues[y, x] > 9) {
                        X = x;
                        Y = y;
                        return true;
                    }
                }
            }
            Y = -1;
            X = -1;
            return false;
        }

        private static int[,] Flash(int[,] aValues, bool[,] aFlashed, int aX, int aY) {
            aValues[aY, aX] = 0;
            // Above
            if (aY != 0) {
                if (!aFlashed[aY - 1, aX]) {
                    aValues[aY - 1, aX]++;
                }
            }
            // Below
            if (aY != 9) {
                if (!aFlashed[aY + 1, aX]) {
                    aValues[aY + 1, aX]++;
                }
            }
            // Next 
            if (aX != 9) {
                if (!aFlashed[aY, aX + 1]) {
                    aValues[aY, aX + 1]++;
                }
            }
            // Before
            if (aX != 0) {
                if (!aFlashed[aY, aX - 1]) {
                    aValues[aY, aX - 1]++;
                }
            }
            // LeftTopDiagonal
            if (aX != 0 && aY != 0) {
                if (!aFlashed[aY - 1, aX - 1]) {
                    aValues[aY - 1, aX - 1]++;
                }
            }
            // RightTopDiagonal
            if (aX != 9 && aY != 0) {
                if (!aFlashed[aY - 1, aX + 1]) {
                    aValues[aY - 1, aX + 1]++;
                }
            }
            // LeftBottomDiagonal
            if (aX != 0 && aY != 9) {
                if (!aFlashed[aY + 1, aX - 1]) {
                    aValues[aY + 1, aX - 1]++;
                }
            }
            // RightBottomDiagonal
            if (aX != 9 && aY != 9) {
                if (!aFlashed[aY + 1, aX + 1]) {
                    aValues[aY + 1, aX + 1]++;
                }
            }
            return aValues;
        }

    }
}
