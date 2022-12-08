using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021 {
    internal class Day13 : DayN {
        bool[,] TransparentList = new bool[1311, 895];
        List<FoldingInstruction> FoldingInstructions = new List<FoldingInstruction>();
        public override string Part1() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week13.txt");
            bool lInstructionsActive = false;
            for (int i = 0; i < lInput.Length; i++) {
                if (lInput[i] == string.Empty) {
                    lInstructionsActive = true;
                    continue;
                }
                if (!lInstructionsActive) {
                    string[] lSplit = lInput[i].Split(',');
                    TransparentList[int.Parse(lSplit[0]), int.Parse(lSplit[1])] = true;
                } else {
                    if (lInput[i].Contains("y")) FoldingInstructions.Add(new FoldingInstruction(false, int.Parse(lInput[i].Split('=')[1])));
                    else FoldingInstructions.Add(new FoldingInstruction(true, int.Parse(lInput[i].Split('=')[1])));
                }
            }
            bool[,] OneTimeFolded = Fold(TransparentList, FoldingInstructions[0].IsVertical, FoldingInstructions[0].Foldline);
            double lCount = 0;
            for (int x = 0; x < OneTimeFolded.GetLength(0); x++) {
                for (int y = 0; y < OneTimeFolded.GetLength(1); y++) {
                    if (OneTimeFolded[x, y]) lCount++;
                }
            }
            return $"{lCount}";
        }

        public override string Part2() {
            for (int i = 0; i < FoldingInstructions.Count; i++) {
                TransparentList = Fold(TransparentList, FoldingInstructions[i].IsVertical, FoldingInstructions[i].Foldline);
            }
            for (int y = 0; y < TransparentList.GetLength(1); y++) {
                for (int x = 0; x < TransparentList.GetLength(0); x++) {
                    if (TransparentList[x, y]) Console.Write("#");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
            string lReturnValue = GetActivationCode();
            Console.WriteLine($"Activation Code ==> {lReturnValue}");
            return lReturnValue;
        }

        private string GetActivationCode() {
            string lActivationCode = string.Empty;
            for (int i = 0; i < TransparentList.GetLength(0) / 5; i++) {
                string[,] lCheck = new string[6, 4];
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 6; y++) {
                        if (TransparentList[x + 4 * i + i, y]) lCheck[y, x] = "#";
                        else lCheck[y, x] = ".";
                    }
                }
                lActivationCode += GetLetter(lCheck);
            }

            return lActivationCode;
        }
        private string GetLetter(string[,] a1) {
            if (TwoDArraysEqual<string>(a1, LETTER_A)) return "A";
            else if (TwoDArraysEqual<string>(a1, LETTER_B)) return "B";
            else if (TwoDArraysEqual<string>(a1, LETTER_C)) return "C";
            else if (TwoDArraysEqual<string>(a1, LETTER_E)) return "E";
            else if (TwoDArraysEqual<string>(a1, LETTER_F)) return "F";
            else if (TwoDArraysEqual<string>(a1, LETTER_G)) return "G";
            else if (TwoDArraysEqual<string>(a1, LETTER_H)) return "H";
            else if (TwoDArraysEqual<string>(a1, LETTER_I)) return "I";
            else if (TwoDArraysEqual<string>(a1, LETTER_J)) return "J";
            else if (TwoDArraysEqual<string>(a1, LETTER_K)) return "K";
            else if (TwoDArraysEqual<string>(a1, LETTER_L)) return "L";
            else if (TwoDArraysEqual<string>(a1, LETTER_O)) return "O";
            else if (TwoDArraysEqual<string>(a1, LETTER_P)) return "P";
            else if (TwoDArraysEqual<string>(a1, LETTER_R)) return "R";
            else if (TwoDArraysEqual<string>(a1, LETTER_S)) return "S";
            else if (TwoDArraysEqual<string>(a1, LETTER_U)) return "U";
            else if (TwoDArraysEqual<string>(a1, LETTER_Z)) return "Z";
            return "";
        }

        static bool TwoDArraysEqual<T>(T[,] a1, T[,] a2) {
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int x = 0; x < a1.GetLength(0); x++) {
                for (int y = 0; y < a1.GetLength(1); y++) {
                    if (!comparer.Equals(a1[x, y], a2[x, y])) return false;
                }
            }
            return true;
        }

        private bool[,] Fold(bool[,] aList, bool aVertical, int aFoldline) {
            bool[,] NewList;
            if (aVertical) {
                int lYLength = aList.GetLength(1);
                int lXLength = aList.GetLength(0);
                NewList = new bool[aFoldline, lYLength];
                for (int x = 0; x < aFoldline; x++) {
                    for (int y = 0; y < lYLength; y++) {
                        NewList[x, y] = aList[x, y] || aList[lXLength - 1 - x, y];
                    }
                }
            } else {
                int lYLength = aList.GetLength(1);
                int lXLength = aList.GetLength(0);
                NewList = new bool[lXLength, aFoldline];
                for (int x = 0; x < lXLength; x++) {
                    for (int y = 0; y < aFoldline; y++) {
                        NewList[x, y] = aList[x, y] || aList[x, lYLength - 1 - y];
                    }
                }
            }
            return NewList;
        }

        internal class FoldingInstruction {
            public FoldingInstruction(bool aIsVertical, int aFoldline) {
                IsVertical = aIsVertical;
                Foldline = aFoldline;
            }

            public bool IsVertical { get; set; }
            public int Foldline { get; set; }
        }
        #region Letter
        private string[,] LETTER_A = { { ".","#","#","." },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { "#","#","#","#" },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" }
        };
        private string[,] LETTER_B = { { "#","#","#","." },
                                       { "#",".",".","#" },
                                       { "#","#","#","." },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { "#","#","#","." }
        };
        private string[,] LETTER_C = { { ".","#","#","." },
                                       { "#",".",".","#" },
                                       { "#",".",".","." },
                                       { "#",".",".","." },
                                       { "#",".",".","#" },
                                       { ".","#","#","." }
        };
        private string[,] LETTER_E = { { "#","#","#","#" },
                                       { "#",".",".","." },
                                       { "#","#","#","." },
                                       { "#",".",".","." },
                                       { "#",".",".","." },
                                       { "#","#","#","#" }
        };
        private string[,] LETTER_F = { { "#","#","#","#" },
                                       { "#",".",".","." },
                                       { "#","#","#","." },
                                       { "#",".",".","." },
                                       { "#",".",".","." },
                                       { "#",".",".","." }
        };
        private string[,] LETTER_G = { { ".","#","#","." },
                                       { "#",".",".","#" },
                                       { "#",".",".","." },
                                       { "#",".","#","#" },
                                       { "#",".",".","#" },
                                       { ".","#","#","#" }
        };
        private string[,] LETTER_H = { { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { "#","#","#","#" },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" }
        };
        private string[,] LETTER_I = { { ".","#","#","#" },
                                       { ".",".","#","." },
                                       { ".",".","#","." },
                                       { ".",".","#","." },
                                       { ".",".","#","." },
                                       { ".","#","#","#" }
        };
        private string[,] LETTER_J = { { ".",".","#","#" },
                                       { ".",".",".","#" },
                                       { ".",".",".","#" },
                                       { ".",".",".","#" },
                                       { "#",".",".","#" },
                                       { ".","#","#","." }
        };
        private string[,] LETTER_K = { { "#",".",".","#" },
                                       { "#",".","#","." },
                                       { "#","#",".","." },
                                       { "#",".","#","." },
                                       { "#",".","#","." },
                                       { "#",".",".","#" }
        };
        private string[,] LETTER_L = { { "#",".",".","." },
                                       { "#",".",".","." },
                                       { "#",".",".","." },
                                       { "#",".",".","." },
                                       { "#",".",".","." },
                                       { "#","#","#","#" }
        };
        private string[,] LETTER_O = { { ".","#","#","." },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { ".","#","#","." }
        };
        private string[,] LETTER_P = { { "#","#","#","." },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { "#","#","#","." },
                                       { "#",".",".","." },
                                       { "#",".",".","." }
        };
        private string[,] LETTER_R = { { "#","#","#","." },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { "#","#","#","." },
                                       { "#",".","#","." },
                                       { "#",".",".","#" }
        };
        private string[,] LETTER_S = { { ".","#","#","#" },
                                       { "#",".",".","." },
                                       { "#",".",".","." },
                                       { ".","#","#","." },
                                       { ".",".",".","#" },
                                       { "#","#","#","." }
        };
        private string[,] LETTER_U = { { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { "#",".",".","#" },
                                       { ".","#","#","." }
        };
        private string[,] LETTER_Z = { { "#","#","#","#" },
                                       { ".",".",".","#" },
                                       { ".",".","#","." },
                                       { ".","#",".","." },
                                       { "#",".",".","." },
                                       { "#","#","#","#" }
        };
        #endregion

    }
}