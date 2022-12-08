using System.Collections.Generic;

namespace AdventOfCode.Year2021 {
    internal class Day10 : DayN {
        static string[] mInput;
        const char OPEN_ROUND_BRACKET = '(';
        const char CLOSE_ROUND_BRACKET = ')';
        const char OPEN_SQUARE_BRACKET = '[';
        const char CLOSE_SQUARE_BRACKET = ']';
        const char OPEN_CURLY_BRACKET = '{';
        const char CLOSE_CURLY_BRACKET = '}';
        const char OPEN_ANGLE_BRACKET = '<';
        const char CLOSE_ANGLE_BRACKET = '>';

        public override string Part1() {
            mInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week10.txt");
            double lCorruptedScore = 0.0;
            for (int i = 0; i < mInput.Length; i++) {
                List<char> lLastOpen = new List<char>();
                for (int j = 0; j < mInput[i].Length; j++) {
                    if (mInput[i][j] == OPEN_ROUND_BRACKET || mInput[i][j] == OPEN_SQUARE_BRACKET || mInput[i][j] == OPEN_CURLY_BRACKET || mInput[i][j] == OPEN_ANGLE_BRACKET) {
                        lLastOpen.Add(mInput[i][j]);
                        continue;
                    }
                    if (lLastOpen[lLastOpen.Count - 1] == OPEN_ROUND_BRACKET && mInput[i][j] != CLOSE_ROUND_BRACKET ||
                        lLastOpen[lLastOpen.Count - 1] == OPEN_SQUARE_BRACKET && mInput[i][j] != CLOSE_SQUARE_BRACKET ||
                        lLastOpen[lLastOpen.Count - 1] == OPEN_CURLY_BRACKET && mInput[i][j] != CLOSE_CURLY_BRACKET ||
                        lLastOpen[lLastOpen.Count - 1] == OPEN_ANGLE_BRACKET && mInput[i][j] != CLOSE_ANGLE_BRACKET) {
                        lCorruptedScore += GetSyntaxScore(mInput[i][j]);
                        lLastOpen.RemoveAt(lLastOpen.Count - 1);
                        break;
                    }
                    lLastOpen.RemoveAt(lLastOpen.Count - 1);
                }
            }
            //Console.Write("Week10 - PartA: ");
            //Console.WriteLine(lCorruptedScore);
            return lCorruptedScore.ToString();
        }

        public override string Part2() {
            List<double> lAutotestScore = new List<double>();
            int b = 0;
            for (int i = 0; i < mInput.Length; i++) {
                List<char> lLastOpen = new List<char>();
                bool lCorrupted = false;
                for (int j = 0; j < mInput[i].Length; j++) {
                    if (mInput[i][j] == OPEN_ROUND_BRACKET || mInput[i][j] == OPEN_SQUARE_BRACKET || mInput[i][j] == OPEN_CURLY_BRACKET || mInput[i][j] == OPEN_ANGLE_BRACKET) {
                        lLastOpen.Add(mInput[i][j]);
                        continue;
                    }
                    if (lLastOpen[lLastOpen.Count - 1] == OPEN_ROUND_BRACKET && mInput[i][j] != CLOSE_ROUND_BRACKET ||
                        lLastOpen[lLastOpen.Count - 1] == OPEN_SQUARE_BRACKET && mInput[i][j] != CLOSE_SQUARE_BRACKET ||
                        lLastOpen[lLastOpen.Count - 1] == OPEN_CURLY_BRACKET && mInput[i][j] != CLOSE_CURLY_BRACKET ||
                        lLastOpen[lLastOpen.Count - 1] == OPEN_ANGLE_BRACKET && mInput[i][j] != CLOSE_ANGLE_BRACKET) {
                        // Corrupted ignore them
                        lCorrupted = true;
                        b++;
                        break;
                    }
                    lLastOpen.RemoveAt(lLastOpen.Count - 1);
                }
                if (!lCorrupted) {
                    lAutotestScore.Add(GetAutocompleteScore(lLastOpen));
                }
            }
            lAutotestScore.Sort();
            //Console.Write("Week10 - PartB: ");
            //Console.WriteLine(lAutotestScore[lAutotestScore.Count / 2]);
            return lAutotestScore[lAutotestScore.Count / 2].ToString();
        }

        private double GetSyntaxScore(char aCharacter) {
            if (aCharacter == CLOSE_ROUND_BRACKET) return 3;
            if (aCharacter == CLOSE_SQUARE_BRACKET) return 57;
            if (aCharacter == CLOSE_CURLY_BRACKET) return 1197;
            if (aCharacter == CLOSE_ANGLE_BRACKET) return 25137;
            return 0;
        }

        private double GetAutocompleteScore(List<char> aLeftover) {
            double lScore = 0.0;
            for (int i = aLeftover.Count - 1; i >= 0; i--) {
                lScore = 5 * lScore + GetCharValue(aLeftover[i]);
            }
            return lScore;
        }

        private double GetCharValue(char aCharacter) {
            if (aCharacter == OPEN_ROUND_BRACKET) return 1;
            if (aCharacter == OPEN_SQUARE_BRACKET) return 2;
            if (aCharacter == OPEN_CURLY_BRACKET) return 3;
            if (aCharacter == OPEN_ANGLE_BRACKET) return 4;
            return 0;
        }
    }
}