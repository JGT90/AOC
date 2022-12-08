using System.IO;

namespace AdventOfCode2022 {
    class Day06 {
        public string DoPartA() {
            string lPath = @"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode2022\Input\Day06.txt";
            int lLength = 3;
            foreach (string lLine in File.ReadAllLines(lPath)) {
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

        public string DoPartB() {
            string lPath = @"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode2022\Input\Day06.txt";
            int lLength = 13;
            foreach (string lLine in File.ReadAllLines(lPath)) {
                for (int i = lLength; i < lLine.Length; i++) {
                    for (int k = 0; k <= lLength; k++) {
                        for (int l = 0; l <= lLength; l++) {
                            if (lLine[i-k] == lLine[i-l] && l != k) {
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
    }
}
