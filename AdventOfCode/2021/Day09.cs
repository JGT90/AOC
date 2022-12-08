using System.Collections.Generic;

namespace AdventOfCode.Year2021 {
    internal class Day09 : DayN {
        static int[,] mHeightMap;
        static bool[,] mDone;

        public override string Part1() {
            string[] lLines = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week09.txt");
            mHeightMap = new int[lLines.Length, lLines[0].Length];
            double lRiskLevel = 0;
            for (int y = 0; y < lLines.Length; y++) {
                for (int x = 0; x < lLines[y].Length; x++) {
                    mHeightMap[y, x] = lLines[y][x] - 48;
                }
            }
            for (int y = 0; y < mHeightMap.GetLength(0); y++) {
                for (int x = 0; x < mHeightMap.GetLength(1); x++) {
                    if (IsALowPoint(y, x)) lRiskLevel += 1 + mHeightMap[y, x];
                }
            }
            //Console.Write("Week09 - PartA: ");
            //Console.WriteLine(lRiskLevel);
            return lRiskLevel.ToString();
            //DrawImage();
        }

        public override string Part2() {
            mDone = new bool[mHeightMap.GetLength(0), mHeightMap.GetLength(1)];
            List<double> lBasinSize = new List<double>();
            for (int y = 0; y < mHeightMap.GetLength(0); y++) {
                for (int x = 0; x < mHeightMap.GetLength(1); x++) {
                    if (IsALowPoint(y, x)) {
                        lBasinSize.Add(GetBasinSize(y, x));
                    }
                }
            }
            lBasinSize.Sort();
            //Console.Write("Week9 - PartB: ");
            //Console.WriteLine(lBasinSize[lBasinSize.Count - 1] * lBasinSize[lBasinSize.Count - 2] * lBasinSize[lBasinSize.Count - 3]);
            return (lBasinSize[lBasinSize.Count - 1] * lBasinSize[lBasinSize.Count - 2] * lBasinSize[lBasinSize.Count - 3]).ToString();
        }

        private bool IsALowPoint(int y, int x) {
            if (y > 0 && mHeightMap[y, x] >= mHeightMap[y - 1, x]) {
                return false;
            }
            if (y < mHeightMap.GetLength(0) - 1 && mHeightMap[y, x] >= mHeightMap[y + 1, x]) {
                return false;
            }
            if (x > 0 && mHeightMap[y, x] >= mHeightMap[y, x - 1]) {
                return false;
            }
            if (x < mHeightMap.GetLength(1) - 1 && mHeightMap[y, x] >= mHeightMap[y, x + 1]) {
                return false;
            }
            return true;
        }

        private double GetBasinSize(int y, int x) {
            if (mDone[y, x]) return 0;
            mDone[y, x] = true;
            double lSum = 0;
            if (y > 0 && mHeightMap[y - 1, x] < 9) {
                lSum += GetBasinSize(y - 1, x);
            }
            if (y < mHeightMap.GetLength(0) - 1 && mHeightMap[y + 1, x] < 9) {
                lSum += GetBasinSize(y + 1, x);
            }
            if (x > 0 && mHeightMap[y, x - 1] < 9) {
                lSum += GetBasinSize(y, x - 1);
            }
            if (x < mHeightMap.GetLength(1) - 1 && mHeightMap[y, x + 1] < 9) {
                lSum += GetBasinSize(y, x + 1);
            }
            return 1 + lSum;
        }

        //private void DrawImage() {
        //    string finalImage = @"..\..\..\Inputs\FinalImage.jpg";
        //    Bitmap lBitmap = new Bitmap(1600, 1600);
        //    Graphics g = Graphics.FromImage(lBitmap);
        //    g.Clear(SystemColors.AppWorkspace);
        //    Image lSkull = Image.FromFile(@"..\..\..\Inputs\skull.png");
        //    Image lRoad = Image.FromFile(@"..\..\..\Inputs\road.png");
        //    Image lSmoke = Image.FromFile(@"..\..\..\Inputs\smoke.png");
        //    Image lLandslide = Image.FromFile(@"..\..\..\Inputs\landslide.png");
        //    Image lGrass = Image.FromFile(@"..\..\..\Inputs\grass.png");
        //    ((Bitmap)lSkull).SetResolution(g.DpiX, g.DpiY);
        //    ((Bitmap)lRoad).SetResolution(g.DpiX, g.DpiY);
        //    ((Bitmap)lSmoke).SetResolution(g.DpiX, g.DpiY);
        //    ((Bitmap)lLandslide).SetResolution(g.DpiX, g.DpiY);
        //    ((Bitmap)lGrass).SetResolution(g.DpiX, g.DpiY);
        //    for (int y = 0; y < 100; y ++) {
        //        for (int x = 0; x < 100; x ++) {
        //            if (mHeightMap[y, x] == 9) {
        //                g.DrawImage(lRoad, new Point(x * 16, y * 16));
        //            } else if (mHeightMap[y, x] == 0) {
        //                g.DrawImage(lSkull, new Point(x * 16, y * 16));
        //            } else if (mHeightMap[y,x] < 3) {
        //                g.DrawImage(lSmoke, new Point(x * 16, y * 16));
        //            } else if (mHeightMap[y,x] < 6) {
        //                g.DrawImage(lLandslide, new Point(x * 16, y * 16));
        //            } else if (mHeightMap[y,x] < 9) {
        //                g.DrawImage(lGrass, new Point(x * 16, y * 16));
        //            }
        //        }
        //    }
        //    g.Dispose();
        //    lBitmap.Save(finalImage, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    lBitmap.Dispose();
        //}
    }
}