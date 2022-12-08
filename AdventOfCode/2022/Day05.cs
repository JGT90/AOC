using System.IO;

namespace AdventOfCode.Year2022 {
    class Day05 {
        public string DoPartA() {
            string lPath = @"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode2022\Input\Day05.txt";
            string[] lStacks = new string[10];
            //lStacks[1] = "ZN";
            //lStacks[2] = "MCD";
            //lStacks[3] = "P";
            lStacks[1] = "RNPG";
            lStacks[2] = "TJBLCSVH";
            lStacks[3] = "TDBMNL";
            lStacks[4] = "RVPSB";
            lStacks[5] = "GCQSWMVH";
            lStacks[6] = "WQSCDBJ";
            lStacks[7] = "FQL";
            lStacks[8] = "WMHTDLFV";
            lStacks[9] = "LPBVMJF";
            bool lInstructions = false;
            foreach (string lLine in File.ReadAllLines(lPath)) {
                if (string.IsNullOrEmpty(lLine)) {
                    lInstructions = true;
                    continue;
                }
                if (!lInstructions) continue;
                string[] lSplit = lLine.Split(' ');
                int From = int.Parse(lSplit[3]);
                int To = int.Parse(lSplit[5]);
                int Length = int.Parse(lSplit[1]);
                for (int i = 0; i < Length; i++) {
                    lStacks[To] += lStacks[From][lStacks[From].Length - 1-i];
                }
                lStacks[From] = lStacks[From].Remove(lStacks[From].Length - Length);
            }
            return $"{lStacks[1][lStacks[1].Length - 1]}{lStacks[2][lStacks[2].Length - 1]}{lStacks[3][lStacks[3].Length - 1]}{lStacks[4][lStacks[4].Length - 1]}{lStacks[5][lStacks[5].Length - 1]}{lStacks[6][lStacks[6].Length - 1]}{lStacks[7][lStacks[7].Length - 1]}{lStacks[8][lStacks[8].Length - 1]}{lStacks[9][lStacks[9].Length - 1]}";
        }

        public string DoPartB() {
            string lPath = @"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode2022\Input\Day05.txt";
            string[] lStacks = new string[10];
            //lStacks[1] = "ZN";
            //lStacks[2] = "MCD";
            //lStacks[3] = "P";
            lStacks[1] = "RNPG";
            lStacks[2] = "TJBLCSVH";
            lStacks[3] = "TDBMNL";
            lStacks[4] = "RVPSB";
            lStacks[5] = "GCQSWMVH";
            lStacks[6] = "WQSCDBJ";
            lStacks[7] = "FQL";
            lStacks[8] = "WMHTDLFV";
            lStacks[9] = "LPBVMJF";
            bool lInstructions = false;
            foreach (string lLine in File.ReadAllLines(lPath)) {
                if (string.IsNullOrEmpty(lLine)) {
                    lInstructions = true;
                    continue;
                }
                if (!lInstructions) continue;
                string[] lSplit = lLine.Split(' ');
                int From = int.Parse(lSplit[3]);
                int To = int.Parse(lSplit[5]);
                int Length = int.Parse(lSplit[1]);
                lStacks[To] += lStacks[From].Substring(lStacks[From].Length - Length);
                //for (int i = 0; i < Length; i++) {
                //    lStacks[To] += lStacks[From][lStacks[From].Length - 1 - i];
                //}
                lStacks[From] = lStacks[From].Remove(lStacks[From].Length - Length);
            }
            return $"{lStacks[1][lStacks[1].Length - 1]}{lStacks[2][lStacks[2].Length - 1]}{lStacks[3][lStacks[3].Length - 1]}{lStacks[4][lStacks[4].Length - 1]}{lStacks[5][lStacks[5].Length - 1]}{lStacks[6][lStacks[6].Length - 1]}{lStacks[7][lStacks[7].Length - 1]}{lStacks[8][lStacks[8].Length - 1]}{lStacks[9][lStacks[9].Length - 1]}";

        }
    }
}
