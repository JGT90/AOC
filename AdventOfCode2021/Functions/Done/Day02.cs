using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day02 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_02_1.txt";
        List<string> mValues;
        const string FORWARD = "forward";
        const string DOWN = "down";
        const string UP = "up";

        public override void ReadIn() {
            mValues = new List<string>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                mValues.Add(lLine);
            }
            Console.WriteLine($"{mValues.Count} values found in file, {mPath}");
            return;
        }


        public override string DoPartA() {
            int lHorizontal = 0;
            int lDepth = 0;
            foreach(string aValue in mValues) {
                if (aValue.Contains(FORWARD)) {
                    lHorizontal += int.Parse(aValue.Remove(0, FORWARD.Length + 1));
                } else if (aValue.Contains(DOWN)) {
                    lDepth += int.Parse(aValue.Remove(0, DOWN.Length + 1));
                } else if (aValue.Contains(UP)) {
                    lDepth -= int.Parse(aValue.Remove(0, UP.Length + 1));
                }
            }
            return $"{lHorizontal * lDepth}";
        }

        public override string DoPartB() {
            int lAim = 0;
            int lHorizontal = 0;
            int lDepth = 0;
            foreach (string aValue in mValues) {
                if (aValue.Contains(FORWARD)) {
                    int lForward = int.Parse(aValue.Remove(0, FORWARD.Length + 1));
                    lHorizontal += lForward;
                    lDepth += lAim * lForward;
                } else if (aValue.Contains(DOWN)) {
                    lAim += int.Parse(aValue.Remove(0, DOWN.Length + 1));
                } else if (aValue.Contains(UP)) {
                    lAim -= int.Parse(aValue.Remove(0, UP.Length + 1));
                }
            }
            return $"{lHorizontal * lDepth}";
        }
    }
}
