using System;

namespace AdventOfCode.Year2022 {
    class Day09 : DayN_2022 {
        #region Fields
        private const int SIZE = 1000;
        #endregion

        #region Constructor
        public Day09() {
            AddInputData(@"Day09-Example01.txt");
            AddInputData(@"Day09-Example02.txt");
        }
        #endregion

        #region Properties
        protected override string PuzzleName => "Rope Bridge";
        protected override int Day => 9;
        protected override int Year => 2022;
        #endregion

        #region Methods
        #endregion

        #region Functions

        public override string SolvePartOne() {
            bool[,] _visited = new bool[SIZE, SIZE];
            int[] _headPosition = new int[2] { SIZE / 2, SIZE / 2 };
            int[] _tailPosition = new int[2] { SIZE / 2, SIZE / 2 };
            foreach (string _line in RawData) {
                string[] _split = _line.Split(' ');
                int _movements = int.Parse(_split[1]);
                int x = 0;
                int y = 0;
                if (_split[0] == "U") y = -1;
                else if (_split[0] == "D") y = 1;
                else if (_split[0] == "R") x = 1;
                else if (_split[0] == "L") x = -1;
                for (int i = 0; i < _movements; i++) {
                    _visited[_tailPosition[0], _tailPosition[1]] = true;
                    _headPosition[0] += x;
                    _headPosition[1] += y;
                    if (_headPosition[0] == _tailPosition[0]) {
                        if (Math.Abs(_headPosition[1] - _tailPosition[1]) == 2) _tailPosition[1] += y;
                    } else if (_headPosition[1] == _tailPosition[1]) {
                        if (Math.Abs(_headPosition[0] - _tailPosition[0]) == 2) _tailPosition[0] += x;
                    } else {
                        if (_headPosition[1] - _tailPosition[1] > 1) {
                            _tailPosition[1] += 1;
                            if (_headPosition[0] - _tailPosition[0] > 0) _tailPosition[0] += 1;
                            else if (_headPosition[0] - _tailPosition[0] < 0) _tailPosition[0] -= 1;
                        } else if (Math.Abs(_headPosition[1] - _tailPosition[1]) > 1) {
                            _tailPosition[1] -= 1;
                            if (_headPosition[0] - _tailPosition[0] > 0) _tailPosition[0] += 1;
                            else if (_headPosition[0] - _tailPosition[0] < 0) _tailPosition[0] -= 1;
                        } else if (_headPosition[0] - _tailPosition[0] > 1) {
                            _tailPosition[0] += 1;
                            if (_headPosition[1] - _tailPosition[1] > 0) _tailPosition[1] += 1;
                            else if (_headPosition[1] - _tailPosition[1] < 0) _tailPosition[1] -= 1;
                        } else if (Math.Abs(_headPosition[0] - _tailPosition[0]) > 1) {
                            _tailPosition[0] -= 1;
                            if (_headPosition[1] - _tailPosition[1] > 0) _tailPosition[1] += 1;
                            else if (_headPosition[1] - _tailPosition[1] < 0) _tailPosition[1] -= 1;
                        }
                    }
                }
            }
            int _visitedCount = 0;
            for (int x = 0; x < SIZE; x++) {
                for (int y = 0; y < SIZE; y++) {
                    if (_visited[x, y]) _visitedCount++;
                }
            }
            return _visitedCount.ToString();
        }

        public override string SolvePartTwo() {
            int _TailSize = 10;
            bool[,] _visited = new bool[SIZE, SIZE];
            int[][] _position = new int[_TailSize][];
            for (int i = 0; i < _TailSize; i++)
                _position[i] = new int[2] { SIZE / 2, SIZE / 2 };
            foreach (string _line in RawData) {
                string[] _split = _line.Split(' ');
                int _movements = int.Parse(_split[1]);
                int x = 0;
                int y = 0;
                if (_split[0] == "U") y = -1;
                else if (_split[0] == "D") y = 1;
                else if (_split[0] == "R") x = 1;
                else if (_split[0] == "L") x = -1;
                for (int i = 0; i < _movements; i++) {
                    _position[0][0] += x;
                    _position[0][1] += y;
                    for (int j = 0; j < _TailSize - 1; j++) {
                        if (_position[j][0] == _position[j + 1][0]) {
                            if (_position[j][1] - _position[j + 1][1] > 1) _position[j + 1][1] += 1;
                            else if (_position[j][1] - _position[j + 1][1] < -1) _position[j + 1][1] -= 1;
                        } else if (_position[j][1] == _position[j + 1][1]) {
                            if (_position[j][0] - _position[j + 1][0] > 1) _position[j + 1][0] += 1;
                            else if (_position[j][0] - _position[j + 1][0] < -1) _position[j + 1][0] -= 1;
                        } else {
                            if ((_position[j][0] - _position[j + 1][0] < 0 && _position[j][1] - _position[j + 1][1] > 1) || (_position[j][0] - _position[j + 1][0] < -1 && _position[j][1] - _position[j + 1][1] > 0)) {
                                _position[j + 1][0]--;
                                _position[j + 1][1]++;
                            } else if ((_position[j][0] - _position[j + 1][0] < 0 && _position[j][1] - _position[j + 1][1] < -1) || (_position[j][0] - _position[j + 1][0] < -1 && _position[j][1] - _position[j + 1][1] < 0)) {
                                _position[j + 1][0]--;
                                _position[j + 1][1]--;
                            } else if ((_position[j][0] - _position[j + 1][0] > 0 && _position[j][1] - _position[j + 1][1] < -1) || (_position[j][0] - _position[j + 1][0] > 1 && _position[j][1] - _position[j + 1][1] < 0)) {
                                _position[j + 1][0]++;
                                _position[j + 1][1]--;
                            } else if ((_position[j][0] - _position[j + 1][0] > 0 && _position[j][1] - _position[j + 1][1] > 1) || (_position[j][0] - _position[j + 1][0] > 1 && _position[j][1] - _position[j + 1][1] > 0)) {
                                _position[j + 1][0]++;
                                _position[j + 1][1]++;
                            }
                        }
                    }
                    _visited[_position[_TailSize - 1][0], _position[_TailSize - 1][1]] = true;

                }
            }
            int _visitedCount = 0;
            for (int x = 0; x < SIZE; x++) {
                for (int y = 0; y < SIZE; y++) {
                    if (_visited[x, y]) _visitedCount++;
                }
            }
            //for (int y = 490; y < 510; y++) {
            //    for (int x = 480; x < 520; x++) {
            //        Console.Write(_visited[x, y] ? "X" : ".");
            //    }
            //    Console.WriteLine();
            //}
            return _visitedCount.ToString();
        }
        #endregion
    }
}
