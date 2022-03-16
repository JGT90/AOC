using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day21 : AdventOfCode.AdventOfCode {
        public override void ReadIn() { }
        public override string DoPartA() {
            return $"{PlayDeterministicDice(6, 9, 1000)}";
        }

        public override string DoPartB() {
            PlayDiracDice(6, 9, 21);
            if (PlayerAWon > PlayerBWon) return $"{PlayerAWon}";
            return $"{PlayerBWon}";
        }

        private double PlayDeterministicDice(int aPositionA, int aPositionB, int aScoreLimit) {
            int index = 0;
            int ScoreA = 0;
            int ScoreB = 0;
            while (ScoreA < aScoreLimit && ScoreB < aScoreLimit) {
                int DiceScore = 0;
                for (int i = index * 3 + 1; i < index * 3 + 3 + 1; i++) {
                    DiceScore += i;
                }
                if (index % 2 == 0) {
                    // Player1
                    aPositionA = (aPositionA + DiceScore) % 10;
                    ScoreA += aPositionA == 0 ? 10 : aPositionA;
                } else {
                    // Player2
                    aPositionB = (aPositionB + DiceScore) % 10;
                    ScoreB += aPositionB == 0 ? 10 : aPositionB;
                }
                index++;
            }
            return ScoreA < ScoreB ? ScoreA * index * 3 : ScoreB * index * 3;
        }
        public static double PlayerAWon;
        public static double PlayerBWon;
        private int[,] UpdatePlayer(int[,] Player, bool PlayerA, int Limit, out bool Done, out int Updated) {
            int x = Player.GetLength(0);
            int[,] NewPlayer = new int[x * 7, 6];
            Updated = 0;
            for (int i = 0; i < x; i++) {
                if (Player[i, 0] >= Limit || Player[i, 3] >= Limit) continue;
                for (int y = 0; y < 7; y++) {
                    int Move = 0;
                    int Factor = 0;
                    switch (y) {
                        case 0:
                            Move = 3;
                            Factor = 1;
                            break;
                        case 1:
                            Move = 9;
                            Factor = 1;
                            break;
                        case 2:
                            Move = 4;
                            Factor = 3;
                            break;
                        case 3:
                            Move = 8;
                            Factor = 3;
                            break;
                        case 4:
                            Move = 5;
                            Factor = 6;
                            break;
                        case 5:
                            Move = 7;
                            Factor = 6;
                            break;
                        case 6:
                            Move = 6;
                            Factor = 7;
                            break;
                    }
                    if (Player[i, 0] == 0 && Player[i, 0] == Player[i, 1] && Player[i, 1] == Player[i, 2] && Player[i, 2] == Player[i, 3] && Player[i, 3] == Player[i, 4] && Player[i, 4] == Player[i, 5]) continue;
                    for (int n = 0; n < 6; n++) {
                        NewPlayer[i * 7 + y, n] = Player[i, n];
                    }
                    Updated++;
                    int offset = 0;
                    if (!PlayerA) offset = 3;
                    int Score = Player[i, offset + 0];
                    int Count = Player[i, offset + 1];
                    int Position = (Player[i, offset + 2] + Move) % 10;
                    NewPlayer[i * 7 + y, offset + 2] = Position == 0 ? 10 : Position;
                    NewPlayer[i * 7 + y, 4] = Count * Factor;
                    NewPlayer[i * 7 + y, 1] = Count * Factor;
                    NewPlayer[i * 7 + y, offset + 0] = Score + NewPlayer[i * 7 + y, offset + 2];
                    if (NewPlayer[i * 7 + y, offset + 0] >= Limit) {
                        if (PlayerA) PlayerAWon += NewPlayer[i * 7 + y, offset + 1];
                        else PlayerBWon += NewPlayer[i * 7 + y, offset + 1];
                        for (int m = 0; m < 6; m++) {
                            NewPlayer[i * 7 + y, m] = 0;
                        }
                        Updated--;
                    }
                }
            }
            if (Updated == 0) Done = true;
            else Done = false;
            return NewPlayer;
        }

        private void PlayDiracDice(int[,] Player, int Limit) {
            bool Done = false;
            bool ATurn = true;
            while (!Done) {
                int[,] TempPlayer = UpdatePlayer(Player, ATurn, Limit, out Done, out int entries);
                int length = TempPlayer.GetLength(0);
                int[,] NewPlayer = new int[entries, 6];
                int index = 0;
                for (int i = 0; i < length; i++) {
                    if (TempPlayer[i, 0] == 0 && TempPlayer[i, 0] == TempPlayer[i, 1] && TempPlayer[i, 1] == TempPlayer[i, 2] && TempPlayer[i, 2] == TempPlayer[i, 3] && TempPlayer[i, 3] == TempPlayer[i, 4] && TempPlayer[i, 4] == TempPlayer[i, 5]) continue;
                    for (int n = 0; n < 6; n++) {
                        NewPlayer[index, n] = TempPlayer[i, n];
                    }
                    index++;
                }
                Player = NewPlayer;
                ATurn = !ATurn;
            }
        }
        static Dictionary<int, int> oddsForNext3Throws;
        private void PlayDiracDice(int PlayerAPosition, int PlayerBPosition, int Limit) {
            oddsForNext3Throws = new Dictionary<int, int>();
            oddsForNext3Throws.Add(3, 1);
            oddsForNext3Throws.Add(9, 1);
            oddsForNext3Throws.Add(4, 3);
            oddsForNext3Throws.Add(8, 3);
            oddsForNext3Throws.Add(5, 6);
            oddsForNext3Throws.Add(7, 6);
            oddsForNext3Throws.Add(6, 7);
            UpdatePlayer2(PlayerAPosition, PlayerBPosition, 0, 0, 1, true, 21);
        }

        private void UpdatePlayer2(int PlayerAPosition, int PlayerBPosition, int PlayerAScore, int PlayerBScore, double universes, bool PlayerA, int Limit) {
            foreach (int nextSteps in oddsForNext3Throws.Keys) {
                if (PlayerA) {
                    int landOnTile = (PlayerAPosition + nextSteps) % 10;
                    if (landOnTile == 0) landOnTile = 10;
                    int score = PlayerAScore + landOnTile;
                    double probability = universes * oddsForNext3Throws[nextSteps];
                    if (score >= Limit)
                        PlayerAWon += probability;
                    else
                        UpdatePlayer2(landOnTile, PlayerBPosition, score, PlayerBScore, probability, false, Limit);
                } else {
                    int landOnTile = (PlayerBPosition + nextSteps) % 10;
                    if (landOnTile == 0) landOnTile = 10;
                    int score = PlayerBScore + landOnTile;
                    double probability = universes * oddsForNext3Throws[nextSteps];
                    if (score >= Limit)
                        PlayerBWon += probability;
                    else
                        UpdatePlayer2(PlayerAPosition, landOnTile, PlayerAScore, score, probability, true, Limit);
                }
            }
        }
    }


}

