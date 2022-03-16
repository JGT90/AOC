using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Functions {
    public class Day08 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_08_1.txt";
        List<string> mStrings;
        
        public override string DoPartA() {
            double lCodeCharactersCount = 0;
            double lStringCharactersCount = 0;
            foreach (string line in mStrings) {
                lCodeCharactersCount += line.Length +2;
                lStringCharactersCount += line.Length;
                int lSubtract = 0;
                for (int i = 0; i < line.Length-1; i++) {
                    if (line[i] == '\u005c') {
                        if (line[i + 1] == '"') {
                            lSubtract++;
                            i++;
                        } else if (line[i + 1] == 'x') {
                            lSubtract += 3;
                            i++;
                        } else if (line[i + 1] == '\u005c') {
                            lSubtract++;
                            i++;
                        }
                    }
                }
                lStringCharactersCount -= lSubtract;
            }
            return $"{lCodeCharactersCount - lStringCharactersCount}";
        }

        public override string DoPartB() {
            double lCodeCharactersCount = 0;
            double lStringCharactersCount = 0;
            foreach (string line in mStrings) {
                lCodeCharactersCount += line.Length + 2;
                lStringCharactersCount += line.Length + 2;
                int lAdd = 4;
                for (int i = 0; i < line.Length - 1; i++) {
                    if (line[i] == '\u005c') {
                        if (line[i + 1] == '"') {
                            lAdd += 2;
                            i++;
                        } else if (line[i + 1] == 'x') {
                            lAdd++;
                            i++;
                        } else if (line[i + 1] == '\u005c') {
                            lAdd += 2;
                            i++;
                        }
                    }
                }
                lStringCharactersCount += lAdd;
            }
            return $"{lStringCharactersCount - lCodeCharactersCount}";
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
