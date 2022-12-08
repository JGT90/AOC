using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2022 {
    class Day01 {
        int[] mElfes;
        public string DoPartA() {
            string lPath = @"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode2022\Input\Day01.txt";
            List<int> lElfes = new List<int>();
            lElfes.Add(0);
            foreach (string lLine in File.ReadAllLines(lPath)) {
                if (string.IsNullOrEmpty(lLine)) lElfes.Add(0);
                else lElfes[lElfes.Count - 1] += int.Parse(lLine);
            }
            mElfes = lElfes.ToArray();
            return lElfes.Max().ToString();
        }

        public string DoPartB() {
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
    }
}
