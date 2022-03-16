using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AdventOfCode2021.Functions {
    public class Day24 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_24_1.txt";

        List<InstructionData> mInstructions;
        int[] Calculations;
        Dictionary<int, string> Positions;
        private enum Instruction {
            inp,
            add,
            mul,
            div,
            mod,
            eql,
        }

        private class InstructionData {
            public Instruction inst { get; set; }
            public string ValueA { get; set; }
            public string ValueB { get; set; }
        }
        public override void ReadIn() {
            mInstructions = new List<InstructionData>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                string[] split = lLine.Split(' ');
                InstructionData data = new InstructionData() { inst = (Instruction)Enum.Parse(typeof(Instruction), split[0]), ValueA = split[1] };
                if (split.Length == 3) data.ValueB = split[2];
                mInstructions.Add(data);
            }
            Evaluate();
            return;
        }

        private void Evaluate() {
            Positions = new Dictionary<int, string>();
            var boundaries = mInstructions.Select((item, index) => new { Item = item, Index = index })
                              .Where(o => o.Item.inst == Instruction.inp)
                              .Select(o => o.Index).ToList();
            Calculations = new int[boundaries.Count / 2];
            bool[] lDone = new bool[boundaries.Count / 2];
            int lCount = 0;
            for (int i = 0; i < boundaries.Count; i++) {
                int end;
                if (i == boundaries.Count - 1) {
                    end = mInstructions.Count;
                } else {
                    end = boundaries[i + 1];
                }
                var lInstructionRange = mInstructions.GetRange(boundaries[i], end - boundaries[i]);
                int lIndex = lInstructionRange.FindIndex(x => x.inst == Instruction.add && x.ValueA == "x" && x.ValueB.Contains("-"));
                if (lIndex < 0) {
                    var inst = lInstructionRange.LastOrDefault(x => x.inst == Instruction.add && x.ValueA == "y");
                    Calculations[lCount] += int.Parse(inst.ValueB);
                    Positions.Add(lCount, i.ToString());
                    lCount++;
                } else {
                    while (lDone[lCount - 1] == true) lCount--;
                    Calculations[lCount - 1] += int.Parse(lInstructionRange[lIndex].ValueB);
                    Positions[lCount - 1] += $",{i}";
                    lDone[lCount - 1] = true;
                }
            }
            int a = 0;
        }

        public override string DoPartA() {
            string lResultString = "00000000000000";
            for (int i = 0; i < Calculations.Length; i++) {
                for (int y = 9; y > 0; y--) {
                    int lResult = Calculations[i] + y;
                    if (lResult > 0 && lResult < 10) {
                        string[] split = Positions[i].Split(',');
                        int index = int.Parse(split[0]);
                        lResultString = lResultString.Remove(index, 1);
                        lResultString = lResultString.Insert(index, y.ToString());
                        index = int.Parse(split[1]);
                        lResultString = lResultString.Remove(index, 1);
                        lResultString = lResultString.Insert(index, lResult.ToString());
                        break;
                    }
                }
            }

            return $"{double.Parse(lResultString)}";
        }

        public override string DoPartB() {
            string lResultString = "00000000000000";
            for (int i = 0; i < Calculations.Length; i++) {
                for (int y = 1; y < 10; y++) {
                    int lResult = Calculations[i] + y;
                    if (lResult > 0 && lResult < 10) {
                        string[] split = Positions[i].Split(',');
                        int index = int.Parse(split[0]);
                        lResultString = lResultString.Remove(index, 1);
                        lResultString = lResultString.Insert(index, y.ToString());
                        index = int.Parse(split[1]);
                        lResultString = lResultString.Remove(index, 1);
                        lResultString = lResultString.Insert(index, lResult.ToString());
                        break;
                    }
                }
            }

            return $"{double.Parse(lResultString)}";
        }
    }
}
