using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day09 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_09_1.txt";
        Point[,] mValues;
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
            mValues = new Point[lLines[0].Length, lLines.Count];
            for (int y = 0; y < lLines.Count; y++) {
                for (int x = 0; x < lLines[y].Length; x++) {
                    int lHeight = lLines[y][x] - 48;
                    mValues[y, x] = new Point(lHeight);
                }
            }
            Console.WriteLine($"{mValues.Length} values found in file, {mPath}");
            return;
        }
        public override string DoPartA() {
            mValues = GetLowPoint();
            int lRiskLevel = 0;
            foreach (Point aValue in mValues) {
                if (aValue.IsLowPoint) {
                    lRiskLevel += aValue.Height + 1;
                }
            }
            return $"{lRiskLevel}";
        }
        private Point[,] GetLowPoint() {
            for (int i = 0; i < 100; i++) {
                for (int y = 0; y < 100; y++) {
                    mValues[i, y].IsLowPoint = true;
                    int lHeight = mValues[i, y].Height;
                    // Above
                    if (y > 0) {
                        if (lHeight >= mValues[i, y - 1].Height) {
                            mValues[i, y].IsLowPoint = false;
                            continue;
                        }
                    }
                    // Below
                    if (y < 99) {
                        if (lHeight >= mValues[i, y + 1].Height) {
                            mValues[i, y].IsLowPoint = false;
                            continue;
                        }
                    }
                    // Next
                    if (i < 99) {
                        if (lHeight >= mValues[i + 1, y].Height) {
                            mValues[i, y].IsLowPoint = false;
                            continue;
                        }
                    }
                    // Before
                    if (i > 0) {
                        if (lHeight >= mValues[i - 1, y].Height) {
                            mValues[i, y].IsLowPoint = false;
                            continue;
                        }
                    }
                }
            }
            return mValues;
        }

        public override string DoPartB() {
            int BasinIndex = 0;
            int[,] bla = new int[2, 3];
            for (int y = 0; y < 100; y++) {
                for (int x = 0; x < 100; x++) {
                    if (!mValues[y, x].IsLowPoint) {
                        continue;
                    }
                    if (BasinIndex == 244) {
                        int a = 0;
                    }
                    mValues = DetermineBasins(mValues, y, x, BasinIndex);
                    BasinIndex++;
                }
            }
            List<int> BasinSize = new List<int>();
            for (int i = 0; i < BasinIndex; i++) {
                int lBasinSize = 0;
                foreach (Point lPoint in mValues) {
                    if (lPoint.BasinIndex == i) {
                        lBasinSize ++;
                    }
                }
                BasinSize.Add(lBasinSize);
            }
            int Largest = 0;
            int SecondLargest = 0;
            int ThirdLargest = 0;
            foreach (int lSize in BasinSize) {
                if (lSize > Largest) {
                    ThirdLargest = SecondLargest;
                    SecondLargest = Largest;
                    Largest = lSize;
                    continue;
                } else if (lSize > SecondLargest) {
                    ThirdLargest = SecondLargest;
                    SecondLargest = lSize;
                    continue;
                } else if (lSize > ThirdLargest) {
                    ThirdLargest = lSize;
                    continue;
                }

            }
            return $"{Largest * SecondLargest * ThirdLargest}";
        }

        private static Point[,] DetermineBasins(Point[,] mValues, int aY, int aX, int aBasinIndex) {
            int lHeight = mValues[aY, aX].Height;
            mValues[aY, aX].BasinIndex = aBasinIndex;
            // Before
            if (aX > 0) {
                if (mValues[aY, aX - 1].Height != 9 && mValues[aY, aX - 1].BasinIndex == -1) {
                    if (lHeight < mValues[aY, aX - 1].Height) {
                        //mValues[aY, aX - 1].BasinIndex = aBasinIndex;
                        mValues = DetermineBasins(mValues, aY, aX - 1, aBasinIndex);
                    }
                }
            }
            // Next
            if (aX < 99) {
                if (mValues[aY, aX + 1].Height != 9 && mValues[aY, aX + 1].BasinIndex == -1) {
                    if (lHeight < mValues[aY, aX + 1].Height) {
                        //mValues[aY, aX + 1].BasinIndex = aBasinIndex;
                        mValues = DetermineBasins(mValues, aY, aX + 1, aBasinIndex);
                    }
                }
            }
            // Below
            if (aY < 99) {
                if (mValues[aY + 1, aX].Height != 9 && mValues[aY + 1, aX].BasinIndex == -1) {
                    if (lHeight < mValues[aY + 1, aX].Height) {
                        //mValues[aY + 1, aX].BasinIndex = aBasinIndex;
                        mValues = DetermineBasins(mValues, aY + 1, aX, aBasinIndex);
                    }
                }
            }
            // Above
            if (aY > 0) {
                if (mValues[aY - 1, aX].Height != 9 && mValues[aY - 1, aX].BasinIndex == -1) {
                    if (lHeight < mValues[aY - 1, aX].Height) {
                        //mValues[aY - 1, aX].BasinIndex = aBasinIndex;
                        mValues = DetermineBasins(mValues, aY - 1, aX, aBasinIndex);
                    }
                }
            }
            return mValues;
        }
        public class Point {
            public int Height;
            public bool IsLowPoint;
            public int BasinIndex = -1;
            public int Basin;
            public Point(int aHeight) {
                Height = aHeight;
            }
        }
    }
}
