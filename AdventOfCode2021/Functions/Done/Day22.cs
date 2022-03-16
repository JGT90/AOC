using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day22 : AdventOfCode.AdventOfCode{
        const string mPath = @"..\..\InputFiles\Input12_22_1.txt";
        List<Cube> Cubes;
        private class Cube {
            public bool On { get; set; }
            public double xMin { get; set; }
            public double xMax { get; set; }
            public double yMin { get; set; }
            public double yMax { get; set; }
            public double zMin { get; set; }
            public double zMax { get; set; }
            public double Volume { get; set; }
            public Cube(bool aOn, double axMin, double axMax, double ayMin, double ayMax, double azMin, double azMax) {
                On = aOn;
                xMin = axMin;
                xMax = axMax;
                yMin = ayMin;
                yMax = ayMax;
                zMin = azMin;
                zMax = azMax;
                Volume = (xMax - xMin + 1) * (yMax - yMin + 1) * (zMax - zMin + 1);
            }
        }

        public override void ReadIn() {
            Cubes = new List<Cube>();
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                string[] split = lLine.Split(new string[] { " ", "=", "..", "," }, StringSplitOptions.None);
                Cubes.Add(new Cube(
                    split[0] == "on" ? true : false,
                    int.Parse(split[2]),
                    int.Parse(split[3]),
                    int.Parse(split[5]),
                    int.Parse(split[6]),
                    int.Parse(split[8]),
                    int.Parse(split[9])) { });
            }
        }

        private Cube IntersectCubes(Cube current, Cube previouslyIntersected, bool on) {
            // If there's no intersection, we return null as the cubes don't overlap.
            if (current.xMin > previouslyIntersected.xMax || current.xMax < previouslyIntersected.xMin ||
                current.yMin > previouslyIntersected.yMax || current.yMax < previouslyIntersected.yMin ||
                current.zMin > previouslyIntersected.zMax || current.zMax < previouslyIntersected.zMin) {
                return null;
            }
              // Otherwise we return a new cube that describes the overlap
              else {
                return new Cube(on,
                Math.Max(current.xMin, previouslyIntersected.xMin),
                Math.Min(current.xMax, previouslyIntersected.xMax),
                Math.Max(current.yMin, previouslyIntersected.yMin),
                Math.Min(current.yMax, previouslyIntersected.yMax),
                Math.Max(current.zMin, previouslyIntersected.zMin),
                Math.Min(current.zMax, previouslyIntersected.zMax));
            }
        }

        public override string DoPartA() {
            return DoStuff(Cubes, true);
        }
        public override string DoPartB() {
            return DoStuff(Cubes, false);
        }

        private string DoStuff(List<Cube> aCubes, bool initialize) {
            // Setup a list to hold all the cubes from the various instersections we will perform
            List<Cube> intersectedCubes = new List<Cube>();

            // cycle through each cube in our main cube list
            foreach (Cube currentCube in aCubes) {
                if (initialize && (currentCube.xMin < -50 || currentCube.xMin > 50)) {
                    continue;
                }
                List<Cube> intersectedCubesToAdd = new List<Cube>();
                // if the cube is on, add it to the add list.
                // it hasn't been intersected, but it will be in the next pass
                if (currentCube.On) {
                    intersectedCubesToAdd.Add(currentCube);
                }
                // check the current cube against all previously intersected cubes and
                // if there's an intersect, add that resulting cube to the list to add
                foreach (Cube previouslyIntersectedCube in intersectedCubes) {
                    // we send the opposite value of ON for the intersection so we don't double count on/on || off/off cubes that overlap
                    Cube newlyIntersectedCube = IntersectCubes(currentCube, previouslyIntersectedCube, !previouslyIntersectedCube.On);
                    if (newlyIntersectedCube != null) {
                        intersectedCubesToAdd.Add(newlyIntersectedCube);
                    }
                }
                // add all the cubes in the ToAdd list to the intersectedCubes list
                foreach (Cube c in intersectedCubesToAdd) {
                    intersectedCubes.Add(c);
                }
            }
            double part2 = 0;
            foreach (Cube c in intersectedCubes) {
                if (c.On) {
                    part2 += c.Volume;
                } else {
                    part2 -= c.Volume;
                }
            }
            return part2.ToString();
        }
    }
}
