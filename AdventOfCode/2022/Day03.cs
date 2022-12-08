using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2022 {
    class Day03 : DayN_2022 {
        #region Constructor
        public Day03() {
            AddInputData(@"2022/Day03-JGT90.txt");
        }
        #endregion

        #region Properties
        protected override string PuzzleName => "Rucksack Reorganization";
        #endregion

        #region Functions
        public override string SolvePartOne() {
            double lPrioritySum = 0;
            foreach(string lLine in RawData) {
                string lFirstCompartment = lLine.Substring(0, lLine.Length / 2);
                string lSecondCompartment = lLine.Substring(lLine.Length / 2);
                foreach (char c in lSecondCompartment) {
                    int i = lFirstCompartment.IndexOf(c);
                    if (i == -1) continue;
                    else {
                        int lScore = (int)lFirstCompartment[i] - 96;
                        if (lScore < 0) {
                            lScore += 58;
                        }
                        lPrioritySum += lScore;
                        goto BLUBB;
                    }
                }
            BLUBB:
                int a = 0;
            }
            return lPrioritySum.ToString();
        }

        public override string SolvePartTwo() {
            double lPrioritySum = 0;
            for (int i = 0; i < RawData.Length; i++) {
                foreach (char c in RawData[i]) {
                    foreach (char cc in RawData[i+1]) {
                        foreach(char ccc in RawData[i+2]) {
                            if (c == cc && cc == ccc) {
                                int lScore = (int)c - 96;
                                if (lScore < 0) {
                                    lScore += 58;
                                }
                                lPrioritySum += lScore;
                                goto BLUBB;
                            }
                        }
                    }
                }
            BLUBB:
                i += 2;
            }
            return lPrioritySum.ToString();
        }
        #endregion
    }
}
