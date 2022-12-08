using AdventOfCode;
using System;

namespace AdventOfCode {
    internal class Day20 : DayN {
        int[] mEnhancementLookup;
        int[,] mImage;
        public override string Part1() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week20.txt");
            mEnhancementLookup = new int[lInput[0].Length];
            for (int i = 0; i < lInput[0].Length; i++) {
                mEnhancementLookup[i] = lInput[0][i] == '#' ? 1 : 0;
            }
            int[,] lImage = new int[100, 100];
            for (int y = 2; y < lInput.Length; y++) {
                for (int x = 0; x < lInput[y].Length; x++) {
                    if (lInput[y][x] == '#') lImage[y - 2, x] = 1;
                    else lImage[y - 2, x] = 0;
                }
            }
            mImage = lImage;
            int lBackground = 0;
            for (int i = 0; i < 2; i++) {
                lImage = ExpandImage(lImage, lBackground);
                //PrintImage(lImage);
                lImage = Enhance(lImage, lBackground);
                //PrintImage(lImage);
                if (mEnhancementLookup[0] == 1) {
                    if (i % 2 == 0) lBackground = mEnhancementLookup[0];
                    else lBackground = mEnhancementLookup[mEnhancementLookup.Length - 1];
                }
            }
            //PrintImage(lImage);
            return $"{CountLitPixels(lImage)}";
        }

        public override string Part2() {
            int[,] lImage = mImage;
            int lBackground = 0;
            for (int i = 0; i < 50; i++) {
                lImage = ExpandImage(lImage, lBackground);
                lImage = Enhance(lImage, lBackground);
                if (mEnhancementLookup[0] == 1) {
                    if (i % 2 == 0) lBackground = mEnhancementLookup[0];
                    else lBackground = mEnhancementLookup[mEnhancementLookup.Length - 1];
                }
            }
            return $"{CountLitPixels(lImage)}";
        }

        private void PrintImage(int[,] aImage) {
            int ySize = aImage.GetLength(0);
            int xSize = aImage.GetLength(1);
            Console.WriteLine();
            for (int y = 0; y < ySize; y++) {
                for (int x = 0; x < xSize; x++) {
                    Console.Write(aImage[y, x] == 0 ? "." : "#");
                }
                Console.WriteLine();
            }
        }


        private int[,] Enhance(int[,] aImage, int lBackground) {
            int ySize = aImage.GetLength(0);
            int xSize = aImage.GetLength(1);
            int[,] lResult = new int[ySize, xSize];
            for (int y = 0; y < ySize; y++) {
                for (int x = 0; x < xSize; x++) {
                    lResult[y, x] = mEnhancementLookup[GetEnhancementIndex(y, x, aImage, lBackground)];
                }
            }
            return lResult;
        }

        private int CountLitPixels(int[,] aImage) {
            int ySize = aImage.GetLength(0);
            int xSize = aImage.GetLength(1);
            int lResult = 0;
            for (int y = 0; y < ySize; y++) {
                for (int x = 0; x < xSize; x++) {
                    lResult += aImage[y, x];
                }
            }
            return lResult;
        }

        private int[,] ExpandImage(int[,] aImage, int aBackground) {
            int ySize = aImage.GetLength(0);
            int xSize = aImage.GetLength(1);
            int lOffset = 5;
            int[,] lResult = new int[ySize + 2 * lOffset, xSize + 2 * lOffset];
            for (int y = 0; y < ySize + 2 * lOffset; y++) {
                for (int x = 0; x < xSize + 2* lOffset; x++) {
                    if (y < lOffset || y > ySize - lOffset || x < lOffset || x > xSize - lOffset) lResult[y, x] = aBackground;
                    else continue;
                }
            }
            for (int y = 0; y < ySize; y++) {
                for (int x = 0; x < xSize; x++) {
                    lResult[y + lOffset, x + lOffset] = aImage[y, x];
                }
            }
            return lResult;
        }

        private int GetEnhancementIndex(int aY, int aX, int[,] aImage, int aBackground) {
            int ySize = aImage.GetLength(1);
            int xSize = aImage.GetLength(1);
            int lResult = 0;
            if (aY == 0) {
                lResult += aBackground << 8;
                lResult += aBackground << 7;
                lResult += aBackground << 6;
            } else {
                if (aX == 0) lResult += aBackground << 8;
                else lResult += aImage[aY - 1, aX - 1] << 8;
                lResult += aImage[aY - 1, aX] << 7;
                if (aX == xSize - 1) lResult += aBackground << 6;
                else lResult += aImage[aY - 1, aX + 1] << 6;
            }
            if (aX == 0) {
                lResult += aBackground << 5;
                lResult += aBackground << 2;
            } else {
                lResult += aImage[aY, aX - 1] << 5;
                if (aY == ySize - 1) lResult += aBackground << 2;
                else lResult += aImage[aY + 1, aX - 1] << 2;
            }
            if (aY == ySize - 1) {
                lResult += aBackground << 1;
                lResult += aBackground << 0;
            } else {
                if (aX == xSize - 1) lResult += aBackground << 0;
                else lResult += aImage[aY + 1, aX + 1] << 0;
                lResult += aImage[aY + 1, aX] << 1;
            }
            if (aX == xSize - 1) {
                lResult += aBackground << 3;
            } else {
                lResult += aImage[aY, aX + 1] << 3;
            }
            lResult += aImage[aY, aX] << 4;
            return lResult;
        }
    }
}