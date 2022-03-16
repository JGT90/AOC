using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day17 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_17_1.txt";

        static int xMin;
        static int xMax;
        static int yMin;
        static int yMax;
        static List<int> xVelo;
        static List<int> yVelo;
        static List<Hit> pairs;
        private class Hit {
            public int xVelo;
            public int yVelo;
            public Hit(int axVelo, int ayVelo) {
                xVelo = axVelo;
                yVelo = ayVelo;
            }
        }

        public override void ReadIn() {
            xVelo = new List<int>();
            yVelo = new List<int>();
            pairs = new List<Hit>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }

            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                string[] lSplit = lLine.Split(new string[] { "=", ",", ".." }, StringSplitOptions.None);
                int a = 0;
                xMin = int.Parse(lSplit[1]);
                xMax = int.Parse(lSplit[2]);
                yMin = int.Parse(lSplit[4]);
                yMax = int.Parse(lSplit[5]);
            }
            Console.WriteLine($"Target area x={xMin}..{xMax} and y={yMin}..{yMax}");
        }

        public override string DoPartA() {
            for (int i = xMax; i >= 0; i--) {
                int xHit = 0;
                for (int n = 0; n < 1000; n++) {
                    xHit += i - n;
                    if (xHit <= xMax && xHit >= xMin) {
                        xVelo.Add(i);
                        break;
                    }
                    if (xHit > xMax) break;
                }
            }
            //Console.WriteLine($"xVelo={String.Join(",", xVelo.ToArray())}");

            RootQuadraticEquation(1, 1, 2 * yMax, out double xx1, out double xx2, out int _);
            int nMin = xx1 > xx2 ? (int)(xx1 + 0.5) : (int)(xx2 + 0.5);
            for (int i = yMin; i < 1000; i++) {
                int yHit = 0;
                for (int n = 0; n < 1000; n++) {
                    yHit += i - n;
                    if (yHit <= yMax && yHit >= yMin) {
                        yVelo.Add(i);
                        break;
                    }
                    if (yHit < yMin) break;
                }
            }

            //Console.WriteLine($"yVelo={String.Join(",", yVelo.ToArray().Distinct())}");
            return $"{SumOfIntegers(yVelo.Max())}";
        }

        public override string DoPartB() {
            foreach (int x in xVelo) {
                foreach (int y in yVelo.Distinct()) {
                    int xvel = x;
                    int xHit = 0;
                    int yHit = 0;
                    int yvel = y;
                    for (int step = 1; step < 10000; step++) {
                        xHit += xvel;
                        yHit += yvel;
                        if (xvel != 0) xvel--;
                        yvel--;
                        if (xHit <= xMax && xHit >= xMin && yHit <= yMax && yHit >= yMin) {
                            pairs.Add(new Hit(x, y));
                            break;
                        }
                        if (yHit < yMin) {
                            break;
                        }
                    }
                }
            }
            //Console.WriteLine($"Number of pairs: {pairs.Count}");
            return $"{pairs.Count}";
        }

        private static int SumOfIntegers(int aVelo) {
            return aVelo * (aVelo + 1) / 2;
        }
        private static bool RootQuadraticEquation(int a, int b, int c, out double x1, out double x2, out int NextQuadratic) {
            x1 = 0;
            x2 = 0;
            NextQuadratic = -1;
            int d = b * b - 4 * a * c;
            if (d == 0) {
                x1 = -b / (2 * a);
                x2 = x1;
            } else if (d > 0) {
                x1 = (-b + Math.Sqrt(d)) / (2 * a);
                x2 = (-b - Math.Sqrt(d)) / (2 * a);
                NextQuadratic = (int)(Math.Sqrt(d) + 0.5);
            } else {
                return false;
            }
            if (x1 == (int)x1) return true;
            return false;
        }
    }
}
