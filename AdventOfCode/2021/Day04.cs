using SEGCC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021 {
    internal class Day04 : DayN {
        static int[] mCardDrawn;
        static List<int[,]> mBingoFields;
        const int BINGO_SIZE = 5;
        public override string Part1() {
            mBingoFields = new List<int[,]>();
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week04.txt");
            mCardDrawn = lines[0].Split(',').Select(x => Convert.ToInt32(x)).ToArray();
            int[,] lBingoField = new int[BINGO_SIZE, BINGO_SIZE];
            for (int i = 2, y = 0; i < lines.Length; i++, y++) {
                if (lines[i] == String.Empty) {
                    mBingoFields.Add(lBingoField);
                    lBingoField = new int[BINGO_SIZE, BINGO_SIZE];
                    y = -1;
                    continue;
                }
                int[] lRowOfBingo = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
                for (int x = 0; x < lRowOfBingo.Length; x++) {
                    lBingoField[y, x] = Convert.ToInt32(lRowOfBingo[x]);
                }

            }
            mBingoFields.Add(lBingoField);
            for (int y = 5; y < mCardDrawn.Length; y++) {
                for (int i = 0; i < mBingoFields.Count; i++) {
                    if (IsBingo(mBingoFields[i], y)) {
                        //Console.Write("Week04 - PartA: ");
                        //Console.WriteLine(GetScore(mBingoFields[i], y));
                        return GetScore(mBingoFields[i], y).ToString();
                    }
                }
            }
            return "";
        }

        public override string Part2() {
            int lWinningBoardIndex = -1;
            int lLastCardDrawn = -1;
            bool[] lDoneBoards = new bool[mBingoFields.Count];
            for (int y = 5; y < mCardDrawn.Length; y++) {
                for (int i = 0; i < lDoneBoards.Length; i++) {
                    if (lDoneBoards[i] == true) continue;
                    if (IsBingo(mBingoFields[i], y)) {
                        lDoneBoards[i] = true;
                        lWinningBoardIndex = i;
                        lLastCardDrawn = y;
                    }
                }
            }
            //Console.Write("Week04 - PartB: ");
            //Console.WriteLine(GetScore(mBingoFields[lWinningBoardIndex], lLastCardDrawn));
            return GetScore(mBingoFields[lWinningBoardIndex], lLastCardDrawn).ToString();
        }
        private double GetScore(int[,] aBingoField, int aCardDrawnMaxIndex) {
            double lUnmarkedScore = 0.0;
            for (int x = 0; x < BINGO_SIZE; x++) {
                for (int y = 0; y < BINGO_SIZE; y++) {
                    if (mCardDrawn.Take(aCardDrawnMaxIndex + 1).Any(n => n == aBingoField[y, x])) { } else lUnmarkedScore += aBingoField[y, x];
                }
            }
            return lUnmarkedScore * mCardDrawn[aCardDrawnMaxIndex];
        }

        private bool IsBingo(int[,] aBingoField, int aCardDrawnMaxIndex) {
            for (int y = 0; y < BINGO_SIZE; y++) {
                int lHorizontalBingo = 0;
                for (int x = 0; x < BINGO_SIZE; x++) {
                    if (mCardDrawn.Take(aCardDrawnMaxIndex + 1).Any(n => n == aBingoField[y, x])) lHorizontalBingo++;
                    else continue;
                }
                if (BINGO_SIZE != lHorizontalBingo) continue;
                else return true;
            }
            for (int x = 0; x < BINGO_SIZE; x++) {
                int lVerticalBingo = 0;
                for (int y = 0; y < BINGO_SIZE; y++) {
                    if (mCardDrawn.Take(aCardDrawnMaxIndex + 1).Any(n => n == aBingoField[y, x])) lVerticalBingo++;
                    else continue;
                }
                if (BINGO_SIZE != lVerticalBingo) continue;
                else return true;
            }
            return false;
        }
    }
}
