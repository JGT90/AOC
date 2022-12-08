﻿using System.IO;

namespace AdventOfCode.Year2022 {
    class Day08 {
        int[,] TreeGrid;
        public string DoPartA() {
            string lPath = @"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode2022\Input\Day08.txt";
            string[] lLines = File.ReadAllLines(lPath);
            TreeGrid = new int[lLines.Length,lLines[0].Length];
            for(int y = 0; y < lLines.Length;y++) {
                for (int x = 0; x < lLines[y].Length; x++) {
                    TreeGrid[y, x] = int.Parse(lLines[y][x].ToString());
                }
            }
            int lVisible = lLines.Length * 2 + lLines[0].Length*2 - 4;
            for (int y = 1; y < lLines.Length-1; y++) {
                for (int x = 1; x < lLines[y].Length-1; x++) {
                    if (IsVisible(y, x, TreeGrid, lLines[0].Length, lLines.Length)) lVisible++;
                }
            }

            return lVisible.ToString();
        }

        private bool IsVisible(int yTree, int xTree, int[,] Grid, int GridLength, int GridHeight) {
            bool IsVisible = true;
            int Height = Grid[yTree, xTree];
            for(int x = xTree-1; x >= 0; x--) {
                if (Grid[yTree, x] >= Height) IsVisible = false;
            }
            if (IsVisible) return IsVisible;
            IsVisible = true;
            for (int x = xTree+1; x < GridLength; x++) {
                if (Grid[yTree, x] >= Height) IsVisible = false;
            }
            if (IsVisible) return IsVisible;
            IsVisible = true;
            for (int y = yTree-1; y >= 0; y--) {
                if (Grid[y, xTree] >= Height) IsVisible = false;
            }
            if (IsVisible) return IsVisible;
            IsVisible = true;
            for (int y = yTree+1; y < GridHeight; y++) {
                if (Grid[y, xTree] >= Height) IsVisible = false;
            }
            return IsVisible;
        }

        public string DoPartB() {
            int Length = TreeGrid.GetLength(0);
            int Height = TreeGrid.GetLength(1);
            int[,] ScoreGrid = new int[Height, Length];
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Length; x++) {
                    ScoreGrid[y, x] = ScenicScore(y, x, TreeGrid, Length, Height);
                }
            }
            int HighestScenicScore = int.MinValue;
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Length; x++) {
                    if (ScoreGrid[y, x] > HighestScenicScore) HighestScenicScore = ScoreGrid[y, x];
                }
            }
            return HighestScenicScore.ToString();
        }
        private int ScenicScore(int yTree, int xTree, int[,] Grid, int GridLength, int GridHeight) {
            int Height = Grid[yTree, xTree];
            bool IsVisible = true;
            int LeftScore = 0;
            //if (xTree != 0) LeftScore++;
            for (int x = xTree - 1; x >= 0; x--) {
                if (Grid[yTree, x] >= Height) IsVisible = false;
                LeftScore++;
                if (!IsVisible) break;
            }
            IsVisible = true;
            int RightScore = 0;
            //if (xTree != GridLength) RightScore++;
            for (int x = xTree + 1; x < GridLength; x++) {
                if (Grid[yTree, x] >= Height) IsVisible = false;
                RightScore++;
                if (!IsVisible) break;
            }
            IsVisible = true;
            int TopScore = 0;
            //if (yTree != 0) TopScore++;
            for (int y = yTree - 1; y >= 0; y--) {
                if (Grid[y, xTree] >= Height) IsVisible = false;
                TopScore++;
                if (!IsVisible) break;
            }
            IsVisible = true;
            int DownScore = 0;
            //if (yTree != GridHeight) DownScore++;
            for (int y = yTree + 1; y < GridHeight; y++) {
                if (Grid[y, xTree] >= Height) IsVisible = false;
                DownScore++;
                if (!IsVisible) break;
            }
            return LeftScore * RightScore * TopScore * DownScore;
        }
    }
}
