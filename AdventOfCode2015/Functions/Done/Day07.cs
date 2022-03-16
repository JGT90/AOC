using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Functions {
    public class Day07 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_07_1.txt";
        List<string> mInstructions;
        Dictionary<string, int> mValues = new Dictionary<string, int>();
        int mResultA;
        public override string DoPartA() {
            bool[] lInstructionDone = new bool[mInstructions.Count];
            for (int i = 0; i < mInstructions.Count; i++) {
                if (mInstructions[i].Contains("RSHIFT")) continue;
                if (mInstructions[i].Contains("LSHIFT")) continue;
                if (mInstructions[i].Contains("OR")) continue;
                if (mInstructions[i].Contains("AND")) continue;
                if (mInstructions[i].Contains("NOT")) continue;
                string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "->" }, StringSplitOptions.None), p => p.Trim());
                if (int.TryParse(split[0], out int lInitValue)) {
                    mValues.Add(split[1], int.Parse(split[0]));
                    lInstructionDone[i] = true;
                }
            }
            bool lAllDone = false;
            do {
                for (int i = 0; i < mInstructions.Count; i++) {
                    if (lInstructionDone[i]) continue;
                    if (mInstructions[i].Contains("RSHIFT")) {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "RSHIFT", "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0])) {
                            mValues.Add(split[2], RightShift(mValues[split[0]], int.Parse(split[1])));
                            lInstructionDone[i] = true;
                        } else if (int.TryParse(split[0], out int lValue)) {
                            mValues.Add(split[2], RightShift(lValue, int.Parse(split[1])));
                            lInstructionDone[i] = true;
                        }
                    } else if (mInstructions[i].Contains("LSHIFT")) {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "LSHIFT", "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0])) {
                            mValues.Add(split[2], LeftShift(mValues[split[0]], int.Parse(split[1])));
                            lInstructionDone[i] = true;
                        } else if (int.TryParse(split[0], out int lValue)) {
                            mValues.Add(split[2], LeftShift(lValue, int.Parse(split[1])));
                            lInstructionDone[i] = true;
                        }
                    } else if (mInstructions[i].Contains("OR")) {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "OR", "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0]) && mValues.ContainsKey(split[1])) {
                            mValues.Add(split[2], BitWiseOr(mValues[split[0]], mValues[split[1]]));
                            lInstructionDone[i] = true;
                        } else if (mValues.ContainsKey(split[0]) && int.TryParse(split[1], out int lValue)) {
                            mValues.Add(split[2], BitWiseOr(mValues[split[0]], lValue));
                            lInstructionDone[i] = true;
                        } else if (int.TryParse(split[0], out lValue) && mValues.ContainsKey(split[1])) {
                            mValues.Add(split[2], BitWiseOr(lValue, mValues[split[1]]));
                            lInstructionDone[i] = true;
                        }
                    } else if (mInstructions[i].Contains("AND")) {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "AND", "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0]) && mValues.ContainsKey(split[1])) {
                            mValues.Add(split[2], BitWiseAnd(mValues[split[0]], mValues[split[1]]));
                            lInstructionDone[i] = true;
                        } else if (mValues.ContainsKey(split[0]) && int.TryParse(split[1], out int lValue)) {
                            mValues.Add(split[2], BitWiseAnd(mValues[split[0]], lValue));
                            lInstructionDone[i] = true;
                        } else if (int.TryParse(split[0], out lValue) && mValues.ContainsKey(split[1])) {
                            mValues.Add(split[2], BitWiseAnd(lValue, mValues[split[1]]));
                            lInstructionDone[i] = true;
                        }
                    } else if (mInstructions[i].Contains("NOT")) {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "NOT", "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0])) {
                            mValues.Add(split[1], Complement(mValues[split[0]]));
                            lInstructionDone[i] = true;
                        } else if (int.TryParse(split[0], out int lValue)) {
                            mValues.Add(split[1], lValue);
                            lInstructionDone[i] = true;
                        }
                    } else {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0])) {
                            mValues.Add(split[1], mValues[split[0]]);
                            lInstructionDone[i] = true;
                        } else if (int.TryParse(split[0], out int lValue)) {
                            mValues.Add(split[1], lValue);
                            lInstructionDone[i] = true;
                        }
                    }
                }

                lAllDone = true;
                for (int i = 0; i < mInstructions.Count; i++) {
                    if (!lInstructionDone[i]) {
                        lAllDone = false;
                        break;
                    }
                }
            } while (!lAllDone);
            mResultA = mValues["a"];
            return $"{mResultA}";
        }

        public override string DoPartB() {
            mValues.Clear();
            bool[] lInstructionDone = new bool[mInstructions.Count];
            for (int i = 0; i < mInstructions.Count; i++) {
                if (mInstructions[i].Contains("RSHIFT")) continue;
                if (mInstructions[i].Contains("LSHIFT")) continue;
                if (mInstructions[i].Contains("OR")) continue;
                if (mInstructions[i].Contains("AND")) continue;
                if (mInstructions[i].Contains("NOT")) continue;
                string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "->" }, StringSplitOptions.None), p => p.Trim());
                if (int.TryParse(split[0], out int lInitValue)) {
                    if (split[1] == "b") {
                        mValues.Add(split[1], mResultA);
                        lInstructionDone[i] = true;
                    } else {
                        mValues.Add(split[1], int.Parse(split[0]));
                        lInstructionDone[i] = true;
                    }
                }
            }
            bool lAllDone = false;
            do {
                for (int i = 0; i < mInstructions.Count; i++) {
                    if (lInstructionDone[i]) continue;
                    if (mInstructions[i].Contains("RSHIFT")) {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "RSHIFT", "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0])) {
                            if (split[2] == "b") {
                                mValues.Add(split[2], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[2], RightShift(mValues[split[0]], int.Parse(split[1])));
                                lInstructionDone[i] = true;
                            }
                        } else if (int.TryParse(split[0], out int lValue)) {
                            if (split[2] == "b") {
                                mValues.Add(split[2], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[2], RightShift(lValue, int.Parse(split[1])));
                                lInstructionDone[i] = true;
                            }
                        }
                    } else if (mInstructions[i].Contains("LSHIFT")) {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "LSHIFT", "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0])) {
                            if (split[2] == "b") {
                                mValues.Add(split[2], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[2], LeftShift(mValues[split[0]], int.Parse(split[1])));
                                lInstructionDone[i] = true;
                            }
                        } else if (int.TryParse(split[0], out int lValue)) {
                            if (split[2] == "b") {
                                mValues.Add(split[2], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[2], LeftShift(lValue, int.Parse(split[1])));
                                lInstructionDone[i] = true;
                            }
                        }
                    } else if (mInstructions[i].Contains("OR")) {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "OR", "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0]) && mValues.ContainsKey(split[1])) {
                            if (split[2] == "b") {
                                mValues.Add(split[2], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[2], BitWiseOr(mValues[split[0]], mValues[split[1]]));
                                lInstructionDone[i] = true;
                            }
                        } else if (mValues.ContainsKey(split[0]) && int.TryParse(split[1], out int lValue)) {
                            if (split[2] == "b") {
                                mValues.Add(split[2], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[2], BitWiseOr(mValues[split[0]], lValue));
                                lInstructionDone[i] = true;
                            }
                        } else if (int.TryParse(split[0], out lValue) && mValues.ContainsKey(split[1])) {
                            if (split[2] == "b") {
                                mValues.Add(split[2], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[2], BitWiseOr(lValue, mValues[split[1]]));
                                lInstructionDone[i] = true;
                            }
                        }
                    } else if (mInstructions[i].Contains("AND")) {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "AND", "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0]) && mValues.ContainsKey(split[1])) {
                            if (split[2] == "b") {
                                mValues.Add(split[2], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[2], BitWiseAnd(mValues[split[0]], mValues[split[1]]));
                                lInstructionDone[i] = true;
                            }
                        } else if (mValues.ContainsKey(split[0]) && int.TryParse(split[1], out int lValue)) {
                            if (split[2] == "b") {
                                mValues.Add(split[2], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[2], BitWiseAnd(mValues[split[0]], lValue));
                                lInstructionDone[i] = true;
                            }
                        } else if (int.TryParse(split[0], out lValue) && mValues.ContainsKey(split[1])) {
                            if (split[2] == "b") {
                                mValues.Add(split[2], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[2], BitWiseAnd(lValue, mValues[split[1]]));
                                lInstructionDone[i] = true;
                            }
                        }
                    } else if (mInstructions[i].Contains("NOT")) {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "NOT", "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0])) {
                            if (split[1] == "b") {
                                mValues.Add(split[1], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[1], Complement(mValues[split[0]]));
                                lInstructionDone[i] = true;
                            }
                        } else if (int.TryParse(split[0], out int lValue)) {
                            if (split[1] == "b") {
                                mValues.Add(split[1], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[1], lValue);
                                lInstructionDone[i] = true;
                            }
                        }
                    } else {
                        string[] split = Array.ConvertAll(mInstructions[i].Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
                        if (mValues.ContainsKey(split[0])) {
                            if (split[1] == "b") {
                                mValues.Add(split[1], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[1], mValues[split[0]]);
                                lInstructionDone[i] = true;
                            }
                        } else if (int.TryParse(split[0], out int lValue)) {
                            if (split[1] == "b") {
                                mValues.Add(split[1], mResultA);
                                lInstructionDone[i] = true;
                            } else {
                                mValues.Add(split[1], lValue);
                                lInstructionDone[i] = true;
                            }
                        }
                    }
                }

                lAllDone = true;
                for (int i = 0; i < mInstructions.Count; i++) {
                    if (!lInstructionDone[i]) {
                        lAllDone = false;
                        break;
                    }
                }
            } while (!lAllDone);

            return $"{mValues["a"]}";
        }

        public override void ReadIn() {
            mInstructions = new List<string>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                mInstructions.Add(lLine);
            }
            return;
        }

        private int BitWiseAnd(int aValueA, int aValueB) {
            return aValueA & aValueB;
        }
        private int BitWiseOr(int aValueA, int aValueB) {
            return aValueA | aValueB;
        }
        private int LeftShift(int aValue, int aBit) {
            return aValue << aBit;
        }
        private int RightShift(int aValue, int aBit) {
            return aValue >> aBit;
        }
        private int Complement(int aValue) {
            return ~aValue;
        }
    }
}
