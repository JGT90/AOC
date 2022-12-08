using SEGCC;

namespace AOC2021 {
    internal class Day16 : DayN {
        Week16Lib_JGT.Week16 mWeek16 = new Week16Lib_JGT.Week16();

        public override string Part1() {
            return $"{mWeek16.Part1()}";
        }

        public override string Part2() {
            return $"{mWeek16.Part2()}";
        }
    }
}