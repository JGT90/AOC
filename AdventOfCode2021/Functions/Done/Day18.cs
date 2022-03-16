using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day18 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_18_1.txt";
        List<string> mValues;
        public override void ReadIn() {
            mValues = new List<string>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                mValues.Add(lLine);
            }
        }

        private string SnailfishAddition(List<string> aSummands) {
            int index = 1;
            StringBuilder sb = new StringBuilder();
            string sum = aSummands[0];
            sb.Append($"{sum}\n");
            List<Tuple<int, int>> Numbers = new List<Tuple<int, int>>();
            while (index <= aSummands.Count) {
                int OpenBrackets = 0;
                Numbers.Clear();
                int SplitIndex = 0;
                for (int i = 0; i < sum.Length; i++) {
                    if (sum[i] == '[') OpenBrackets++;
                    if (sum[i] == ']') OpenBrackets--;
                    // save numbers;
                    int lNumber = GetNumber(sum, i);
                    if (lNumber != -1) Numbers.Add(new Tuple<int, int>(lNumber, i));
                    if (lNumber != -1 && lNumber.ToString().Length > 1) {
                        if (SplitIndex == 0) SplitIndex = i;
                        i += lNumber.ToString().Length - 1;
                        continue;
                    }
                    if (OpenBrackets > 4) {
                        // explode
                        List<Tuple<int, int>> ExplodeNumbers = Explode(sum, i, out int ExplodeEnd);
                        int offset = 0;
                        if (ExplodeNumbers.Count > 2) {
                            offset = ExplodeNumbers[2].Item1 > 99 ? 3 : ExplodeNumbers[2].Item1 > 9 ? 2 : 1;
                            sum = sum.Remove(ExplodeNumbers.Last().Item2, offset);
                            sum = sum.Insert(ExplodeNumbers.Last().Item2, $"{ExplodeNumbers.Last().Item1 + ExplodeNumbers[1].Item1}");
                        }
                        sum = sum.Remove(i, ExplodeEnd - i + 1);
                        sum = sum.Insert(i, "0");
                        if (Numbers.Count > 0) {
                            // Override value left
                            offset = Numbers.Last().Item1 > 99 ? 3 : Numbers.Last().Item1 > 9 ? 2 : 1;
                            sum = sum.Remove(Numbers.Last().Item2, offset);
                            sum = sum.Insert(Numbers.Last().Item2, $"{Numbers.Last().Item1 + ExplodeNumbers.First().Item1}");
                        }
                        //Console.WriteLine(sum);
                        sb.Append($"{sum}\texplode\n");
                        break;
                    }
                    //if (lNumber > 9 && SplitIndex == 0) {
                    //    // split
                    //    SplitIndex = i;
                    //    //break;
                    //}
                    //if (index >= Summands.Count && i == sum.Length - 1) return sum;
                    if (i == sum.Length - 1) {
                        if (SplitIndex != 0) {
                            lNumber = GetNumber(sum, SplitIndex);
                            sum = sum.Remove(SplitIndex, 2);
                            int left = (int)(lNumber / 2.0);
                            int right = (int)(lNumber / 2.0 + 0.5);
                            sum = sum.Insert(SplitIndex, $"[{left},{right}]");
                            //Console.WriteLine(sum);
                            sb.Append($"{sum}\tsplit\n");
                            break;
                        }
                        if (index >= aSummands.Count) return sum;
                        // add
                        sum = $"[{sum},{aSummands[index]}]";
                        sb.Append($"{sum}\tadd\n");
                        index++;
                        break;
                    }
                }
            }
            return sum;
        }

        private List<Tuple<int, int>> Explode(string aSum, int aExplodeBegin, out int ExplodeEnd) {
            List<Tuple<int, int>> Numbers = new List<Tuple<int, int>>();
            ExplodeEnd = 0;
            for (int i = aExplodeBegin; i < aSum.Length; i++) {
                if (aSum[i] == ']') {
                    ExplodeEnd = i;
                    break;
                } else {
                    int lNumber = GetNumber(aSum, i);
                    if (lNumber != -1) Numbers.Add(new Tuple<int, int>(lNumber, i));
                    if (lNumber != -1 && lNumber.ToString().Length > 1) {
                        i += lNumber.ToString().Length - 1;
                        continue;
                    }
                }
            }
            for (int i = ExplodeEnd; i < aSum.Length; i++) {
                int lNumber = GetNumber(aSum, i);
                if (lNumber != -1) {
                    Numbers.Add(new Tuple<int, int>(lNumber, i));
                    break;
                }
            }
            return Numbers;
        }

        private int GetNumber(string aSum, int aIndex) {
            if (int.TryParse(aSum.Substring(aIndex, 1), out int SingleDigitNumber)) {
                if (int.TryParse(aSum.Substring(aIndex, 2), out int DoubleDigitNumber)) {
                    if (int.TryParse(aSum.Substring(aIndex, 3), out int TripleDigitNumber)) {
                        return TripleDigitNumber;
                    }
                    return DoubleDigitNumber;
                }
                return SingleDigitNumber;
            }
            return -1;
        }
        public override string DoPartA() {
            string lSnailFishSum = SnailfishAddition(mValues);
            return $"{DoWork(lSnailFishSum)}";
        }
        public int DoWork(string aSnailFishSum) {
            int OpenBrackets = 0;
            List<Tuple<int, int>> Numbers = new List<Tuple<int, int>>();
            Numbers.Clear();
            int SplitIndex = 0;
            for (int i = 0; i < aSnailFishSum.Length; i++) {
                if (aSnailFishSum[i] == '[') OpenBrackets++;
                if (aSnailFishSum[i] == ']') OpenBrackets--;
                // save numbers;
                int lNumber = GetNumber(aSnailFishSum, i);
                if (lNumber != -1) Numbers.Add(new Tuple<int, int>(lNumber, OpenBrackets));
                if (lNumber != -1 && lNumber.ToString().Length > 1) {
                    if (SplitIndex == 0) SplitIndex = i;
                    i += lNumber.ToString().Length - 1;
                    continue;
                }
            }
            while (Numbers.Count > 1) {
                int max = Numbers.Max(x => x.Item2);
                var maxValues = Numbers.Where(x => x.Item2 == max).ToList();
                int Number = maxValues.Count / 2;
                for (int i = 0; i < Number; i++) {
                    int Magnitude = 3 * maxValues[2 * i].Item1 + 2 * maxValues[2 * i + 1].Item1;
                    int index = Numbers.IndexOf(maxValues[2 * i]);
                    Numbers.Insert(index, new Tuple<int, int>(Magnitude, maxValues[2 * i].Item2 - 1));
                    Numbers.RemoveAt(index + 1);
                    Numbers.RemoveAt(index + 1);
                }
            }
            return Numbers[0].Item1;
        }
        public override string DoPartB() {
            int LargestMagnitude = 0;
            for (int i = 0; i < mValues.Count - 1; i++) {
                for (int y = i+1; y <mValues.Count-1;y++) {
                    List<string> newSummands = new List<string>();
                    newSummands.Add(mValues[i]);
                    newSummands.Add(mValues[y]);
                    string DecodedMagnitude = SnailfishAddition(newSummands);
                    int Magnitude = DoWork(DecodedMagnitude);
                    if (Magnitude > LargestMagnitude) LargestMagnitude = Magnitude;
                    newSummands.Clear();
                    newSummands.Add(mValues[y]);
                    newSummands.Add(mValues[i]);
                    DecodedMagnitude = SnailfishAddition(newSummands);
                    Magnitude = DoWork(DecodedMagnitude);
                    if (Magnitude > LargestMagnitude) LargestMagnitude = Magnitude;
                }
            }
            return $"{LargestMagnitude}";
        }
    }
}
