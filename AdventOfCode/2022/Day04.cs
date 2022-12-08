using System.IO;

namespace AdventOfCode.Year2022 {
    class Day04 : DayN {
        public override string Part1() {
            string lPath = @"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode2022\Input\Day04.txt";
            int lCountPairs = 0;
            foreach (string lLine in File.ReadAllLines(lPath)) {
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

        public override string Part2() {
            string lPath = @"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode2022\Input\Day04.txt";
            int lOverlap = 0;
            foreach (string lLine in File.ReadAllLines(lPath)) {
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
    }
}
