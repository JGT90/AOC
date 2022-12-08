using SEGCC;
using System;
using System.Windows;

namespace AOC2021 {
    internal class Week05 : DayN {
        static Point[] mStartPoints;
        static Point[] mEndPoints;
        static int[,] mArea;
        const int AREA_SIZE = 1000;
        public override string Part1() {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week05.txt");
            mStartPoints = new Point[lines.Length];
            mEndPoints = new Point[lines.Length];
            mArea = new int[AREA_SIZE, AREA_SIZE];
            for (int i = 0; i < lines.Length; i++) {
                string[] lSplits = lines[i].Split(new string[] { ",", "->" }, StringSplitOptions.RemoveEmptyEntries);
                mStartPoints[i] = new Point(Convert.ToDouble(lSplits[0]), Convert.ToDouble(lSplits[1]));
                mEndPoints[i] = new Point(Convert.ToDouble(lSplits[2]), Convert.ToDouble(lSplits[3]));
            }
            for (int i = 0; i < mStartPoints.Length; i++) {
                if (!IsVerticalOrHorizontal(i)) continue;
                int lXStart = mStartPoints[i].X < mEndPoints[i].X ? (int)mStartPoints[i].X : (int)mEndPoints[i].X;
                int lXEnd = mStartPoints[i].X > mEndPoints[i].X ? (int)mStartPoints[i].X : (int)mEndPoints[i].X;
                int lYStart = mStartPoints[i].Y < mEndPoints[i].Y ? (int)mStartPoints[i].Y : (int)mEndPoints[i].Y;
                int lYEnd = mStartPoints[i].Y > mEndPoints[i].Y ? (int)mStartPoints[i].Y : (int)mEndPoints[i].Y;
                for (int x = lXStart; x < lXEnd + 1; x++) {
                    for (int y = lYStart; y < lYEnd + 1; y++) {
                        mArea[y, x] += 1;
                    }
                }
            }
            int lOverlapCount = 0;
            for (int x = 0; x < AREA_SIZE; x++) {
                for (int y = 0; y < AREA_SIZE; y++) {
                    if (mArea[y, x] >= 2) lOverlapCount++;
                }
            }
            //Console.Write("Week05 - PartA: ");
            //Console.WriteLine(lOverlapCount);
            return lOverlapCount.ToString();
        }

        public override string Part2() {
            mArea = new int[AREA_SIZE, AREA_SIZE];
            for (int i = 0; i < mStartPoints.Length; i++) {
                if (IsVerticalOrHorizontal(i)) {
                    int lXStart = mStartPoints[i].X < mEndPoints[i].X ? (int)mStartPoints[i].X : (int)mEndPoints[i].X;
                    int lXEnd = mStartPoints[i].X > mEndPoints[i].X ? (int)mStartPoints[i].X : (int)mEndPoints[i].X;
                    int lYStart = mStartPoints[i].Y < mEndPoints[i].Y ? (int)mStartPoints[i].Y : (int)mEndPoints[i].Y;
                    int lYEnd = mStartPoints[i].Y > mEndPoints[i].Y ? (int)mStartPoints[i].Y : (int)mEndPoints[i].Y;
                    for (int x = lXStart; x < lXEnd + 1; x++) {
                        for (int y = lYStart; y < lYEnd + 1; y++) {
                            mArea[y, x] += 1;
                        }
                    }
                } else if (IsDiagonal(i)) {
                    if (mStartPoints[i].X < mEndPoints[i].X) {
                        for (int x = (int)mStartPoints[i].X; x < mEndPoints[i].X + 1;) {
                            if (mStartPoints[i].Y < mEndPoints[i].Y) {
                                for (int y = (int)mStartPoints[i].Y; y < mEndPoints[i].Y + 1; y++, x++) {
                                    mArea[y, x] += 1;
                                }
                            } else {
                                for (int y = (int)mStartPoints[i].Y; y > mEndPoints[i].Y - 1; y--, x++) {
                                    mArea[y, x] += 1;
                                }
                            }
                        }
                    } else {
                        for (int x = (int)mStartPoints[i].X; x > mEndPoints[i].X - 1;) {
                            if (mStartPoints[i].Y < mEndPoints[i].Y) {
                                for (int y = (int)mStartPoints[i].Y; y < mEndPoints[i].Y + 1; y++, x--) {
                                    mArea[y, x] += 1;
                                }
                            } else {
                                for (int y = (int)mStartPoints[i].Y; y > mEndPoints[i].Y - 1; y--, x--) {
                                    mArea[y, x] += 1;
                                }
                            }
                        }
                    }
                }
            }
            int lOverlapCount = 0;
            for (int x = 0; x < AREA_SIZE; x++) {
                for (int y = 0; y < AREA_SIZE; y++) {
                    if (mArea[y, x] >= 2) lOverlapCount++;
                }
            }

            //Console.Write("Week05 - PartB: ");
            //Console.WriteLine(lOverlapCount);
            return lOverlapCount.ToString();
        }

        private bool IsVerticalOrHorizontal(int aIndex) {
            if (mStartPoints[aIndex].X == mEndPoints[aIndex].X || mStartPoints[aIndex].Y == mEndPoints[aIndex].Y) return true;
            return false;
        }
        private bool IsDiagonal(int aIndex) {
            if (Math.Abs(mStartPoints[aIndex].X - mEndPoints[aIndex].X) == Math.Abs(mStartPoints[aIndex].Y - mEndPoints[aIndex].Y)) return true;
            return false;
        }
    }
}
