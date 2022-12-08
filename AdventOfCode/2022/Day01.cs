using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2022 {
    class Day01 : DayN_2022 {
        #region Fields
        private int[] mElfes;
        #endregion

        #region Constructor
        public Day01() {
            AddInputData(@"2022/Day01-JGT90.txt");
        }
        #endregion

        #region Properties
        protected override string PuzzleName => "Calorie Counting";
        public override string SolvePartOne() {
            List<int> lElfes = new List<int>();
            lElfes.Add(0);
            foreach (string lLine in RawData) {
                if (string.IsNullOrEmpty(lLine)) lElfes.Add(0);
                else lElfes[lElfes.Count - 1] += int.Parse(lLine);
            }
            mElfes = lElfes.ToArray();
            return lElfes.Max().ToString();
        }

        public override string SolvePartTwo() {
            int lThirdMax = int.MinValue;
            int lSecondMax = int.MinValue;
            int lFirstMax = int.MinValue;
            foreach (int lElf in mElfes) {
                if (lFirstMax < lElf) {
                    lThirdMax = lSecondMax;
                    lSecondMax = lFirstMax;
                    lFirstMax = lElf;
                } else if (lSecondMax < lElf) {
                    lThirdMax = lSecondMax;
                    lSecondMax = lElf;
                } else if (lThirdMax < lElf) lThirdMax = lElf;
            }
            return (lFirstMax + lSecondMax + lThirdMax).ToString(); ;
        }
        #endregion
    }
}
