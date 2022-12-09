using System.IO;

namespace AdventOfCode.Year2022 {
    class Day08 : DayN_2022 {
        #region Fields
        private int[,] _TreeGrid;
        #endregion

        #region Constructor
        public Day08() {
            AddInputData(@"Day08-JGT90.txt");
        }
        #endregion

        #region Properties
        protected override string PuzzleName => "Treetop Tree House";
        protected override int Day => 8;
        protected override int Year => 2022;
        #endregion

        #region Methods
        private bool IsVisible(int yTree, int xTree, int[,] Grid, int GridLength, int GridHeight) {
            bool IsVisible = true;
            int Height = Grid[yTree, xTree];
            for (int x = xTree - 1; x >= 0; x--) {
                if (Grid[yTree, x] >= Height) IsVisible = false;
            }
            if (IsVisible) return IsVisible;
            IsVisible = true;
            for (int x = xTree + 1; x < GridLength; x++) {
                if (Grid[yTree, x] >= Height) IsVisible = false;
            }
            if (IsVisible) return IsVisible;
            IsVisible = true;
            for (int y = yTree - 1; y >= 0; y--) {
                if (Grid[y, xTree] >= Height) IsVisible = false;
            }
            if (IsVisible) return IsVisible;
            IsVisible = true;
            for (int y = yTree + 1; y < GridHeight; y++) {
                if (Grid[y, xTree] >= Height) IsVisible = false;
            }
            return IsVisible;
        }

        private int ScenicScore(int yTree, int xTree, int[,] Grid, int GridLength, int GridHeight) {
            int Height = Grid[yTree, xTree];
            bool IsVisible = true;
            int LeftScore = 0;
            for (int x = xTree - 1; x >= 0; x--) {
                if (Grid[yTree, x] >= Height) IsVisible = false;
                LeftScore++;
                if (!IsVisible) break;
            }
            IsVisible = true;
            int RightScore = 0;
            for (int x = xTree + 1; x < GridLength; x++) {
                if (Grid[yTree, x] >= Height) IsVisible = false;
                RightScore++;
                if (!IsVisible) break;
            }
            IsVisible = true;
            int TopScore = 0;
            for (int y = yTree - 1; y >= 0; y--) {
                if (Grid[y, xTree] >= Height) IsVisible = false;
                TopScore++;
                if (!IsVisible) break;
            }
            IsVisible = true;
            int DownScore = 0;
            for (int y = yTree + 1; y < GridHeight; y++) {
                if (Grid[y, xTree] >= Height) IsVisible = false;
                DownScore++;
                if (!IsVisible) break;
            }
            return LeftScore * RightScore * TopScore * DownScore;
        }
        #endregion

        #region Functions

        public override string SolvePartOne() {
            _TreeGrid = new int[RawData.Length, RawData[0].Length];
            for (int y = 0; y < RawData.Length; y++) {
                for (int x = 0; x < RawData[y].Length; x++) {
                    _TreeGrid[y, x] = int.Parse(RawData[y][x].ToString());
                }
            }
            int lVisible = RawData.Length * 2 + RawData[0].Length * 2 - 4;
            for (int y = 1; y < RawData.Length - 1; y++) {
                for (int x = 1; x < RawData[y].Length - 1; x++) {
                    if (IsVisible(y, x, _TreeGrid, RawData[0].Length, RawData.Length)) lVisible++;
                }
            }

            return lVisible.ToString();
        }

        public override string SolvePartTwo() {
            int Length = _TreeGrid.GetLength(0);
            int Height = _TreeGrid.GetLength(1);
            int[,] ScoreGrid = new int[Height, Length];
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Length; x++) {
                    ScoreGrid[y, x] = ScenicScore(y, x, _TreeGrid, Length, Height);
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
        #endregion
    }
}
