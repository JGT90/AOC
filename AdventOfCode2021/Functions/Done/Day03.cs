using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day03 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_03_1.txt";
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
            Console.WriteLine($"{mValues.Count} values found in file, {mPath}");
            return;
        }
        public override string DoPartA() {
            string lGammaRate = string.Empty;
            string lEpsilonRate = string.Empty;
            for (int i = 0; i < mValues[0].Length; i++) {
                int lCountOnes = 0;
                int lCountZeros = 0;
                foreach (string lValue in mValues) {
                    if (lValue[i] == '1') {
                        lCountOnes++;
                    } else {
                        lCountZeros++;
                    }
                }
                if (lCountOnes > lCountZeros) {
                    lGammaRate += "1";
                    lEpsilonRate += "0";
                } else {
                    lGammaRate += "0";
                    lEpsilonRate += "1";
                }
            }
            int lGamma = Convert.ToInt32(lGammaRate, 2);
            int lEpsilon = Convert.ToInt32(lEpsilonRate, 2);
            //Console.WriteLine($"GammaRate is {lGamma}");
            //Console.WriteLine($"EpsilonRate is {lEpsilon}");
            return $"{lGamma * lEpsilon}";
        }

        public override string DoPartB() {
            return $"{GetOxygen(mValues) * GetScrubber(mValues)}";
        }

        public int GetOxygen(List<string> aValues) {
            for (int i = 0; i < aValues[0].Length; i++) {
                List<string> lOnes = new List<string>();
                List<string> lZeros = new List<string>();
                int lCountOnes = 0;
                int lCountZeros = 0;
                foreach (string lValue in aValues) {
                    if (lValue[i] == '1') {
                        lOnes.Add(lValue);
                        lCountOnes++;
                    } else {
                        lZeros.Add(lValue);
                        lCountZeros++;
                    }
                }
                if (lCountOnes > lCountZeros) {
                    aValues = lOnes;
                } else if (lCountOnes < lCountZeros) {
                    aValues = lZeros;
                } else {
                    aValues = lOnes;
                }
            }
            return Convert.ToInt32(aValues[0], 2);
        }

        public int GetScrubber(List<string> aValues) {
            for (int i = 0; i < aValues[0].Length; i++) {
                List<string> lOnes = new List<string>();
                List<string> lZeros = new List<string>();
                int lCountOnes = 0;
                int lCountZeros = 0;
                foreach (string lValue in aValues) {
                    if (lValue[i] == '1') {
                        lOnes.Add(lValue);
                        lCountOnes++;
                    } else {
                        lZeros.Add(lValue);
                        lCountZeros++;
                    }
                }
                if (aValues.Count == 1) {
                    return Convert.ToInt32(aValues[0], 2);
                }
                if (lCountOnes > lCountZeros) {
                    aValues = lZeros;
                } else if (lCountOnes < lCountZeros) {
                    aValues = lOnes;
                } else {
                    aValues = lZeros;
                }
            }
            return Convert.ToInt32(aValues[0], 2);
        }
    }
}
