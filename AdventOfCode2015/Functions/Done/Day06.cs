using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Functions {
    public class Day06 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_06_1.txt";
        List<string> mInstructions;
        public override string DoPartA() {
            bool[,] lLights = new bool[1000, 1000];
            for (int i = 0; i < mInstructions.Count; i++) {
                int xStart = 0;
                int xEnd = 0;
                int yStart = 0;
                int yEnd = 0;
                int config = -1;
                if (mInstructions[i].Contains("turn on")) {
                    mInstructions[i] = mInstructions[i].Remove(0, 8);
                    config = 0;
                } else if (mInstructions[i].Contains("turn off")) {
                    mInstructions[i] = mInstructions[i].Remove(0, 9);
                    config = 1;
                } else if (mInstructions[i].Contains("toggle")) {
                    mInstructions[i] = mInstructions[i].Remove(0, 7);
                    config = 2;
                }
                string[] lSplit = mInstructions[i].Split(new string[] { "through" }, StringSplitOptions.None);
                string[] lStart = lSplit[0].Split(',');
                string[] lEnd = lSplit[1].Split(',');
                xStart = int.Parse(lStart[0]);
                yStart = int.Parse(lStart[1]);
                xEnd = int.Parse(lEnd[0]);
                yEnd = int.Parse(lEnd[1]);
                for (int y = yStart; y < yEnd+1; y++) {
                    for (int x = xStart; x < xEnd+1; x++) {
                        if (config == 0) {
                            lLights[y, x] = true;
                        } else if (config == 1) {
                            lLights[y, x] = false;
                        } else if (config == 2) {
                            lLights[y, x] = !lLights[y, x];
                        }
                    }

                }
            }
            int lCount = 0;
            for (int y = 0; y < 1000; y++) {
                for (int x = 0; x < 1000; x++) {
                    if (lLights[y, x]) lCount++;
                }
            }
            return $"{lCount}";
        }

        public override string DoPartB() {
            int[,] lLights = new int[1000, 1000];
            for (int i = 0; i < mInstructions.Count; i++) {
                int xStart = 0;
                int xEnd = 0;
                int yStart = 0;
                int yEnd = 0;
                int config = -1;
                if (mInstructions[i].Contains("turn on")) {
                    mInstructions[i] = mInstructions[i].Remove(0, 8);
                    config = 0;
                } else if (mInstructions[i].Contains("turn off")) {
                    mInstructions[i] = mInstructions[i].Remove(0, 9);
                    config = 1;
                } else if (mInstructions[i].Contains("toggle")) {
                    mInstructions[i] = mInstructions[i].Remove(0, 7);
                    config = 2;
                }
                string[] lSplit = mInstructions[i].Split(new string[] { "through" }, StringSplitOptions.None);
                string[] lStart = lSplit[0].Split(',');
                string[] lEnd = lSplit[1].Split(',');
                xStart = int.Parse(lStart[0]);
                yStart = int.Parse(lStart[1]);
                xEnd = int.Parse(lEnd[0]);
                yEnd = int.Parse(lEnd[1]);
                for (int y = yStart; y < yEnd + 1; y++) {
                    for (int x = xStart; x < xEnd + 1; x++) {
                        if (config == 0) {
                            lLights[y,x]++;
                        } else if (config == 1) {
                            lLights[y, x]--;
                            if (lLights[y, x] < 0) lLights[y, x] = 0;
                        } else if (config == 2) {
                            lLights[y, x] += 2;
                        }
                    }
                }
            }
            int lCount = 0;
            for (int y = 0; y < 1000; y++) {
                for (int x = 0; x < 1000; x++) {
                    lCount += lLights[y, x];
                }
            }
            return $"{lCount}";
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
    }
}
