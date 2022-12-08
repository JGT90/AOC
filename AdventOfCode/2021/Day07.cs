using SEGCC;
using System;
using System.Linq;

namespace AOC2021 {
    internal class Day07 : DayN {
        static int[] mValues;
        static int mMinValue;
        static int mMaxValue;
        static int mLowestConsumption = int.MaxValue;
        public override string Part1() {
            mValues = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week07.txt")[0].Split(',').Select(x => Convert.ToInt32(x)).ToArray();
            mMinValue = mValues.Min();
            mMaxValue = mValues.Max();
            for (int i = mMinValue; i < mMaxValue; i++) {
                int lConsumption = 0;
                foreach (int value in mValues) {
                    lConsumption += Math.Abs(value - i);
                }
                if (mLowestConsumption > lConsumption) {
                    mLowestConsumption = lConsumption;
                }
            }
            //Console.Write("Week07 - PartA: ");
            //Console.WriteLine(mLowestConsumption);
            return mLowestConsumption.ToString();
        }

        public override string Part2() {
            mLowestConsumption = int.MaxValue;
            for (int i = mMinValue; i < mMaxValue; i++) {
                int lConsumption = 0;
                foreach (int value in mValues) {
                    int lDifference = Math.Abs(value - i);
                    lConsumption += lDifference * (lDifference + 1) / 2;
                }
                if (mLowestConsumption > lConsumption) {
                    mLowestConsumption = lConsumption;
                }
            }
            //Console.Write("Week07 - PartB: ");
            //Console.WriteLine(mLowestConsumption);
            return mLowestConsumption.ToString();
        }
    }
}