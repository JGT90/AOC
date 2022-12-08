using System;
using System.Linq;

namespace AdventOfCode.Year2021 {
    internal class Day06 : DayN {
        static double[] mLaternFishPopulation;
        static double[] mTemp;
        static double mTotalLaternFishPopulation;
        static double mStartPopulation;
        public override string Part1() {
            mLaternFishPopulation = new double[9];
            mTemp = new double[9];
            int[] lValues = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week06.txt")[0].Split(',').Select(x => Convert.ToInt32(x)).ToArray();
            foreach (int value in lValues) { mLaternFishPopulation[value]++; mTemp[value]++; mStartPopulation++; }
            //Console.Write("Week06 - PartA: ");
            //Console.WriteLine(LaternFishPopulationAfter(80));
            return LaternFishPopulationAfter(80).ToString();
        }

        public override string Part2() {
            mLaternFishPopulation = mTemp;
            //Console.Write("Week06 - PartB: ");
            //Console.WriteLine(LaternFishPopulationAfter(256));
            return LaternFishPopulationAfter(256).ToString();
        }

        private double LaternFishPopulationAfter(int aDays) {
            for (int i = 0; i < aDays; i++) {
                double lTemp = mLaternFishPopulation[0];
                for (int j = 0; j < 9 - 1; j++) {
                    mLaternFishPopulation[j] = mLaternFishPopulation[j + 1];
                }
                mLaternFishPopulation[8] = lTemp;
                mLaternFishPopulation[6] += lTemp;
                mTotalLaternFishPopulation += lTemp;
            }
            return mTotalLaternFishPopulation + mStartPopulation;
        }
    }
}