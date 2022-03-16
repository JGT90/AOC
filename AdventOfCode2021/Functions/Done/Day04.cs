using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day04 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_04_1.txt";
        List<int> mPulledNumbers;
        List<List<int>> mBoards;

        static List<string> BingoValues = new List<string> {
            "1111100000000000000000000",
            "0000011111000000000000000",
            "0000000000111110000000000",
            "0000000000000001111100000",
            "0000000000000000000011111",
            "0000100001000010000100001",
            "0001000010000100001000010",
            "0010000100001000010000100",
            "0100001000010000100001000",
            "1000010000100001000010000",
        };
        public override void ReadIn() {
            GetPulledNumbers();
            GetBoards();
        }
        public void GetPulledNumbers() {
            mPulledNumbers = new List<int>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return ;
            }
            List<string> lLines = new List<string>();
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                if (lLine == string.Empty) {
                    break;
                }
                lLines.Add(lLine);
            }
            foreach (string lLine in lLines) {
                string[] lValues = lLine.Split(',');
                for (int i = 0; i < lValues.Length; i++) {
                    mPulledNumbers.Add(int.Parse(lValues[i]));
                }
            }
            Console.WriteLine($"{mPulledNumbers.Count} values found in file, {mPath}");
            return ;
        }

        public void GetBoards() {
            mBoards = new List<List<int>>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            List<int> lBoard = new List<int>();
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                if (lLine == string.Empty) {
                    if (lBoard.Count != 0) {
                        mBoards.Add(lBoard);
                    }
                    lBoard = new List<int>();
                    continue;
                } else if (lLine.Contains(',')) {
                    continue;
                }
                string[] lValues = lLine.Split(' ');
                for (int i = 0; i < lValues.Length; i++) {
                    if (lValues[i] == string.Empty) {
                        continue;
                    }
                    lBoard.Add(int.Parse(lValues[i]));
                }
            }
            mBoards.Add(lBoard);
            return;
        }

        public override string DoPartA() {
            int[,] ScoreBoard = new int[5, mBoards.Count * 6];
            int lWinNumber = 0;
            int xBoard = 0;
            int yBoard = 0;
            // Pull one Number after another
            for (int i = 0; i < mPulledNumbers.Count; i++) {
                // Fill ScoreBoard respectivly if number was pulled
                for (int y = 0; y < mBoards.Count; y++) {
                    int lIndex = mBoards[y].IndexOf(mPulledNumbers[i]);
                    if (lIndex > -1) {
                        int xIndex = lIndex % 5;
                        int yIndex = (lIndex - xIndex) / 5 + y * 6;
                        ScoreBoard[xIndex, yIndex] = 1;
                    }
                }

                // Check for 5 in a row
                for (int m = 0; m < mBoards.Count * 6; m++) {
                    int lCount = 0;
                    for (int n = 0; n < 5; n++) {
                        if (ScoreBoard[n, m] == 1) {
                            lCount++;
                        } else {
                            // Reset if they not matching
                            lCount = 0;
                        }
                        if (lCount >= 5) {
                            lWinNumber = i;
                            xBoard = n;
                            yBoard = m;
                            break;
                        }
                    }
                    if (lWinNumber != 0) {
                        break;
                    }
                }
                if (lWinNumber != 0) {
                    break;
                }
                // Check for 5 in a column
                for (int n = 0; n < 5; n++) {
                    int lCount = 0;
                    for (int m = 0; m < mBoards.Count * 6; m++) {
                        if (ScoreBoard[n, m] == 1) {
                            lCount++;
                        } else {
                            // reset if they not matching 
                            lCount = 0;
                        }
                        if (lCount >= 5) {
                            lWinNumber = i;
                            xBoard = n;
                            yBoard = m;
                            break;
                        }
                    }
                    if (lWinNumber != 0) {
                        break;
                    }
                }
                if (lWinNumber != 0) {
                    break;
                }

            }

            int lSumUnmarkedNumber = 0;
            int lBoardIndex = yBoard / 6;
            List<int> MatchingBoard = mBoards[lBoardIndex];
            foreach (int lValue in MatchingBoard) {
                bool lMatch = false;
                for (int h = 0; h < lWinNumber + 1; h++) {
                    if (lValue == mPulledNumbers[h]) {
                        lMatch = true;
                        break;
                    }
                }
                if (!lMatch) {
                    lSumUnmarkedNumber += lValue;
                }
            }
            return $"{lSumUnmarkedNumber * mPulledNumbers[lWinNumber]}";
        }

        public override string DoPartB() {
            int[,] ScoreBoard = new int[5, mBoards.Count * 6];
            int lWinNumber = 0;
            List<int> lWinBoards = new List<int>();
            int xBoard = 0;
            int yBoard = 0;
            // Pull one Number after another
            for (int i = 0; i < mPulledNumbers.Count; i++) {
                // Fill ScoreBoard respectivly if number was pulled
                for (int y = 0; y < mBoards.Count; y++) {
                    // Fill not if already finished
                    if (lWinBoards.Contains(y)) {
                        continue;
                    }
                    int lIndex = mBoards[y].IndexOf(mPulledNumbers[i]);
                    if (lIndex > -1) {
                        int xIndex = lIndex % 5;
                        int yIndex = (lIndex - xIndex) / 5 + y * 6;
                        ScoreBoard[xIndex, yIndex] = 1;
                    }
                }

                // Check for 5 in a row
                for (int m = 0; m < mBoards.Count * 6; m++) {
                    int lCount = 0;
                    for (int n = 0; n < 5; n++) {
                        if (ScoreBoard[n, m] == 1) {
                            lCount++;
                        } else {
                            // Reset if they not matching
                            lCount = 0;
                        }
                        if (lCount >= 5) {
                            if (lWinBoards.Contains(m/6)) {
                                continue;
                            }
                            lWinBoards.Add(m/6);
                            lWinNumber = i;
                            xBoard = n;
                            yBoard = m;
                            lCount = 0;
                            continue;
                        }
                    }
                    //if (lWinNumber != 0) {
                    //    break;
                    //}
                }
                //if (lWinNumber != 0) {
                //    break;
                //}
                // Check for 5 in a column
                for (int n = 0; n < 5; n++) {
                    int lCount = 0;
                    for (int m = 0; m < mBoards.Count * 6; m++) {
                        if (ScoreBoard[n, m] == 1) {
                            lCount++;
                        } else {
                            // reset if they not matching 
                            lCount = 0;
                        }
                        if (lCount >= 5) {
                            if (lWinBoards.Contains(m / 6)) {
                                continue;
                            }
                            lWinBoards.Add(m/6);
                            lWinNumber = i;
                            xBoard = n;
                            yBoard = m;
                            lCount = 0;
                            continue;
                        }
                    }
                    //if (lWinNumber != 0) {
                    //    break;
                    //}
                }
                //if (lWinNumber != 0) {
                //    break;
                //}

            }

            int lSumUnmarkedNumber = 0;
            int lBoardIndex = yBoard / 6;
            List<int> MatchingBoard = mBoards[lBoardIndex];
            foreach (int lValue in MatchingBoard) {
                bool lMatch = false;
                for (int h = 0; h < lWinNumber + 1; h++) {
                    if (lValue == mPulledNumbers[h]) {
                        lMatch = true;
                        break;
                    }
                }
                if (!lMatch) {
                    lSumUnmarkedNumber += lValue;
                }
            }
            return $"{lSumUnmarkedNumber * mPulledNumbers[lWinNumber]}";
        }
    }
}
