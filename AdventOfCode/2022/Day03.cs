using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2022 {
    class Day03 : DayN {
        public override string Part1() {
            string lPath = @"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode2022\Input\Day03.txt";
            double lPrioritySum = 0;
            foreach(string lLine in File.ReadAllLines(lPath)) {
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

        public override string Part2() {
            string lPath = @"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode2022\Input\Day03.txt";
            double lPrioritySum = 0;
            string[] lLines = File.ReadAllLines(lPath);
            for (int i = 0; i < lLines.Length; i++) {
                foreach (char c in lLines[i]) {
                    foreach (char cc in lLines[i+1]) {
                        foreach(char ccc in lLines[i+2]) {
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
    }
}
