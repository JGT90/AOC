using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2022 {
    class Day02 : DayN_2022 {
        #region Constructor
        public Day02() {
            AddInputData(@"2022/Day02-JGT90.txt");
        }
        #endregion

        #region Properties
        protected override string PuzzleName => "Rock Paper Scissors";
        #endregion

        #region Functions
        public override string SolvePartOne() {
            Dictionary<string, int> lConverter = new Dictionary<string, int>();
            lConverter.Add("A", 1);
            lConverter.Add("X", 1);
            lConverter.Add("B", 2);
            lConverter.Add("Y", 2);
            lConverter.Add("C", 3);
            lConverter.Add("Z", 3);
            double lTotalScore = 0;
            foreach (string lLine in RawData) {
                string[] lSplit = lLine.Split(' ');
                int lPlayerA = lConverter[lSplit[0]];
                int lPlayerB = lConverter[lSplit[1]];
                if (lPlayerA == lPlayerB) {
                    lTotalScore += 3 + lPlayerB;
                } else if (lPlayerA == 1 && lPlayerB == 3) {
                    lTotalScore += 0 + lPlayerB;
                } else if (lPlayerA == 1 && lPlayerB == 2) {
                    lTotalScore += 6 + lPlayerB;
                } else if (lPlayerA == 2 && lPlayerB == 3) {
                    lTotalScore += 6 + lPlayerB;
                } else if (lPlayerA == 2 && lPlayerB == 1) {
                    lTotalScore += 0 + lPlayerB;
                } else if (lPlayerA == 3 && lPlayerB == 1) {
                    lTotalScore += 6 + lPlayerB;
                } else if (lPlayerA == 3 && lPlayerB == 2) {
                    lTotalScore += 0 + lPlayerB;
                } else {
                    throw new System.Exception();
                }
            }
            return lTotalScore.ToString();
        }

        public override string SolvePartTwo() {
            Dictionary<string, int> lConverter = new Dictionary<string, int>();
            lConverter.Add("A", 1);
            lConverter.Add("X", 1);
            lConverter.Add("B", 2);
            lConverter.Add("Y", 2);
            lConverter.Add("C", 3);
            lConverter.Add("Z", 3);
            double lTotalScore = 0;
            foreach (string lLine in RawData) {
                string[] lSplit = lLine.Split(' ');
                int lPlayerA = lConverter[lSplit[0]];
                int lPlayerB = lConverter[lSplit[1]];
                if (lPlayerB == 1) {
                    switch(lPlayerA) {
                        case 1:
                            lTotalScore += 0 + 3;
                            break;
                        case 2:
                            lTotalScore += 0 + 1;
                            break;
                        case 3:
                            lTotalScore += 0 + 2;
                            break;
                    }
                } else if (lPlayerB == 2) {
                    lTotalScore += 3 + lPlayerA;
                } else if (lPlayerB == 3) {
                    switch (lPlayerA) {
                        case 1:
                            lTotalScore += 6 + 2;
                            break;
                        case 2:
                            lTotalScore += 6 + 3;
                            break;
                        case 3:
                            lTotalScore += 6 + 1;
                            break;
                    }
                }
            }
            return lTotalScore.ToString();
        }
        #endregion
    }
}
