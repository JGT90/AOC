using SEGCC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021 {
    internal class Day17 : DayN {
        int mTargetX1;
        int mTargetX2;
        int mTargetY1;
        int mTargetY2;
        List<int> mPossibleXVelocities;
        List<int> mPossibleYVelocities;

        public override string Part1() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week17.txt");
            string[] lSplit = lInput[0].Split('=', ',', '.');
            mTargetX1 = int.Parse(lSplit[1]);
            mTargetX2 = int.Parse(lSplit[3]);
            mTargetY1 = int.Parse(lSplit[5]);
            mTargetY2 = int.Parse(lSplit[7]);
            mPossibleXVelocities = new List<int>();
            for (int i = 0; i < mTargetX2; i++) {
                int lTarget = TriangularNumber(i);
                if (lTarget >= mTargetX1 && lTarget <= mTargetX2) {
                    mPossibleXVelocities.Add(i);
                    break;
                } 
            }
            for (int i = mPossibleXVelocities[0]; i < mTargetX2; i++) mPossibleXVelocities.Add(i+1);
            mPossibleYVelocities = new List<int>();
            int lYMax = 0;
            for (int i = -1000; i < 1000; i++) {
                int lSign = 1;
                if (i != 0) lSign = i / Math.Abs(i);
                int lTop = TriangularNumber(Math.Abs(i)) * lSign;
                for (int y = i+1; y < 1000; y++) {
                    int lFall = TriangularNumber(y);
                    if ((lTop - lFall) >= mTargetY1 && (lTop - lFall)<= mTargetY2) {
                        if (lYMax < i) lYMax = i;
                    }
                }
            }
            for (int i = lYMax; i > mTargetY1 - 1; i--) {
                mPossibleYVelocities.Add(i);
            }
            return $"{TriangularNumber(mPossibleYVelocities.Max())}";
        }

        private int TriangularNumber(int aMax) {
            return aMax * (aMax + 1) / 2;
        }

        public override string Part2() {
            int lHitCount = 0;
            mPossibleXVelocities = mPossibleXVelocities.Distinct().ToList();
            mPossibleYVelocities = mPossibleYVelocities.Distinct().ToList();
            foreach( int x in mPossibleXVelocities) {
                foreach (int y in mPossibleYVelocities) {
                    int xvel = x;
                    int yvel = y;
                    int xHit = 0;
                    int yHit = 0;
                    for (int step = 1; step < 10000; step++) {
                        xHit += xvel;
                        yHit += yvel;
                        if (xvel != 0) xvel--;
                        yvel--;
                        if (xHit <= mTargetX2 && xHit >= mTargetX1 && yHit <= mTargetY2 && yHit >= mTargetY1) {
                            lHitCount++;
                            break;
                        }
                        if (yHit < mTargetY1) break;
                    }
                }
            }
           
            return $"{lHitCount}";
        }
    }
}