using SEGCC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021 {
    internal class Week24 : DayN {

        List<InstructionData> mInstructions = new List<InstructionData>();
        int[] Calculations;
        Dictionary<int, string> Positions;

        public override string Part1() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week24.txt");

            foreach (string lLine in lInput) {
                string[] split = lLine.Split(' ');
                InstructionData data = new InstructionData() { inst = (Instruction)Enum.Parse(typeof(Instruction), split[0]), ValueA = split[1] };
                if (split.Length == 3) data.ValueB = split[2];
                mInstructions.Add(data);
            }
            Evaluate();
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

        public override string Part2() {
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
    }
}