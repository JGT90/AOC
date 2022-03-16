using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day14 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_14_1.txt";
        Dictionary<string, string> mValues;
        Dictionary<string, int> mDic;
        string mPolymerTemplate;
        public override void ReadIn() {
            mPolymerTemplate = string.Empty;
            mDic = new Dictionary<string, int>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            mValues = new Dictionary<string, string>();
            bool NowPairInsertion = false;
            int index = 0;
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                if (lLine == string.Empty) {
                    NowPairInsertion = true;
                    continue;
                }
                if (NowPairInsertion) {

                    string[] lsplit = lLine.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                    mValues.Add(lsplit[0].Trim(), lsplit[1].Trim());
                    try {
                        mDic.Add(lsplit[0].Trim(), index);
                        index++;
                    } catch (Exception) { }
                } else {
                    mPolymerTemplate = lLine;
                }
            }
            Console.WriteLine($"{mValues.Count} values found in file, {mPath}");
            return;
        }

        public override string DoPartA() {
            return $"{GetQuantity(mValues, mPolymerTemplate, 10, mDic)}";
        }
        public override string DoPartB() {
            return $"{GetQuantity(mValues, mPolymerTemplate, 40, mDic)}";
        }
        private double GetQuantity(Dictionary<string, string> aPairInsertion, string aPolymer, int aSteps, Dictionary<string, int> aDic) {
            double[] lQuantityPair = new double[aPairInsertion.Count];
            List<string> distinctPairValue = aPairInsertion.Select(x => x.Value).Distinct().ToList();
            for (int i = 0; i < aPolymer.Length - 1; i++) {
                lQuantityPair[aDic[$"{aPolymer[i]}{aPolymer[i + 1]}"]]++;
            }
            //int[] lDouble = new int[distinctPairValue.Count];
            string ended = aPolymer.Substring(aPolymer.Length - 2);
            int llastcharindex = distinctPairValue.IndexOf($"{ended.Last()}");
            double[] lTest = new double[distinctPairValue.Count];
            for (int i = 0; i < aSteps; i++) {
                //lDouble = new int[distinctPairValue.Count];
                lTest = new double[distinctPairValue.Count];
                double[] lTempQuantity = new double[aPairInsertion.Count];
                for (int y = 0; y < aPairInsertion.Count; y++) {
                    //lQuantity[y] 
                    if (lQuantityPair[y] > 0 ) {
                        string bla = aDic.Where(x => x.Value == y).Select(x => x.Key).FirstOrDefault();
                        string lInsert = aPairInsertion[bla];
                        int index = aDic[$"{bla.First()}{lInsert}"];
                        lTempQuantity[index] += lQuantityPair[y];
                        index = aDic[$"{lInsert}{bla.Last()}"];
                        lTempQuantity[index] += lQuantityPair[y];
                        int ldoubleindex = distinctPairValue.IndexOf($"{bla.First()}");
                        lTest[ldoubleindex] += lQuantityPair[y];
                        ldoubleindex = distinctPairValue.IndexOf(lInsert);
                        lTest[ldoubleindex] += lQuantityPair[y];
                    }
                }
                lTest[llastcharindex]++;
                lQuantityPair = lTempQuantity;
            }
            return lTest.Max() - lTest.Min();
        }
    }
}
