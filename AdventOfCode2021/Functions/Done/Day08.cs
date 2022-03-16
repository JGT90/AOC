using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day08 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_08_1.txt";
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
        public static List<string> GetOutputValue(List<string> aInput) {
            List<string> lValues = new List<string>();
            foreach (string lInput in aInput) {
                string[] lSplit = lInput.Split('|');
                lValues.Add(lSplit[1]);
            }
            return lValues;
        }

        public override string DoPartA() {
            List<string> aValues = GetOutputValue(mValues);
            int lTotalSum = 0;
            int lOneSum = 0;
            int lFourSum = 0;
            int lSevenSum = 0;
            int lEightSum = 0;
            foreach (string lValue in aValues) {
                string[] lSplit = lValue.Split(' ');
                for (int i = 0; i < lSplit.Length; i++) {
                    int lLength = lSplit[i].Length;
                    switch (lLength) {
                        case 2:
                            // One
                            lOneSum++;
                            break;
                        case 3:
                            // Seven
                            lSevenSum++;
                            break;
                        case 4:
                            // Four
                            lFourSum++;
                            break;
                        case 7:
                            // Eight
                            lEightSum++;
                            break;
                        default:
                            break;
                    }
                }
            }
            lTotalSum = lOneSum + lFourSum + lSevenSum + lEightSum;
            return $"{lTotalSum}";
        }
        public override string DoPartB() {
            List<SevenDigitDisplay> lDisplay = new List<SevenDigitDisplay>();
            foreach (string lValue in mValues) {
                lDisplay.Add(new SevenDigitDisplay(lValue));

            }
            int lSum = 0;
            foreach (SevenDigitDisplay ldis in lDisplay) {
                lSum += ldis.OutputValue;
            }
            return $"{lSum}";
        }
        public class SevenDigitDisplay {
            public string Top;
            public string TopLeft;
            public string TopRight;
            public string Center;
            public string BottomLeft;
            public string BottomRight;
            public string Bottom;
            public int OutputValue;
            public SevenDigitDisplay(string aLine) {
                string[] lSplit = aLine.Split('|');
                DeterminePositions(lSplit[0]);
                GetValue(lSplit[1]);
            }
            private void GetValue(string aValues) {
                string[] lSplit = aValues.Split(' ');
                string lNumberString = string.Empty;
                for (int i = 0; i < lSplit.Length; i++) {
                    int lLength = lSplit[i].Length;
                    switch (lLength) {
                        case 2:
                            // One
                            lNumberString += "1";
                            break;
                        case 3:
                            // Seven
                            lNumberString += "7";
                            break;
                        case 4:
                            // Four
                            lNumberString += "4";
                            break;
                        case 5:
                            if (lSplit[i].Contains(TopLeft)) {
                                // Five
                                lNumberString += "5";
                            } else if (lSplit[i].Contains(BottomLeft)) {
                                // Two
                                lNumberString += "2";
                            } else {
                                // Three
                                lNumberString += "3";
                            }
                            break;
                        case 6:
                            if (lSplit[i].Contains(Center)) {
                                if (lSplit[i].Contains(TopRight)) {
                                    // Nine
                                    lNumberString += "9";
                                } else {
                                    // Six
                                    lNumberString += "6";
                                }
                            } else {
                                // Zero
                                lNumberString += "0";
                            }
                            break;
                        case 7:
                            // Eight
                            lNumberString += "8";
                            break;
                        default:
                            break;
                    }
                }
                OutputValue = int.Parse(lNumberString);
            }
            private void DeterminePositions(string aValues) {
                string[] lSplit = aValues.Split(' ');
                string Right = lSplit.Where(x => x.Length == 2).FirstOrDefault();
                string lTopTemp = lSplit.Where(x => x.Length == 3).FirstOrDefault();
                string lFourTemp = lSplit.Where(x => x.Length == 4).FirstOrDefault();
                for (int i = 0; i < 2; i++) {
                    lTopTemp = lTopTemp.Remove(lTopTemp.IndexOf(Right[i]), 1);
                }
                Top = lTopTemp;
                string lTemp = lFourTemp + Top;
                string[] lFiveDigitTemp = lSplit.Where(x => x.Length == 5).ToArray();
                string[] lSixDigitTemp = lSplit.Where(x => x.Length == 6).ToArray();
                for (int i = 0; i < lSixDigitTemp.Length; i++) {
                    int lMatch = 0;
                    foreach (char c in lTemp) {
                        if (lSixDigitTemp[i].Contains(c)) {
                            lMatch++;
                            continue;
                        }
                    }
                    if (lMatch == 5) {
                        foreach (char c in lSixDigitTemp[i]) {
                            if (!lTemp.Contains(c)) {
                                Bottom = c.ToString();
                            }
                        }
                    }
                }
                lTemp = Top + Bottom + Right;
                for (int i = 0; i < lFiveDigitTemp.Length; i++) {
                    int lMatch = 0;
                    foreach (char c in lTemp) {
                        if (lFiveDigitTemp[i].Contains(c)) {
                            lMatch++;
                            continue;
                        }
                    }
                    if (lMatch == 4) {
                        foreach (char c in lFiveDigitTemp[i]) {
                            if (!lTemp.Contains(c)) {
                                Center = c.ToString();
                            }
                        }
                    }
                }
                foreach (char c in lFourTemp) {
                    if (!Right.Contains(c)) {
                        if (Center != c.ToString()) {
                            TopLeft = c.ToString();
                        }
                    }
                }
                lTemp = Top + Center + Bottom + TopLeft;
                for (int i = 0; i < lFiveDigitTemp.Length; i++) {
                    int lMatch = 0;
                    foreach (char c in lTemp) {
                        if (lFiveDigitTemp[i].Contains(c)) {
                            lMatch++;
                            continue;
                        }
                    }
                    if (lMatch == 4) {
                        foreach (char c in lFiveDigitTemp[i]) {
                            if (!lTemp.Contains(c)) {
                                BottomRight = c.ToString();
                                TopRight = Right.Remove(Right.IndexOf(c), 1);
                            }
                        }
                    }
                }
                lTemp = "abcdefg";
                lTemp = lTemp.Remove(lTemp.IndexOf(Top), 1);
                lTemp = lTemp.Remove(lTemp.IndexOf(TopLeft), 1);
                lTemp = lTemp.Remove(lTemp.IndexOf(TopRight), 1);
                lTemp = lTemp.Remove(lTemp.IndexOf(Center), 1);
                lTemp = lTemp.Remove(lTemp.IndexOf(BottomRight), 1);
                lTemp = lTemp.Remove(lTemp.IndexOf(Bottom), 1);
                BottomLeft = lTemp;
            }
        }
    }
}
