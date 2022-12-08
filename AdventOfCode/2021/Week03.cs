using SEGCC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021 {
    internal class Week03 : DayN {
        static string[] mLines;
        static int[,] mValues;
        static int mXSize;
        static int mYSize;

        public override string Part1() {
            mLines = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week03.txt");
            mXSize = mLines[0].Length;
            mYSize = mLines.Length;
            mValues = new int[mYSize, mXSize];
            double lGammaRate = 0;
            double lEpsilonRate = 0;
            for (int i = 0; i < mYSize; i++) {
                for (int j = 0; j < mXSize; j++) {
                    mValues[i, j] = Convert.ToInt32(mLines[i][j]) - 48;
                }
            }
            for (int j = 0; j < mXSize; j++) {
                int lMostCommon = 0;
                for (int i = 0; i < mYSize; i++) {
                    if (mValues[i, j] == 0) lMostCommon--;
                    if (mValues[i, j] == 1) lMostCommon++;
                }
                if (lMostCommon > 0) lGammaRate += Math.Pow(2, mXSize - 1 - j);
                if (lMostCommon < 0) lEpsilonRate += Math.Pow(2, mXSize - 1 - j);
            }
            //Console.Write("Week03 - PartA: ");
            //Console.WriteLine(lGammaRate * lEpsilonRate);
            return (lGammaRate * lEpsilonRate).ToString();
        }

        public override string Part2() {
            List<string> lLines = mLines.ToList();
            string lOxygenString = string.Empty;
            for (int j = 0; j < mXSize; j++) {
                int lMostCommon = 0;
                for (int i = 0; i < lLines.Count; i++) {
                    if (lLines[i][j] == '0') lMostCommon--;
                    if (lLines[i][j] == '1') lMostCommon++;
                }
                if (lMostCommon > 0) lOxygenString += "1";
                else if (lMostCommon < 0) lOxygenString += "0";
                else lOxygenString += "1";

                lLines = lLines.Where(x => x.StartsWith(lOxygenString)).ToList();
                if (lLines.Count == 1) {
                    lOxygenString = lLines[0].ToString();
                    break;
                }
            }

            lLines = mLines.ToList();
            string lCO2ScrubberString = string.Empty;
            for (int j = 0; j < mXSize; j++) {
                int lMostCommon = 0;
                for (int i = 0; i < lLines.Count; i++) {
                    if (lLines[i][j] == '0') lMostCommon--;
                    if (lLines[i][j] == '1') lMostCommon++;
                }
                if (lMostCommon > 0) lCO2ScrubberString += "0";
                else if (lMostCommon < 0) lCO2ScrubberString += "1";
                else lCO2ScrubberString += "0";

                lLines = lLines.Where(x => x.StartsWith(lCO2ScrubberString)).ToList();
                if (lLines.Count == 1) {
                    lCO2ScrubberString = lLines[0].ToString();
                    break;
                }
            }
            double lOxygenRate = 0;
            double lCO2Rate = 0;
            for (int j = 0; j < mXSize; j++) {
                if (lOxygenString[j] == '1') lOxygenRate += Math.Pow(2, mXSize - 1 - j);
                if (lCO2ScrubberString[j] == '1') lCO2Rate += Math.Pow(2, mXSize - 1 - j);
            }
            //Console.Write("Week03 - PartB: ");
            //Console.WriteLine(lOxygenRate * lCO2Rate);
            return (lOxygenRate * lCO2Rate).ToString();
        }
    }
}
