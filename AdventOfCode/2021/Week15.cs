using SEGCC;
using System;
using System.Collections.Generic;

namespace AOC2021 {
    internal class Week15 : DayN {
        const int START_INDEX = 0;
        int Y_SIZE;
        int X_SIZE;
        int AREA_SIZE;
        int[,] mInput;
        public override string Part1() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week15.txt");

            Y_SIZE = lInput.Length;
            X_SIZE = lInput[0].Length;
            AREA_SIZE = Y_SIZE * X_SIZE;
            mInput = new int[X_SIZE, Y_SIZE];
            for (int y = 0; y < Y_SIZE; y++) {
                for (int x = 0; x < X_SIZE; x++) {
                    mInput[y, x] = lInput[y][x] - 48;
                }
            }

            return $"{GetLowestRiskLevel()}";
        }

        public override string Part2() {
            int xOld = X_SIZE;
            int yOld = Y_SIZE;
            Y_SIZE = 5 * Y_SIZE;
            X_SIZE = 5 * X_SIZE;
            AREA_SIZE = Y_SIZE * X_SIZE;
            int[,] lTemp = new int[X_SIZE, Y_SIZE];
            for (int y = 0; y < Y_SIZE; y++) {
                for (int x = 0; x < X_SIZE; x++) {
                    int xAdd = x / xOld;
                    int yAdd = y / yOld;
                    int lNewValue = mInput[x % xOld,y % yOld] + xAdd + yAdd;
                    lTemp[x,y] = lNewValue > 9 ? lNewValue % 10 + 1: lNewValue;
                }
            }
            mInput = lTemp;
            return $"{GetLowestRiskLevel()}";
        }

        private int GetLowestRiskLevel() {
            int lIndex;
            int[] lRiskLevel = new int[AREA_SIZE];
            for (int i = 0; i < AREA_SIZE; i++) {
                lRiskLevel[i] = int.MaxValue;
            }
            Queue<int> lQueue = new Queue<int>();
            lQueue.Enqueue(START_INDEX);
            lRiskLevel[START_INDEX] = 0;
            while (lQueue.Count > 0) {
                lIndex = lQueue.Peek();
                lQueue.Dequeue();
                int lRow = (lIndex) / Y_SIZE;
                int lColumn = (lIndex - lRow * Y_SIZE) % X_SIZE;
                if (lColumn > 0) {
                    int lNewRow = (lIndex - 1) / Y_SIZE;
                    int lNewColumn = (lIndex - 1 - lNewRow * Y_SIZE) % X_SIZE;
                    int lNewRiskLevel = lRiskLevel[lIndex] + mInput[lNewColumn, lNewRow];
                    if (lRiskLevel[lIndex - 1] > lNewRiskLevel) {
                        lRiskLevel[lIndex - 1] = lNewRiskLevel;
                        lQueue.Enqueue(lIndex - 1);
                    }
                }
                if (lColumn < X_SIZE - 1) {
                    int lNewRow = (lIndex + 1) / Y_SIZE;
                    int lNewColumn = (lIndex + 1 - lNewRow * Y_SIZE) % X_SIZE;
                    int lNewRiskLevel = lRiskLevel[lIndex] + mInput[lNewColumn, lNewRow];

                    if (lRiskLevel[lIndex + 1] > lNewRiskLevel) {
                        lRiskLevel[lIndex + 1] = lNewRiskLevel;
                        lQueue.Enqueue(lIndex + 1);
                    }
                }
                if (lRow > 0) {
                    int lNewRow = (lIndex - X_SIZE) / Y_SIZE;
                    int lNewColumn = (lIndex - X_SIZE - lNewRow * Y_SIZE) % X_SIZE;
                    int lNewRiskLevel = lRiskLevel[lIndex] + mInput[lNewColumn, lNewRow];

                    if (lRiskLevel[lIndex - X_SIZE] > lNewRiskLevel) {
                        lRiskLevel[lIndex - X_SIZE] = lNewRiskLevel;
                        lQueue.Enqueue(lIndex - X_SIZE);
                    }
                }
                if (lRow < X_SIZE - 1) {
                    int lNewRow = (lIndex + X_SIZE) / Y_SIZE;
                    int lNewColumn = (lIndex + X_SIZE - lNewRow * Y_SIZE) % X_SIZE;
                    int lNewRiskLevel = lRiskLevel[lIndex] + mInput[lNewColumn, lNewRow];

                    if (lRiskLevel[lIndex + X_SIZE] > lNewRiskLevel) {
                        lRiskLevel[lIndex + X_SIZE] = lNewRiskLevel;
                        lQueue.Enqueue(lIndex + X_SIZE);
                    }
                }
            }
            return lRiskLevel[AREA_SIZE - 1];
        }
    }
}