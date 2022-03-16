using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day07 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_07_1.txt";
        List<int> mValues;
        public override void ReadIn() {
            mValues = new List<int>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            List<string> lLines = new List<string>();
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                if (lLine == string.Empty) {
                    break;
                }
                lLines.Add(lLine);
            }
            foreach (string lLine in lLines) {
                string[] lValues = lLine.Split(',');
                for (int i = 0; i < lValues.Length; i++) {
                    mValues.Add(int.Parse(lValues[i]));
                }
            }
            Console.WriteLine($"{mValues.Count} values found in file, {mPath}");
            return;
        }

        private int GetMaxPosition(List<int> mValues) {
            return mValues.Max();
        }

        private int GetMinPosition(List<int> mValues) {
            return mValues.Min();
        }

        public override string DoPartA() {
            double lBestFuelConsumption = double.MaxValue;
            int lBestPosition = 0;
            int lMaxPosition = GetMaxPosition(mValues);
            int lMinPosition = GetMinPosition(mValues);
            for (int i = lMinPosition; i < lMaxPosition; i++) {
                int lFuelConsumption = 0;
                foreach(int lPosition in mValues) {
                    lFuelConsumption += Math.Abs(lPosition - i);
                }
                if (lFuelConsumption < lBestFuelConsumption) {
                    lBestFuelConsumption = lFuelConsumption;
                    lBestPosition = i;
                }
            }
            Console.WriteLine($"Crabs should align at position, {lBestPosition}");
            return $"{lBestFuelConsumption}";
        }

        public override string DoPartB() {
            double lBestFuelConsumption = double.MaxValue;
            int lBestPosition = 0;
            int lMaxPosition = GetMaxPosition(mValues);
            int lMinPosition = GetMinPosition(mValues);
            for (int i = lMinPosition; i < lMaxPosition; i++) {
                int lFuelConsumption = 0;
                foreach (int lPosition in mValues) {
                    int lDistance = Math.Abs(lPosition - i);
                    int lFuelCost = 1;
                    for (int y = 0; y < lDistance; y++, lFuelCost++) {
                        lFuelConsumption += lFuelCost;
                    }
                }
                if (lFuelConsumption < lBestFuelConsumption) {
                    lBestFuelConsumption = lFuelConsumption;
                    lBestPosition = i;
                }
            }
            Console.WriteLine($"Crabs should align at position, {lBestPosition}");
            return $"{lBestFuelConsumption}";
        }

    }
}
