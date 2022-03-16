using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day25 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_25_1.txt";
        char[,] mValues;
        public override void ReadIn() {
            List<string> Lines = new List<string>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                Lines.Add(lLine);
            }
            int x = Lines[0].Length;
            int y = Lines.Count;
            mValues = new char[y, x];
            for (int iy = 0; iy < y; iy++) {
                for (int ix = 0; ix < x; ix++) {
                    if (Lines[iy].Length <= ix) continue;
                    mValues[iy, ix] = Lines[iy][ix];
                }
            }
            return;
        }

        public override string DoPartA() {
            int y = mValues.GetLength(0);
            int x = mValues.GetLength(1);
            bool Moved = true;
            int Count = 0;
            while (Moved) {
                Moved = false;
                //Count++;
                for (int iy = 0; iy < y; iy++) {
                    bool firstmoved = false;
                    for (int ix = 0; ix < x; ix++) {
                        if (mValues[iy, ix] == '>') {
                            if (ix == x - 1) {
                                if (mValues[iy, 0] == '.') {
                                    if (firstmoved) continue;
                                    mValues[iy, 0] = '>';
                                    mValues[iy, ix] = '.';
                                    Moved = true;
                                }
                            } else {
                                if (mValues[iy, ix + 1] == '.') {
                                    if (ix == 0) firstmoved = true;
                                    mValues[iy, ix + 1] = '>';
                                    mValues[iy, ix] = '.';
                                    ix++;
                                    Moved = true;
                                }
                            }
                        }
                    }
                }
                for (int ix = 0; ix < x; ix++) {
                    bool firstmoved = false;
                    for (int iy = 0; iy < y; iy++) {
                        if (mValues[iy, ix] == 'v') {
                            if (iy == y - 1) {
                                if (mValues[0, ix] == '.') {
                                    if (firstmoved) continue;
                                    mValues[0, ix] = 'v';
                                    mValues[iy, ix] = '.';
                                    Moved = true;
                                }
                            } else {
                                if (mValues[iy + 1, ix] == '.') {
                                    if (iy == 0) firstmoved = true;
                                    mValues[iy + 1, ix] = 'v';
                                    mValues[iy, ix] = '.';
                                    iy++;
                                    Moved = true;
                                }
                            }
                        }
                    }
                }
                Count++;
            }
            return $"{Count}";
        }

        public override string DoPartB() {
            throw new NotImplementedException();
        }
    }
}
