using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Functions {
    public class Day01 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_01_1.txt";
        string mValue;
        public override string DoPartA() {
            double lFloor = 0;
            foreach (char c in mValue) {
                if (c == '(') lFloor++;
                else if (c == ')') lFloor--;
                else {
                    throw new Exception();
                }
            }
            return $"{lFloor}";
        }

        public override string DoPartB() {
            double lFloor = 0;
            int lPosition = 1;
            foreach (char c in mValue) {
                if (c == '(') lFloor++;
                else if (c == ')') lFloor--;
                else {
                    throw new Exception();
                }
                if (lFloor < 0) break;
                lPosition++;
            }
            return $"{lPosition}";
        }

        public override void ReadIn() {
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                mValue = lLine;
            }
            return;
        }
    }
}
