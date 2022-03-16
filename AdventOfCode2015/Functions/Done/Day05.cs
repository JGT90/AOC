using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Functions {
    public class Day05 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_05_1.txt";
        List<string> mStrings;
        public override string DoPartA() {
            double lNiceStrings = 0;
            foreach(string line in mStrings) {
                char lLastChar = ' ';
                int lVowelsCount = 0;
                bool lTwiceInRow = false;
                int lLength = line.Length;
                for(int i = 0; i < lLength; i++) {
                    if (lLastChar == 'a' && line[i] == 'b' || lLastChar == 'c' && line[i] == 'd' || lLastChar == 'p' && line[i] == 'q' || lLastChar == 'x' && line[i] == 'y') break;
                    if (line[i]== 'a' || line[i]== 'e' || line[i]== 'i' || line[i]== 'o' || line[i]== 'u') lVowelsCount++;
                    if (lLastChar == line[i]) lTwiceInRow = true;
                    if (lVowelsCount > 2 && lTwiceInRow && i == lLength - 1) {
                        lNiceStrings++;
                        break;
                    }
                    lLastChar = line[i];
                }
            }
            return $"{lNiceStrings}";
        }

        public override string DoPartB() {
            double lNiceStrings = 0;
            foreach (string line in mStrings) {
                char lLastChar = ' ';
                char lPenultimateChar = ' ';
                bool lInBetween = false;
                int lOccurence = 0;
                int lLength = line.Length;
                for (int i = 0; i < lLength; i++) {
                    int index = line.IndexOf($"{lLastChar}{line[i]}", i+1);
                    if (index >= 0) lOccurence++; 
                    if (lPenultimateChar == line[i]) lInBetween = true;
                    if (lOccurence > 0 && lInBetween) {
                        lNiceStrings++;
                        break;
                    }
                    lPenultimateChar = lLastChar;
                    lLastChar = line[i];
                }
            }
            return $"{lNiceStrings}";
        }

        public override void ReadIn() {
            mStrings = new List<string>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                mStrings.Add(lLine);
            }
            return;
        }
    }
}
