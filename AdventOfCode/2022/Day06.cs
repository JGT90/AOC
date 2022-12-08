using System.IO;

namespace AdventOfCode.Year2022 {
    class Day06 : DayN_2022 {
        #region Constructor
        public Day06() {
            AddInputData(@"2022/Day06-JGT90.txt");
        }
        #endregion

        #region Properties
        protected override string PuzzleName => "Tuning Trouble";
        #endregion

        #region Functions
        public override string SolvePartOne() {
            int lLength = 3;
            foreach (string lLine in RawData) {
                for (int i = lLength; i < lLine.Length; i++) {
                    for (int k = 0; k <= lLength; k++) {
                        for (int l = 0; l <= lLength; l++) {
                            if (lLine[i - k] == lLine[i - l] && l != k) {
                                goto BLUBB;
                            }
                        }
                    }
                    return (i + 1).ToString();
                BLUBB:
                    int a = 0;
                }
            }
            return "-1";
        }

        public override string SolvePartTwo() {
            int lLength = 13;
            foreach (string lLine in RawData) {
                for (int i = lLength; i < lLine.Length; i++) {
                    for (int k = 0; k <= lLength; k++) {
                        for (int l = 0; l <= lLength; l++) {
                            if (lLine[i - k] == lLine[i - l] && l != k) {
                                goto BLUBB;
                            }
                        }
                    }
                    return (i + 1).ToString();
                BLUBB:
                    int a = 0;
                }
            }
            return "-1";
        }
        #endregion
    }
}
