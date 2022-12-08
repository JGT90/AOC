using System.IO;

namespace AdventOfCode.Year2022 {
    class Day05 : DayN_2022 {
        #region Constructor
        public Day05() {
            AddInputData(@"2022/Day05-JGT90.txt");
        }
        #endregion

        #region Properties
        protected override string PuzzleName => "Supply Stacks";
        #endregion

        #region Methods
        private string[] GetStartingStacks(int EmptyLineIndex) {
            int _stackSize = RawData[EmptyLineIndex - 1].Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries).Length;
            var _stacks = new string[_stackSize];
            for (int i = EmptyLineIndex-2; i >= 0; i--) {
                for (int j = 0; j < _stackSize; j++) {
                    if (RawData[i][j+1+j*3] != ' ') _stacks[j] += RawData[i][j + 1 + j * 3];
                }
            }
            return _stacks;
        }
        #endregion

        #region Functions
        public override string SolvePartOne() {
            string[] lStacks = null;
            int _lineIndex = 0;
            foreach (string lLine in RawData) {
                if (string.IsNullOrEmpty(lLine)) {
                    lStacks = GetStartingStacks(_lineIndex);
                    continue;
                }
                _lineIndex++;
                if (lStacks == null) continue;
                string[] lSplit = lLine.Split(' ');
                int From = int.Parse(lSplit[3])-1;
                int To = int.Parse(lSplit[5])-1;
                int Length = int.Parse(lSplit[1]);
                for (int i = 0; i < Length; i++) {
                    lStacks[To] += lStacks[From][lStacks[From].Length - 1 - i];
                }
                lStacks[From] = lStacks[From].Remove(lStacks[From].Length - Length);
            }
            return $"{lStacks[0][lStacks[0].Length - 1]}{lStacks[1][lStacks[1].Length - 1]}{lStacks[2][lStacks[2].Length - 1]}{lStacks[3][lStacks[3].Length - 1]}{lStacks[4][lStacks[4].Length - 1]}{lStacks[5][lStacks[5].Length - 1]}{lStacks[6][lStacks[6].Length - 1]}{lStacks[7][lStacks[7].Length - 1]}{lStacks[8][lStacks[8].Length - 1]}";
        }

        public override string SolvePartTwo() {
            string[] lStacks = null;
            int _lineIndex = 0;
            foreach (string lLine in RawData) {
                if (string.IsNullOrEmpty(lLine)) {
                    lStacks = GetStartingStacks(_lineIndex);
                    continue;
                }
                _lineIndex++;
                if (lStacks == null) continue;
                string[] lSplit = lLine.Split(' ');
                int From = int.Parse(lSplit[3])-1;
                int To = int.Parse(lSplit[5])-1;
                int Length = int.Parse(lSplit[1]);
                lStacks[To] += lStacks[From].Substring(lStacks[From].Length - Length);
                lStacks[From] = lStacks[From].Remove(lStacks[From].Length - Length);
            }
            return $"{lStacks[0][lStacks[0].Length - 1]}{lStacks[1][lStacks[1].Length - 1]}{lStacks[2][lStacks[2].Length - 1]}{lStacks[3][lStacks[3].Length - 1]}{lStacks[4][lStacks[4].Length - 1]}{lStacks[5][lStacks[5].Length - 1]}{lStacks[6][lStacks[6].Length - 1]}{lStacks[7][lStacks[7].Length - 1]}{lStacks[8][lStacks[8].Length - 1]}";

        }
        #endregion
    }
}
