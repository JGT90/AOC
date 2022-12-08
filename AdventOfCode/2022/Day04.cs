using System.IO;

namespace AdventOfCode.Year2022 {
    class Day04 : DayN_2022 {
        #region Constructor
        public Day04() {
            AddInputData(@"2022/Day04-JGT90.txt");
        }
        #endregion

        #region Properties
        protected override string PuzzleName => "Camp Cleanup";
        #endregion

        #region Functions
        public override string SolvePartOne() {
            int lCountPairs = 0;
            foreach (string lLine in RawData) {
                string[] lSplit = lLine.Split(new char[] { ',', '-' });
                int lMinA = int.Parse(lSplit[0]);
                int lMaxA = int.Parse(lSplit[1]);
                int lMinB = int.Parse(lSplit[2]);
                int lMaxB = int.Parse(lSplit[3]);
                if (lMinA <= lMinB && lMaxA >= lMaxB) lCountPairs++;
                else if (lMinB <= lMinA && lMaxB >= lMaxA) lCountPairs++;
            }
            return lCountPairs.ToString();
        }

        public override string SolvePartTwo() {
            int lOverlap = 0;
            foreach (string lLine in RawData) {
                string[] lSplit = lLine.Split(new char[] { ',', '-' });
                int lMinA = int.Parse(lSplit[0]);
                int lMaxA = int.Parse(lSplit[1]);
                int lMinB = int.Parse(lSplit[2]);
                int lMaxB = int.Parse(lSplit[3]);
                for (int i = lMinA; i < lMaxA + 1; i++) {
                    if (i >= lMinB && i <= lMaxB) {
                        lOverlap++;
                        i = int.MaxValue - 1;
                    }
                }
            }
            return lOverlap.ToString();
        }
        #endregion
    }
}
