using System;
using System.Collections.Generic;

namespace AdventOfCode {
    internal class Day18 : DayN {
        List<Snailfish> mSnailfishes;
        string[] mInput;
        int mIndex = 0;
        public override string Part1() {
            mInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week18.txt");
            mSnailfishes = new List<Snailfish>();
            bool lFirstAdd = true;
            foreach (string line in mInput) {
                mIndex = 0;
                List<Snailfish> lTempFishes = new List<Snailfish>();
                GetSnailfish(lTempFishes, -1, line, 0, true, 0);
                Add(lTempFishes);
                if (!lFirstAdd) {
                    SnailfishAction();
                }
                lFirstAdd = false;
            }
            return $"{GetMagnitude()}";
        }

        public override string Part2() {
            double lLargestMagnitude = 0;
            for (int i = 0; i < mInput.Length; i++) {
                for (int j = 0; j < mInput.Length; j++) {
                    if (i == j) continue;
                    mSnailfishes.Clear();
                    List<Snailfish> lFirstFishes = new List<Snailfish>();
                    GetSnailfish(lFirstFishes, -1, mInput[i], 0, true, 0);
                    List<Snailfish> lSecondFishes = new List<Snailfish>();
                    GetSnailfish(lSecondFishes, -1, mInput[j], 0, true, 0);
                    Add(lFirstFishes);
                    Add(lSecondFishes);
                    SnailfishAction();
                    double lMagnitude = GetMagnitude();
                    if (lMagnitude > lLargestMagnitude) lLargestMagnitude = lMagnitude;
                    mSnailfishes.Clear();
                    lFirstFishes = new List<Snailfish>();
                    GetSnailfish(lFirstFishes, -1, mInput[i], 0, true, 0);
                    lSecondFishes = new List<Snailfish>();
                    GetSnailfish(lSecondFishes, -1, mInput[j], 0, true, 0);
                    Add(lSecondFishes);
                    Add(lFirstFishes);
                    SnailfishAction();
                    lMagnitude = GetMagnitude();
                    if (lMagnitude > lLargestMagnitude) lLargestMagnitude = lMagnitude;
                }
            }
            return $"{lLargestMagnitude}";
        }
        private void SnailfishAction() {
            bool lKeepGoing = true;
            while (lKeepGoing) {
                if (Explode()) continue;
                if (Split()) continue;
                lKeepGoing = false;
            }
        }

        private double GetMagnitude() {
            List<Snailfish> lFishes = new List<Snailfish>();
            for (int i = 0; i < mSnailfishes.Count; i++) {
                if (mSnailfishes[i].X != -1 && mSnailfishes[i].Y != -1) {
                    Snailfish lTemp = new Snailfish() { Level = mSnailfishes[i].Level - 1, Magnitude = mSnailfishes[i].X * 3 + mSnailfishes[i].Y * 2 };
                    lFishes.Add(lTemp);
                } else {
                    lFishes.Add(mSnailfishes[i]);
                }
            }
            while (lFishes.Count > 1) {
                for (int i = 0; i < lFishes.Count - 1; i++) {
                    if (lFishes[i].Level == lFishes[i].Level) {
                        lFishes[i].Level--;
                        lFishes[i].Magnitude = 3 * lFishes[i].Magnitude + 2 * lFishes[i + 1].Magnitude;
                        lFishes.RemoveAt(i + 1);
                    }
                }
            }
            return lFishes[0].Magnitude;
        }

        private int GetSnailfish(List<Snailfish> aFishes, int aIndex, string aLine, int aLevel, bool aLeft, int aFishIndex) {
            Snailfish lFish = new Snailfish();
            bool lInserted = false;
            while (aIndex < aLine.Length - 1) {
                aIndex++;
                if (aLine[aIndex] == '[') {
                    aIndex = GetSnailfish(aFishes, aIndex, aLine, aLevel + 1, true, ++aFishIndex);
                } else if (aLine[aIndex] == ']') {
                    return aIndex;
                } else if (aLine[aIndex] == ',') {
                    aLeft = !aLeft;
                } else {
                    if (!lInserted) {
                        lFish.Level = aLevel;
                        aFishes.Add(lFish);
                        lInserted = true;
                    }
                    if (aLeft) lFish.X = int.Parse(new string(aLine[aIndex], 1));
                    else lFish.Y = int.Parse(new string(aLine[aIndex], 1));
                }
            }
            return -5;
        }

        private void Add(List<Snailfish> aAdded) {
            int mSnailfishCount = mSnailfishes.Count;
            mSnailfishes.AddRange(aAdded);
            if (mSnailfishCount > 0) {
                for (int i = 0; i < mSnailfishes.Count; i++) {
                    mSnailfishes[i].Level++;
                }
            }
        }

        private bool Explode() {
            for (int i = 0; i < mSnailfishes.Count; i++) {
                if (mSnailfishes[i].Level > 4) {
                    if (mSnailfishes[i].X != -1 && mSnailfishes[i].Y != -1) {
                        if (i == 0) { // Left is nothing
                            if (mSnailfishes[i + 1].X == -1) {
                                mSnailfishes[i + 1].X = 0;
                                mSnailfishes[i + 1].Y = mSnailfishes[i + 1].Y + mSnailfishes[i].Y;
                            } else {
                                mSnailfishes[i + 1].X = mSnailfishes[i + 1].X + mSnailfishes[i].Y;
                                mSnailfishes.Insert(1, new Snailfish() { X = 0, Level = mSnailfishes[i].Level - 1 });
                            }
                        } else if (i == mSnailfishes.Count - 1) { // Right is nothing
                            mSnailfishes[i - 1].Y = 0;
                            mSnailfishes[i - 1].X = mSnailfishes[i - 1].X + mSnailfishes[i].X;
                        } else {
                            if (mSnailfishes[i + 1].X != -1) mSnailfishes[i + 1].X = mSnailfishes[i + 1].X + mSnailfishes[i].Y;
                            else if (mSnailfishes[i + 1].Y != -1) mSnailfishes[i + 1].Y = mSnailfishes[i + 1].Y + mSnailfishes[i].Y;
                            if (mSnailfishes[i - 1].Y != -1) mSnailfishes[i - 1].Y = mSnailfishes[i - 1].Y + mSnailfishes[i].X;
                            else if (mSnailfishes[i - 1].X != -1) mSnailfishes[i - 1].X = mSnailfishes[i - 1].X + mSnailfishes[i].X;
                            bool lInsert = true;
                            if (Math.Abs(mSnailfishes[i + 1].Level - mSnailfishes[i].Level) == 1) {
                                if (mSnailfishes[i + 1].X == -1) {
                                    mSnailfishes[i + 1].X = 0;
                                    lInsert = false;
                                }
                            }
                            if (Math.Abs(mSnailfishes[i - 1].Level - mSnailfishes[i].Level) == 1) {
                                if (mSnailfishes[i - 1].Y == -1) {
                                    mSnailfishes[i - 1].Y = 0;
                                    lInsert = false;
                                }
                            }
                            if (lInsert) mSnailfishes.Insert(i + 1, new Snailfish() { X = 0, Level = mSnailfishes[i].Level - 1 });
                        }
                        mSnailfishes.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false; ;
        }

        private bool Split() {
            for (int i = 0; i < mSnailfishes.Count; i++) {
                if (mSnailfishes[i].X > 9) {
                    Snailfish lFish = new Snailfish();
                    lFish.Level = mSnailfishes[i].Level + 1;
                    lFish.X = mSnailfishes[i].X / 2;
                    if (lFish.X * 2 < mSnailfishes[i].X) lFish.Y = lFish.X + 1;
                    else lFish.Y = lFish.X;
                    if (mSnailfishes[i].Y == -1) mSnailfishes[i] = lFish;
                    else {
                        mSnailfishes[i].X = -1;
                        mSnailfishes.Insert(i, lFish);
                    }
                    return true;
                } else if (mSnailfishes[i].Y > 9) {
                    Snailfish lFish = new Snailfish();
                    lFish.Level = mSnailfishes[i].Level + 1;
                    lFish.X = mSnailfishes[i].Y / 2;
                    if (lFish.X * 2 < mSnailfishes[i].Y) lFish.Y = lFish.X + 1;
                    else lFish.Y = lFish.X;
                    if (mSnailfishes[i].X == -1) mSnailfishes[i] = lFish;
                    else {
                        mSnailfishes[i].Y = -1;
                        mSnailfishes.Insert(i + 1, lFish);
                    }
                    return true;
                }
            }
            return false;
        }

        private class Snailfish {
            public Snailfish() {
                X = -1;
                Y = -1;
            }
            public int X { get; set; }
            public int Y { get; set; }
            public int Level { get; set; }
            public double Magnitude { get; set; }
        }
    }
}