using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Functions {
    public class Day02 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_02_1.txt";
        List<Dimension> mValues;
        private class Dimension {
            public Dimension(int aLength, int aWidth, int aHeight) {
                Length = aLength;
                Width = aWidth;
                Height = aHeight;
            }
            public int Length { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }
        public override string DoPartA() {
            double lPaperFeet = 0;
            foreach (Dimension value in mValues) {
                double lSide1 = value.Length * value.Width;
                double lSide2 = value.Width * value.Height;
                double lSide3 = value.Height * value.Length;
                lPaperFeet += 2 * lSide1 + 2 * lSide2 + 2 * lSide3;
                double lAdd = double.MaxValue;
                if (lSide1 < lAdd) lAdd = lSide1;
                if (lSide2 < lAdd) lAdd = lSide2;
                if (lSide3 < lAdd) lAdd = lSide3;
                lPaperFeet += lAdd;
            }
            return $"{lPaperFeet}";
        }

        public override string DoPartB() {
            double lRibbonFeet = 0;
            foreach (Dimension value in mValues) {
                lRibbonFeet += value.Length * value.Width * value.Height;
                if (value.Length > value.Height && value.Length > value.Width) lRibbonFeet += 2 * value.Height + 2 * value.Width;
                else if (value.Width > value.Length && value.Width > value.Height) lRibbonFeet += 2 * value.Length + 2 * value.Height;
                else if (value.Height > value.Length && value.Height > value.Width) lRibbonFeet += 2 * value.Length + 2 * value.Width;
                else {
                    lRibbonFeet += 2 * value.Length;
                    if (value.Length == value.Height) lRibbonFeet += 2 * value.Width;
                    else lRibbonFeet += 2 * value.Height;
                }
            }
            return $"{lRibbonFeet}";
        }

        public override void ReadIn() {
            mValues = new List<Dimension>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                string[] split = lLine.Split('x');
                mValues.Add(new Dimension(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])));
            }
            return;
        }
    }
}
