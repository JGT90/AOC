using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day10 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_10_1.txt";
        List<string> mValues;
        List<string> mCorruptedValues;
        public override void ReadIn() {

            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            mValues = new List<string>();
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                if (lLine == string.Empty) {
                    break;
                }
                mValues.Add(lLine);
            }
            Console.WriteLine($"{mValues.Count} values found in file, {mPath}");
            return;
        }

        public override string DoPartA() {
            double lResult = 0.0;
            mCorruptedValues = new List<string>();
            foreach (string lLine in mValues) {
                mCorruptedValues.Add(lLine);
                bool lExitLoop = false;
                string lString = string.Empty;
                foreach (char lChar in lLine) {
                    switch (lChar) {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            lString += lChar;
                            break;
                        case ')':
                            if (lString.Last() == '(') {
                                lString = lString.Remove(lString.Length - 1, 1);
                            } else {
                                lResult += 3;
                                lExitLoop = true;
                                mCorruptedValues.RemoveAt(mCorruptedValues.Count - 1);
                            }
                            break;
                        case ']':
                            if (lString.Last() == '[') {
                                lString = lString.Remove(lString.Length - 1, 1);
                            } else {
                                lResult += 57;
                                lExitLoop = true;
                                mCorruptedValues.RemoveAt(mCorruptedValues.Count - 1);
                            }
                            break;
                        case '}':
                            if (lString.Last() == '{') {
                                lString = lString.Remove(lString.Length - 1, 1);
                            } else {
                                lResult += 1197;
                                lExitLoop = true;
                                mCorruptedValues.RemoveAt(mCorruptedValues.Count - 1);
                            }
                            break;
                        case '>':
                            if (lString.Last() == '<') {
                                lString = lString.Remove(lString.Length - 1, 1);
                            } else {
                                lResult += 25137;
                                lExitLoop = true;
                                mCorruptedValues.RemoveAt(mCorruptedValues.Count - 1);
                            }
                            break;
                    }
                    if (lExitLoop) {
                        break;
                    }
                }

            }
            return $"{lResult}";
        }

        public override string DoPartB() {
            List<double> lResult = new List<double>();
            foreach (string lLine in mCorruptedValues) {
                double lScore = 0.0;
                //bool lExitLoop = false;
                string lString = string.Empty;
                string lCompletionString = string.Empty;
                int lCount = 0;
                foreach (char lChar in lLine) {
                    lCount++;
                    switch (lChar) {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            lString += lChar;
                            break;
                        case ')':
                            if (lString.Last() == '(') {
                                lString = lString.Remove(lString.Length - 1, 1);
                            } else {
                                //lResult += 3;
                                //lExitLoop = true;
                                //aNewValues.RemoveAt(aNewValues.Count - 1);
                            }
                            break;
                        case ']':
                            if (lString.Last() == '[') {
                                lString = lString.Remove(lString.Length - 1, 1);
                            } else {
                                //lResult += 57;
                                //lExitLoop = true;
                                //aNewValues.RemoveAt(aNewValues.Count - 1);
                            }
                            break;
                        case '}':
                            if (lString.Last() == '{') {
                                lString = lString.Remove(lString.Length - 1, 1);
                            } else {
                                //lResult += 1197;
                                //lExitLoop = true;
                                //aNewValues.RemoveAt(aNewValues.Count - 1);
                            }
                            break;
                        case '>':
                            if (lString.Last() == '<') {
                                lString = lString.Remove(lString.Length - 1, 1);
                            } else {
                                //lResult += 25137;
                                //lExitLoop = true;
                                //aNewValues.RemoveAt(aNewValues.Count - 1);
                            }
                            break;
                    }
                    //if (lExitLoop) {
                    //    break;
                    //}
                    if (lCount == lLine.Length) {
                        int a = 0;
                        for (int i = lString.Length-1; i >= 0; i--) {
                            switch(lString[i]) {
                                case '(':
                                    lScore = 5 * lScore + 1;
                                    break;
                                case '[':
                                    lScore = 5 * lScore + 2;
                                    break;
                                case '{':
                                    lScore = 5 * lScore + 3;
                                    break;
                                case '<':
                                    lScore = 5 * lScore + 4;
                                    break;
                            }
                        }
                        lResult.Add(lScore);
                    }
                }
            }
            List<double> sorted = lResult.OrderByDescending(x => x).ToList();
            return $"{sorted[sorted.Count/2]}";

        }
    }
}
