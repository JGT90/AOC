using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day06 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_06_1.txt";
        List<Fish> mFish;
        public class Fish {
            public int DaysToReproduce;
            public Fish(int aDaysToReproduce) {
                DaysToReproduce = aDaysToReproduce;
            }

            public Fish CountDown() {
                if (DaysToReproduce == 0) {
                    DaysToReproduce = 6;
                    return new Fish(8);
                }
                DaysToReproduce--;
                return null;
            }
        }

        public override void ReadIn() {
            mFish = new List<Fish>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                string[] lValues = lLine.Split(',');
                for (int i = 0; i < lValues.Length; i++) {
                    mFish.Add(new Fish(int.Parse(lValues[i])));
                }
            }
            Console.WriteLine($"{mFish.Count} values found in file, {mPath}");
            return;
        }

        public override string DoPartA() {
            return $"{DoWork(mFish, 80)}";
        }
        public override string DoPartB() {
            return $"{DoWork(mFish, 256)}";
        }

        public double DoWork(List<Fish> aFishes, int aDays) {
            double Group0 = aFishes.Where(x => x.DaysToReproduce == 0).ToArray().Length;
            double Group1 = aFishes.Where(x => x.DaysToReproduce == 1).ToArray().Length;
            double Group2 = aFishes.Where(x => x.DaysToReproduce == 2).ToArray().Length;
            double Group3 = aFishes.Where(x => x.DaysToReproduce == 3).ToArray().Length;
            double Group4 = aFishes.Where(x => x.DaysToReproduce == 4).ToArray().Length;
            double Group5 = aFishes.Where(x => x.DaysToReproduce == 5).ToArray().Length;
            double Group6 = aFishes.Where(x => x.DaysToReproduce == 6).ToArray().Length;
            double Group7 = aFishes.Where(x => x.DaysToReproduce == 7).ToArray().Length;
            double Group8 = aFishes.Where(x => x.DaysToReproduce == 8).ToArray().Length;
            double lTempFirst;
            double lTempSecond;

            for (int i = 0; i < aDays; i++) {
                lTempFirst = Group7;
                Group7 = Group8;
                lTempSecond = Group6;
                Group6 = lTempFirst;
                lTempFirst = Group5;
                Group5 = lTempSecond;
                lTempSecond = Group4;
                Group4 = lTempFirst;
                lTempFirst = Group3;
                Group3 = lTempSecond;
                lTempSecond = Group2;
                Group2 = lTempFirst;
                lTempFirst = Group1;
                Group1 = lTempSecond;
                lTempSecond = Group0;
                Group0 = lTempFirst;
                Group8 = lTempSecond;
                Group6 += lTempSecond;
            }
            double lSum = Group0 + Group1 + Group2 + Group3 + Group4 + Group5 + Group6 + Group7 + Group8;
            return lSum;
        }
    }
}
