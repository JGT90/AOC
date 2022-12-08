using SEGCC;
using System;
using System.Collections.Generic;

namespace AOC2021 {
    internal class Week19 : DayN {
        List<Scanner> mScanners = new List<Scanner>();
        public override string Part1() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week19.txt");
            int lBeaconCount = 0;
            Beacon[] lTemp = new Beacon[30];
            for (int i = 0; i < lInput.Length; i++) {
                if (lInput[i] == "") continue;
                if (lInput[i].StartsWith("---")) {
                    if (lBeaconCount != 0) {
                        mScanners.Add(new Scanner(lBeaconCount, lTemp));
                    }
                    lBeaconCount = 0;
                } else {
                    string[] lSplit = lInput[i].Split(',');
                    lTemp[lBeaconCount] = new Beacon(int.Parse(lSplit[0]), int.Parse(lSplit[1]), int.Parse(lSplit[2]));
                    lBeaconCount++;
                }
            }
            CreateAllRotations();
            mScanners.Add(new Scanner(lBeaconCount, lTemp));
            mScanners[0].AbsoluteLocation = new int[3];
            foreach (Beacon lBeacon in mScanners[0].Beacons) {
                lBeacon.AbsoluteLocation = lBeacon.RelativeeLocation;
            }
            START_OVER:
            for (int i = 0; i < mScanners.Count; i++) {
                for (int j = 0; j < mScanners.Count; j++) {
                    if (i == j) continue;
                    if (mScanners[i].AbsoluteLocation == null) continue;
                    if (mScanners[j].AbsoluteLocation != null) continue;
                    OverlapBeacons(mScanners[i], mScanners[j]);

                }

            }
            for (int i = 0; i < mScanners.Count; i++) {
                if (mScanners[i].AbsoluteLocation == null) goto START_OVER;
            }
            return $"{CountBeacons(mScanners.ToArray())}";
        }

        public override string Part2() {
            int lManhattanDistance = 0;
            for (int i = 0; i < mScanners.Count; i++) {
                for (int j = i; j < mScanners.Count; j++) {
                    if (i == j) continue;
                    int[] lDistance = MatrixSub(mScanners[i].AbsoluteLocation, mScanners[j].AbsoluteLocation);
                    int lTemp = Math.Abs(lDistance[0]) + Math.Abs(lDistance[1]) + Math.Abs(lDistance[2]);
                    if (lTemp > lManhattanDistance) lManhattanDistance = lTemp;
                }
            }
            return $"{lManhattanDistance}";
        }

        private int CountBeacons(Scanner[] aScanners) {
            List<int[]> lBeacons = new List<int[]>();
            foreach (Beacon lBeacon in aScanners[0].Beacons) lBeacons.Add(lBeacon.AbsoluteLocation);
            for (int i = 1; i < aScanners.Length; i++) {
                for (int k = 0; k < aScanners[i].Beacons.Length; k++) {
                    bool lContained = false;
                    for (int j = 0; j < lBeacons.Count; j++) {
                        if (aScanners[i].Beacons[k].AbsoluteLocation == null) {
                            int b = 0;
                        }
                        if (PointEqual(lBeacons[j], aScanners[i].Beacons[k].AbsoluteLocation)) {
                            lContained = true;
                            break;
                        }
                    }
                    if (!lContained) lBeacons.Add(aScanners[i].Beacons[k].AbsoluteLocation);
                }
            }
            return lBeacons.Count;
        }

        private bool PointEqual(int[] aPoint1, int[] aPoint2) {
            for (int i = 0; i < 3; i++) {
                if (aPoint1[i] != aPoint2[i]) return false;
            }
            return true;
        }

        private void CreateAllRotations() {
            int[][,] aFirst = {
                new int[,] { { 1,0,0}, { 0, 1, 0 }, { 0,0,1} },
                new int[,] { { 0,1,0}, { 0, 0, 1 }, { 1,0,0} },
                new int[,] { { 0,0,1}, { 1, 0, 0 }, { 0,1,0} },
            };
            int[][,] aSecond = {
                new int[,] { { 1,0,0}, { 0, 1, 0 }, { 0,0,1} },
                new int[,] { { -1,0,0}, { 0, -1, 0 }, { 0,0,1} },
                new int[,] { { -1,0,0}, { 0, 1, 0 }, { 0,0,-1} },
                new int[,] { { 1,0,0}, { 0, -1, 0 }, { 0,0,-1} },
            };
            int[][,] aThird = {
                new int[,] { { 1,0,0}, { 0, 1, 0 }, { 0,0,1} },
                new int[,] { { 0, 0, -1 }, { 0, -1, 0 }, { -1,0,0 } }
            };
            int i = 0;
            Rotations = new int[24][,];
            for (int f = 0; f < aFirst.Length; f++) {
                for (int s = 0; s < aSecond.Length; s++) {
                    for (int t = 0; t < aThird.Length; t++, i++) {
                        Rotations[i] = Multiply(Multiply(aFirst[f], aSecond[s]), aThird[t]);
                    }
                }
            }
        }

        public static int[,] Multiply(int[,] matrix1, int[,] matrix2) {
            // cahing matrix lengths for better performance  
            var matrix1Rows = matrix1.GetLength(0);
            var matrix1Cols = matrix1.GetLength(1);
            var matrix2Rows = matrix2.GetLength(0);
            var matrix2Cols = matrix2.GetLength(1);

            // checking if product is defined  
            if (matrix1Cols != matrix2Rows)
                throw new InvalidOperationException
                  ("Product is undefined. n columns of first matrix must equal to n rows of second matrix");

            // creating the final product matrix  
            int[,] product = new int[matrix1Rows, matrix2Cols];

            // looping through matrix 1 rows  
            for (int matrix1_row = 0; matrix1_row < matrix1Rows; matrix1_row++) {
                // for each matrix 1 row, loop through matrix 2 columns  
                for (int matrix2_col = 0; matrix2_col < matrix2Cols; matrix2_col++) {
                    // loop through matrix 1 columns to calculate the dot product  
                    for (int matrix1_col = 0; matrix1_col < matrix1Cols; matrix1_col++) {
                        product[matrix1_row, matrix2_col] +=
                          matrix1[matrix1_row, matrix1_col] *
                          matrix2[matrix1_col, matrix2_col];
                    }
                }
            }

            return product;
        }

        public static int[] Multiply(int[,] matrix1, int[] matrix2) {
            // cahing matrix lengths for better performance  
            var matrix1Rows = matrix1.GetLength(0);
            var matrix1Cols = matrix1.GetLength(1);
            var matrix2Rows = matrix2.GetLength(0);

            // checking if product is defined  
            if (matrix1Cols != matrix2Rows)
                throw new InvalidOperationException
                  ("Product is undefined. n columns of first matrix must equal to n rows of second matrix");

            // creating the final product matrix  
            int[] product = new int[matrix2Rows];

            // looping through matrix 1 rows  
            for (int matrix1_row = 0; matrix1_row < matrix1Rows; matrix1_row++) {
                // for each matrix 1 row, loop through matrix 2 columns  
                for (int matrix1_col = 0; matrix1_col < matrix1Cols; matrix1_col++) {
                    product[matrix1_row] += matrix1[matrix1_row, matrix1_col] * matrix2[matrix1_col];
                }
            }

            return product;
        }

        private int[][,] Rotations;


        private bool OverlapBeacons(Scanner aFirst, Scanner aSecond) {
            //Dictionary<Beacon, Beacon> lBeacons = new Dictionary<Beacon, Beacon>();
            Tuple<Beacon, Beacon, Beacon, Beacon> lTemp = null;
            int lOverlaps = 0;
            for (int i = 0; i < aFirst.Beacons.Length; i++) {
                lOverlaps = 0;
                for (int j = i; j < aSecond.Beacons.Length; j++) {
                    for (int n = 0; n < aFirst.Beacons[i].RelativeBeaconDistance.Distances.Length; n++) {
                        for (int m = 0; m < aSecond.Beacons[j].RelativeBeaconDistance.Distances.Length; m++) {
                            if (aFirst.Beacons[i].RelativeBeaconDistance.Distances[n] == 0 || aSecond.Beacons[j].RelativeBeaconDistance.Distances[m] == 0) goto OVERJUMP_N;
                            if (aFirst.Beacons[i].RelativeBeaconDistance.Distances[n] == aSecond.Beacons[j].RelativeBeaconDistance.Distances[m]) {
                                if (lOverlaps == 0) lTemp = new Tuple<Beacon, Beacon, Beacon, Beacon>(aFirst.Beacons[i], aFirst.Beacons[n], aSecond.Beacons[j], aSecond.Beacons[m]);
                                lOverlaps++;
                            }
                        }
                        OVERJUMP_N: { }
                    }
                }
                if (lOverlaps >= 11) goto STEP_OUT;
            }
            STEP_OUT: { }
            if (lOverlaps >= 11) {
                int[] lDelta1 = MatrixSub(lTemp.Item2.AbsoluteLocation, lTemp.Item1.AbsoluteLocation);
                int[] lDelta2 = MatrixSub(lTemp.Item4.RelativeeLocation, lTemp.Item3.RelativeeLocation);
                int[] lDelta3 = MatrixSub(lTemp.Item3.RelativeeLocation, lTemp.Item4.RelativeeLocation);
                int lIndex = GetRotation(lDelta1, lDelta2);
                bool lInvers = false;
                if (lIndex == -1) {
                    lIndex = GetRotation(lDelta1, lDelta3);
                    lInvers = true;
                }
                aSecond.Rotation = Rotations[lIndex];
                if (lInvers) aSecond.AbsoluteLocation = MatrixSub(lTemp.Item2.AbsoluteLocation, Multiply(aSecond.Rotation, lTemp.Item3.RelativeeLocation));
                else aSecond.AbsoluteLocation = MatrixSub(lTemp.Item2.AbsoluteLocation, Multiply(aSecond.Rotation, lTemp.Item4.RelativeeLocation));
                CalculateAbsoluteOrigin(aSecond);
                return true;
            }
            return false;
        }

        private int GetRotation(int[] aMatrix1, int[] aMatrix2) {
            for (int i = 0; i < Rotations.Length; i++) {
                int[] lTemp = Multiply(Rotations[i], aMatrix2);
                if (aMatrix1[0] == lTemp[0] && aMatrix1[1] == lTemp[1] && aMatrix1[2] == lTemp[2]) return i;
            }
            return -1;
        }

        private void CalculateAbsoluteOrigin(Scanner aScanner) {
            for (int i = 0; i < aScanner.Beacons.Length; i++) {
                aScanner.Beacons[i].AbsoluteLocation = MatrixSum(aScanner.AbsoluteLocation, Multiply(aScanner.Rotation, aScanner.Beacons[i].RelativeeLocation));
            }
        }

        private int[] MatrixSum(int[] aFirst, int[] aSecond) {
            int[] aResult = new int[aFirst.Length];
            for (int i = 0; i < aFirst.Length; i++) {
                aResult[i] = aFirst[i] + aSecond[i];
            }
            return aResult;
        }
        private int[] MatrixSub(int[] aFirst, int[] aSecond) {
            int[] aResult = new int[aFirst.Length];
            for (int i = 0; i < aFirst.Length; i++) {
                aResult[i] = aFirst[i] - aSecond[i];
            }
            return aResult;
        }



        private class Beacon {
            public Beacon(int aX, int aY, int aZ) {
                RelativeeLocation = new int[] { aX, aY, aZ };
            }
            public int[] AbsoluteLocation { get; set; }
            public int[] RelativeeLocation { get; set; }
            public BeaconDistance RelativeBeaconDistance { get; set; }

        }
        private class BeaconDistance {
            public BeaconDistance(int aBeaconSize) {
                XDistances = new double[aBeaconSize];
                YDistances = new double[aBeaconSize];
                ZDistances = new double[aBeaconSize];
                Distances = new double[aBeaconSize];
            }
            public double[] XDistances { get; set; }
            public double[] YDistances { get; set; }
            public double[] ZDistances { get; set; }
            public double[] Distances { get; set; }
        }

        private class Scanner {
            public Scanner(int aBeaconSize, Beacon[] aBeacons) {
                Beacons = new Beacon[aBeaconSize];
                Array.Copy(aBeacons, Beacons, aBeaconSize);
                for (int i = 0; i < aBeaconSize; i++) {
                    Beacons[i].RelativeBeaconDistance = new BeaconDistance(aBeaconSize);
                }
                CalculateDistances();
            }

            public int[] AbsoluteLocation { get; set; }
            public Beacon[] Beacons { get; set; }
            public int[,] Rotation { get; set; }

            private void CalculateDistances() {
                for (int i = 0; i < this.Beacons.Length; i++) {
                    for (int j = 0; j < this.Beacons.Length; j++) {
                        if (i == j) continue;
                        double XDistance = Beacons[i].RelativeeLocation[0] - Beacons[j].RelativeeLocation[0];
                        double YDistance = Beacons[i].RelativeeLocation[1] - Beacons[j].RelativeeLocation[1];
                        double ZDistance = Beacons[i].RelativeeLocation[2] - Beacons[j].RelativeeLocation[2];
                        double Distance = Math.Sqrt(Math.Pow(XDistance, 2) + Math.Pow(YDistance, 2) + Math.Pow(ZDistance, 2));
                        Beacons[i].RelativeBeaconDistance.XDistances[j] = XDistance;
                        Beacons[i].RelativeBeaconDistance.YDistances[j] = YDistance;
                        Beacons[i].RelativeBeaconDistance.ZDistances[j] = ZDistance;
                        Beacons[i].RelativeBeaconDistance.Distances[j] = Distance;
                        Beacons[j].RelativeBeaconDistance.XDistances[i] = XDistance;
                        Beacons[j].RelativeBeaconDistance.YDistances[i] = YDistance;
                        Beacons[j].RelativeBeaconDistance.ZDistances[i] = ZDistance;
                        Beacons[j].RelativeBeaconDistance.Distances[i] = Distance;
                    }
                }
            }
        }

    }
}