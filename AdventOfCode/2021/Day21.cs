using SEGCC;

namespace AOC2021 {
    internal class Day21 : DayN {
        private double Player1WinCount;
        private double Player2WinCount;
        private int[] UniverseTimes = new int[] { 1, 3, 6, 7, 6, 3, 1 };
        private int[] Move = new int[] { 3, 4, 5, 6, 7, 8, 9 };

        public override string Part1() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week21.txt");
            int lStartingPointPlayer1 = int.Parse(lInput[0].Split(new char[] { ':' })[1].Trim());
            int lStartingPointPlayer2 = int.Parse(lInput[1].Split(new char[] { ':' })[1].Trim());
            int lScorePointsPlayer1 = 0;
            int lScorePointsPlayer2 = 0;
            int lRoundOfGames = 0;
            while (lScorePointsPlayer1 < 1000 && lScorePointsPlayer2 < 1000) {
                lRoundOfGames++;
                if (lRoundOfGames % 2 == 1) {
                    GetPositionDeterministic(lRoundOfGames, ref lStartingPointPlayer1);
                    lScorePointsPlayer1 += lStartingPointPlayer1;
                } else {
                    GetPositionDeterministic(lRoundOfGames, ref lStartingPointPlayer2);
                    lScorePointsPlayer2 += lStartingPointPlayer2;
                }
            }
            if (lScorePointsPlayer1 < lScorePointsPlayer2) return $"{lScorePointsPlayer1 * lRoundOfGames * 3}";
            else return $"{lScorePointsPlayer2 * lRoundOfGames * 3}";
        }

        public override string Part2() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week21.txt");
            int lStartingPointPlayer1 = int.Parse(lInput[0].Split(new char[] { ':' })[1].Trim());
            int lStartingPointPlayer2 = int.Parse(lInput[1].Split(new char[] { ':' })[1].Trim());
            Player1WinCount = 0;
            Player2WinCount = 0;
            PlayDiracDice(true, 1, lStartingPointPlayer1, lStartingPointPlayer2, 0, 0);
            if (Player1WinCount > Player2WinCount) return $"{Player1WinCount}";
            else return $"{Player2WinCount}";
        }
        private void PlayDiracDice(bool aPlayer1Turn, double aUniverseCount, int aPosition1, int aPosition2, int aScore1, int aScore2) {
            if (aScore1 < 21 && aScore2 < 21) {
                for (int i = 0; i < 7; i++) {
                    if (aPlayer1Turn) {
                        int lPosition = GetPosition(aPosition1, Move[i]);
                        PlayDiracDice(!aPlayer1Turn, aUniverseCount * UniverseTimes[i], lPosition, aPosition2, aScore1 + lPosition, aScore2);
                    } else {
                        int lPosition = GetPosition(aPosition2, Move[i]);
                        PlayDiracDice(!aPlayer1Turn, aUniverseCount * UniverseTimes[i], aPosition1, lPosition, aScore1, aScore2 + lPosition);
                    }
                }
            } else {
                if (aScore1 >= 21) Player1WinCount += aUniverseCount;
                else Player2WinCount += aUniverseCount;
            }
        }
        private int GetPosition(int aPosition, int aMove) {
            aPosition = (aPosition + aMove) % 10;
            if (aPosition == 0) return 10;
            return aPosition;
        }

        private void GetPositionDeterministic(int aRoundOfGames, ref int aStartingPoint) {
            int lDiceSum = 0;
            int lNextRollingDie = (aRoundOfGames - 1) * 3;
            for (int i = 0; i < 3; i++) {
                lNextRollingDie = (lNextRollingDie + 1) % 100;
                if (lNextRollingDie == 0) lDiceSum += 100;
                else lDiceSum += lNextRollingDie;
            }
            int lEndPoint = (aStartingPoint + lDiceSum) % 10;
            if (lEndPoint == 0) {
                aStartingPoint = 10;
                return;
            }
            aStartingPoint = lEndPoint;
        }


    }
}