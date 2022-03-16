using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day01 : AdventOfCode.AdventOfCode  {
        const string mPath = @"..\..\InputFiles\Input12_01_1.txt";
        List<int> mNumbers;
        public override void ReadIn() {
            mNumbers = new List<int>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                mNumbers.Add(int.Parse(lLine));
            }
            Console.WriteLine($"{mNumbers.Count} values found in file, {mPath}");
            return;
        }

        public override string DoPartA() {
            int lIncreased = 0;
            for (int i = 1; i < mNumbers.Count; i++) {
                if (mNumbers[i] > mNumbers[i - 1]) {
                    lIncreased++;
                }
            }
            return $"{lIncreased}";
        }

        public override string DoPartB() {
            int lIncreased = 0;
            for (int i = 1; i < mNumbers.Count - 2; i++) {
                int lPreviousSum = mNumbers[i - 1] + mNumbers[i] + mNumbers[i + 1];
                int lSum = mNumbers[i] + mNumbers[i + 1] + mNumbers[i + 2];
                if (lSum > lPreviousSum) {
                    lIncreased++;
                }
            }
            return $"{lIncreased}";
        }
    }
}
