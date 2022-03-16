using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day20 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_20_1.txt";

        int[,] mValues;
        static int[] enhancement;
        static int width;
        static int height;
        public override void ReadIn() {
            List<string> Summands = new List<string>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                Summands.Add(lLine);
            }
            width = Summands[3].Length;
            height = Summands.Count - 2;
            mValues = new int[width, height];
            for (int y = 0; y < Summands.Count; y++) {
                for (int x = 0; x < Summands[y].Length; x++) {
                    if (y == 0) {
                        if (x == 0) enhancement = new int[Summands[y].Length];
                        enhancement[x] = Summands[y][x] == '#' ? 1 : 0;
                    } else if (y == 1) continue;
                    else {
                        mValues[y - 2, x] = Summands[y][x] == '#' ? 1 : 0;

                    }
                }
            }
        }

        private static int[,] ApplyEnhancement(int[,] aValues, int round) {
            int[,] TempValues = new int[Day20.width + 6, Day20.height + 6];
            for (int y = 0; y < Day20.width + 6; y++) {
                for (int x = 0; x < Day20.height + 6; x++) {
                    if (round % 2 == 0) TempValues[y, x] = 0;
                    else TempValues[y, x] = 1;
                    //TempValues[]
                }
            }
            for (int y = 0; y < Day20.width; y++) {
                for (int x = 0; x < Day20.height; x++) {
                    TempValues[y + 6 / 2 , x + 6 / 2 ] = aValues[y, x];
                }
            }
            Day20.width += 6;
            Day20.height += 6;
            int[,] NewValues = new int[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    StringBuilder sb = new StringBuilder();
                    if (y == 0 || y == (height- 1) || x == 0|| x == (width-1)) {
                        if (round % 2 == 0) NewValues[y, x] = 1;
                        else NewValues[y, x] = 0;
                        continue;
                    }
                    for (int n = -1; n < 2; n++) {
                        for (int m = -1; m < 2; m++) {
                            sb.Append(TempValues[y + n, x + m] == 1 ? (byte)1 : (byte)0);
                        }
                    }
                    //byte[] lValue = ASCIIEncoding.UTF8.GetBytes(sb.ToString());
                    int index = Convert.ToUInt16(sb.ToString(), 2);
                    NewValues[y, x] = enhancement[index];
                }
            }
            return NewValues;

        }
        public override string DoPartA() {
            for (int i = 0; i < 2; i++) {
                for (int y = 0; y < Day20.width; y++) {
                    for (int x = 0; x < Day20.height; x++) {
                        //Console.Write(lReadInValuesDay20[y, x] == 1 ? "#" : ".");
                    }
                    //Console.Write("\n");
                }
                mValues = Day20.ApplyEnhancement(mValues, i);
                //Console.Write("\n");
            }
            return $"{GetLight(mValues)}";
        }

        public override string DoPartB() {
            for (int i = 0; i < 50; i++) {
                mValues = Day20.ApplyEnhancement(mValues, i);
            }
            return $"{GetLight(mValues)}";
        }
        private int GetLight(int[,] aValues) {
            int lightcount = 0;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    //Console.Write(aValues[y, x] == 1 ? "#" : ".");
                    if (aValues[y, x] == 1) lightcount++;
                }
                //Console.Write("\n");
            }
            return lightcount;
        }
    }
}
