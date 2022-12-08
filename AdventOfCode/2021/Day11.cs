using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using SEGCC;

namespace AOC2021 {
    internal class Day11 : DayN {
        const int SIZE = 10;
        static int[,] mInput = new int[SIZE,SIZE];
        static int[,] mTemp = new int[SIZE,SIZE];
        const int STEPS_TO_DO = 100;
        static int FLASH_COUNT;
        static int STEPS_COUNT;

        public override string Part1() {
            FLASH_COUNT = 0;
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week11.txt");
            for (int i = 0; i < lInput.Length; i++) {
                for (int j = 0; j < lInput[i].Length; j++) {
                    mInput[i,j] = lInput[i][j] - 48;
                    mTemp[i,j] = lInput[i][j] - 48;
                }
            }
            for (int step = 0; step < STEPS_TO_DO; step++) {
                // increase by 1
                IncreaseEverythingByOne();
                // check for flash
                bool[,] lFlashed = new bool[SIZE,SIZE];
                Flashing(lFlashed);
                //for (int y = 0; y < SIZE; y++) {
                //    Console.WriteLine("");
                //    for (int x = 0; x < SIZE; x++) {
                //        Console.Write(mInput[y, x]);
                //    }
                //}
                //Console.WriteLine("");
                //Console.WriteLine("");
            }
            //Console.Write("Week11 - PartA: ");
            //Console.WriteLine(FLASH_COUNT);
            return FLASH_COUNT.ToString();
        }

        public override string Part2() {
            mInput = mTemp;
            for (int step = 0; step < STEPS_TO_DO * 10; step++) {
                // check if all are flashed
                STEPS_COUNT = step;
                if (CheckIfAllFlashed()) break;
                // increase by 1
                IncreaseEverythingByOne();
                // check for flash
                bool[,] lFlashed = new bool[SIZE, SIZE];
                Flashing(lFlashed);
                //for (int y = 0; y < SIZE; y++) {
                //    Console.WriteLine("");
                //    for (int x = 0; x < SIZE; x++) {
                //        Console.Write(mInput[y, x]);
                //    }
                //}
                //Console.WriteLine("");
                //Console.WriteLine("");
            }
            //Console.Write("Week11 - PartB: ");
            //Console.WriteLine(STEPS_COUNT);
            return STEPS_COUNT.ToString();
        }
        private void IncreaseEverythingByOne() {
            for (int y = 0; y < SIZE; y++) {
                for (int x = 0; x < SIZE; x++) {
                    mInput[y, x]++;
                }
            }
        }

        private void Flashing(bool[,] aFlashed) {
            bool lFlashed = false;
            for (int y = 0; y < SIZE; y++) {
                for (int x = 0; x < SIZE; x++) {
                    if (!aFlashed[y, x]) {
                        if (mInput[y,x] > 9) {
                            mInput[y, x] = 0;
                            aFlashed[y, x] = true;
                            lFlashed = true;
                            FLASH_COUNT++;
                            IncreaseAdjacent(aFlashed, y, x);
                        }
                    }
                }
            }
            if (lFlashed) Flashing(aFlashed);
        }
        
        private void IncreaseAdjacent(bool[,] aFlashed, int aY, int aX) {
            for (int y = aY - 1; y < aY + 2; y++) {
                if (y >= 0 && y < 10) {
                    for (int x = aX - 1; x < aX + 2; x++) {
                        if (x >= 0 && x < 10) {
                            if (!aFlashed[y, x]) {
                                mInput[y, x]++;
                            }
                        }
                    }
                }
            }
        }

        private bool CheckIfAllFlashed() {
            for (int y = 0; y < SIZE; y++) {
                for (int x = 0; x < SIZE; x++) {
                    if (mInput[y, x] != 0) return false;
                }
            }
            return true;
        }
    }
}