using SEGCC;
using System;

namespace AOC2021 {
    internal class Week01 : DayN {
        static int[] mValues;
        public override string Part1() {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week01.txt");
            mValues = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++) {
                mValues[i] = int.Parse(lines[i]);
            }
            int lLarger = 0;
            for (int i = 1; i < mValues.Length; i++) {
                if (mValues[i] > mValues[i - 1]) lLarger++;
            }
            //Console.Write("Week01 - PartA: ");
            //Console.WriteLine(lLarger);
            return lLarger.ToString();
        }

        public override string Part2() {
            int lLarger = 0;
            int lOld = 999999999;
            for (int i = 2; i < mValues.Length; i++) {
                int lNew = 0;
                lNew += mValues[i - 2] + mValues[i - 1] + mValues[i];
                if (lNew > lOld) lLarger++;
                lOld = lNew;
            }
            //Console.Write("Week01 - PartB: ");
            //Console.WriteLine(lLarger);
            return lLarger.ToString();
        }
    }
}
