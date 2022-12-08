using SEGCC;
using System;
using System.Collections.Generic;

namespace AOC2021 {
    internal class Day22 : DayN {
        Cube[] mCubes;


        public override string Part1() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week22.txt");
            mCubes = new Cube[lInput.Length];
            for (int i = 0; i < lInput.Length; i++) {
                string[] lSplit = lInput[i].Split(new string[] { "x=", "y=", "z=", ",", "..", " " }, System.StringSplitOptions.RemoveEmptyEntries);
                mCubes[i] = new Cube(lSplit[0] == "on", int.Parse(lSplit[1]), int.Parse(lSplit[2]), int.Parse(lSplit[3]), int.Parse(lSplit[4]), int.Parse(lSplit[5]), int.Parse(lSplit[6]));
            }
            List<Cube> lIntersectedCubes = new List<Cube>();
            foreach (Cube lCube in mCubes) {
                if (lCube.XMin < -50 || lCube.XMin > 50) continue;
                AddIntersectedCubes(lIntersectedCubes, lCube);
            }
            double lCount = 0;
            foreach (Cube lCube in lIntersectedCubes) {
                if (lCube.TurnOn) lCount += lCube.Volume;
                else lCount -= lCube.Volume;
            }

            return $"{lCount}";
        }

        public override string Part2() {
            List<Cube> lIntersectedCubes = new List<Cube>();
            foreach (Cube lCube in mCubes) {
                AddIntersectedCubes(lIntersectedCubes, lCube);
            }
            double lCount = 0;
            foreach (Cube lCube in lIntersectedCubes) {
                if (lCube.TurnOn) lCount += lCube.Volume;
                else lCount -= lCube.Volume;
            }

            return $"{lCount}";
        }

        private void AddIntersectedCubes(List<Cube> aIntersectedCubes, Cube aCube) {
            List<Cube> lIntersectedCubesToAdd = new List<Cube>();
            if (aCube.TurnOn) lIntersectedCubesToAdd.Add(aCube);
            foreach (Cube lPreviouslyIntersectedCube in aIntersectedCubes) {
                Cube newlyIntersectedCube = IntersectCubes(aCube, lPreviouslyIntersectedCube, !lPreviouslyIntersectedCube.TurnOn);
                if (newlyIntersectedCube != null) lIntersectedCubesToAdd.Add(newlyIntersectedCube);
            }
            aIntersectedCubes.AddRange(lIntersectedCubesToAdd);
        }

        private Cube IntersectCubes(Cube aCurrent, Cube aPreviouslyIntersected, bool aOn) {
            // If there's no intersection, we return null as the cubes don't overlap.
            if (aCurrent.XMin > aPreviouslyIntersected.XMax || aCurrent.XMax < aPreviouslyIntersected.XMin ||
                aCurrent.YMin > aPreviouslyIntersected.YMax || aCurrent.YMax < aPreviouslyIntersected.YMin ||
                aCurrent.ZMin > aPreviouslyIntersected.ZMax || aCurrent.ZMax < aPreviouslyIntersected.ZMin) {
                return null;
            }
              // Otherwise we return a new cube that describes the overlap
              else {

                return new Cube(aOn,
                Math.Max(aCurrent.XMin, aPreviouslyIntersected.XMin),
                Math.Min(aCurrent.XMax, aPreviouslyIntersected.XMax),
                Math.Max(aCurrent.YMin, aPreviouslyIntersected.YMin),
                Math.Min(aCurrent.YMax, aPreviouslyIntersected.YMax),
                Math.Max(aCurrent.ZMin, aPreviouslyIntersected.ZMin),
                Math.Min(aCurrent.ZMax, aPreviouslyIntersected.ZMax));
            }
        }
        private Cube SplitCube(Cube aCube1, Cube aCube2, ref List<Cube> aCubes) {
            int xMin1 = aCube1.XMin;
            int xMax1 = aCube1.XMax;
            int yMin1 = aCube1.YMin;
            int yMax1 = aCube1.YMax;
            int zMin1 = aCube1.ZMin;
            int zMax1 = aCube1.ZMax;

            int xMax2 = aCube2.XMax + 1;
            int yMax2 = aCube2.YMax + 1;
            int zMax2 = aCube2.ZMax + 1;

            if (aCube1.XMin < aCube2.XMin) {
                aCubes.Add(new Cube(aCube1.TurnOn, xMin1, aCube2.XMin - 1, yMin1, yMax1, zMin1, zMax1));
                xMin1 = aCube2.XMin;
            }

            if( yMin1<aCube2.YMin ) {
                aCubes.Add(new Cube(aCube1.TurnOn, xMin1, xMax1, yMin1, aCube2.YMin - 1, zMin1, zMax1));
                yMin1 = aCube2.YMin;
            }
            if( zMin1<aCube2.ZMin ) {
                aCubes.Add(new Cube(aCube1.TurnOn, xMin1, xMax1, yMin1, yMax1, zMin1, aCube2.ZMin - 1));
                zMin1 = aCube2.ZMin;
            }

            if (xMax2 <= xMax1 ) {
                aCubes.Add(new Cube(aCube1.TurnOn, xMax2, xMax1, yMin1, yMax1, zMin1, zMax1));
                xMax1 = aCube2.XMax;
            }
            if( yMax2 <= yMax1 ){
                aCubes.Add(new Cube(aCube1.TurnOn, xMin1, xMax1, yMax2, yMax1, zMin1, zMax1));
                yMax1 = aCube2.YMax;
            }
            if( zMax2 <= zMax1) {
                aCubes.Add(new Cube(aCube1.TurnOn, xMin1, xMax1, yMin1, yMax1, zMax2, zMax1));
                zMax1 = aCube2.ZMax;
            }
            return new Cube(aCube1.TurnOn, xMin1, xMax1, yMin1, yMax1, zMin1, zMax1);
        }
        private class Cube {
            public bool TurnOn { get; set; }
            public int XMin { get; set; }
            public int XMax { get; set; }
            public int YMin { get; set; }
            public int YMax { get; set; }
            public int ZMin { get; set; }
            public int ZMax { get; set; }
            public double Volume { get; set; }
            public Cube(bool aTurnOn, int aXMin, int aXMax, int aYMin, int aYMax, int aZMin, int aZMax) {
                TurnOn = aTurnOn;
                XMin = aXMin;
                XMax = aXMax;
                YMin = aYMin;
                YMax = aYMax;
                ZMin = aZMin;
                ZMax = aZMax;
                Volume = (double)(XMax - XMin + 1) * (YMax - YMin + 1) * (ZMax - ZMin + 1);
            }
        }
    }
}