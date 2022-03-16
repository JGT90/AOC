using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day19 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_19_1.txt";

        private class Position : IEquatable<Position> {
            public int X;
            public int Y;
            public int Z;
            public int SourceIndex;
            public Position Parent;
            public string Rotation;
            public List<double> Distance = new List<double>();
            public bool Equals(Position pos) {

                //Check whether the compared object is null.  
                if (Object.ReferenceEquals(pos, null)) return false;

                //Check whether the compared object references the same data.  
                if (Object.ReferenceEquals(this, pos)) return true;

                //Check whether the UserDetails' properties are equal.  
                return X.Equals(pos.X) && Y.Equals(pos.Y)
                        && Z.Equals(pos.Z);
            }
            // If Equals() returns true for a pair of objects   
            // then GetHashCode() must return the same value for these objects.  

            public override int GetHashCode() {

                //Get hash code for the UserName field if it is not null.  
                int hashUserName = X == null ? 0 : X.GetHashCode();

                //Get hash code for the City field.  
                int hashCity = Y.GetHashCode();

                //Get hash code for the Country field.  
                int hashCountry = Z.GetHashCode();

                //Calculate the hash code for the GPOPolicy.  
                return hashUserName ^ hashCity ^ hashCountry;
            }
        }

        public static List<Tuple<int, int, int>> Matrix = new List<Tuple<int, int, int>>();
        public static bool[] ScannerDone;
        private static List<List<Position>> BeaconLists = new List<List<Position>>();
        public override void ReadIn() {
            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            //List<List<Position>> Positions = new List<List<Position>>();
            int index = 0;
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                if (string.IsNullOrEmpty(lLine)) continue;
                if (lLine.Contains("scanner")) {
                    BeaconLists.Add(new List<Position>());
                    index++;
                    continue;
                }
                string[] split = lLine.Split(',');
                if (split.Length != 3) throw new Exception();
                BeaconLists[index - 1].Add(new Position() { X = int.Parse(split[0]), Y = int.Parse(split[1]), Z = int.Parse(split[2]) });
            }
            foreach (List<Position> list in BeaconLists) {
                for (int i = 0; i < list.Count; i++) {
                    for (int y = 0; y < list.Count; y++) {
                        list[i].Distance.Add(Math.Sqrt(Math.Pow(list[i].X - list[y].X, 2) + Math.Pow(list[i].Y - list[y].Y, 2) + Math.Pow(list[i].Z - list[y].Z, 2)));
                    }
                }
            }
            ScannerDone = new bool[BeaconLists.Count];
        }

        public override string DoPartA() {
            return $"{GetBeaconList().Distinct().OrderBy(x => x.X).ToList().Count}";
        }
        private List<Position> GetBeaconList() {
            List<Position> Beacons = new List<Position>();
            // Add first list
            Beacons.AddRange(BeaconLists[0]);
            ScannerDone[0] = true;
            ScannerPosition = new List<Position>();
            ScannerPosition.Add(new Position() { X = 0, Y = 0, Z = 0 , Rotation = "012"});

            for (int i = 0; i < BeaconLists.Count; i++) {
                if (ScannerDone[i]) continue;
                Beacons.AddRange(FindOverlap(0, i, ScannerPosition[0]));
            }
            return Beacons;
        }
        private List<Position> ScannerPosition;

        public override string DoPartB() {
            while (ScannerPosition.Any(x => x.Parent !=null)) {
                for (int i = 1; i < ScannerPosition.Count; i++) {
                    if (ScannerPosition[i].Parent == null) continue;
                    Position parent = ScannerPosition[i].Parent.Parent;
                    ScannerPosition[i] = DetermineNewOrigin2(ScannerPosition[i].Parent.Rotation, ScannerPosition[i].Parent, ScannerPosition[i]);
                    ScannerPosition[i].Parent = parent;
                }
            }
            //Position bla = DetermineNewOrigin2(NewOrigin.Parent.Rotation, NewOrigin.Parent, NewOrigin);

            int maxindex1 = 0;
            int maxindex2 = 0;
            double maxdistance = 0;
            for (int i = 0; i < ScannerPosition.Count; i++) {
                for (int y = 0; y < ScannerPosition.Count; y++) {
                    double distance =Math.Abs(ScannerPosition[i].X - ScannerPosition[y].X) + Math.Abs(ScannerPosition[i].Y - ScannerPosition[y].Y) + Math.Abs(ScannerPosition[i].Z - ScannerPosition[y].Z);
                    ScannerPosition[i].Distance.Add(distance);
                    if (maxdistance < distance) {
                        maxdistance = distance;
                        maxindex1 = i;
                        maxindex2 = y;
                    }
                }
            }
            
            return $"{(int)maxdistance}";
            //int a = (int)ScannerPosition.Select(x => x.Distance.Max();
        }

        private List<Position> FindOverlap(int aSourceIndex, int aDestIndex, Position Parent) {
            List<Position> Beacons = new List<Position>();
            List<Position> BeaconA = BeaconLists[aSourceIndex];
            List<Position> BeaconB = BeaconLists[aDestIndex];
            Beacons.AddRange(BeaconB);
            List<Tuple<int, int, int, int>> lMatching = SearchMatchingPoints(BeaconA, BeaconB);
            if (lMatching.Select(x => x.Item1).Distinct().Count() < 12) return new List<Position>();
            if (lMatching.Count < 12) return new List<Position>();
            string rotation = DetermineRotation(BeaconA, BeaconB, lMatching[0], out bool flip);
            Position NewOrigin = new Position();
            //bool done = false;
            //for (int n = 0; n < BeaconA.Count; n++) {
            //    for (int y = 0; y < BeaconB.Count; y++) {
            //        NewOrigin = DetermineNewOrigin(rotation, BeaconA[n], BeaconB[y]);
            //        List<Position> rotatedbeacons = new List<Position>();
            //        foreach (Position beac in BeaconB) {
            //            Position rotated = Rotate(beac, rotation);
            //            rotatedbeacons.Add(new Position() { X = NewOrigin.X + rotated.X, Y = NewOrigin.Y + rotated.Y, Z = NewOrigin.Z + rotated.Z });
            //        }
            //        if (rotatedbeacons.Count - rotatedbeacons.Except(BeaconA).Count() > 11) {
            //            done = true;
            //            break;
            //        }

            //    }
            //    if (done) break;
            //}
            if (flip) {
                NewOrigin = DetermineNewOrigin(rotation, BeaconA[lMatching[0].Item1], BeaconB[lMatching[0].Item3]);
            } else {
                NewOrigin = DetermineNewOrigin(rotation, BeaconA[lMatching[1].Item1], BeaconB[lMatching[1].Item3]);
            }
            if (rotation == string.Empty) {
                return new List<Position>();
            }
            if (lMatching.Select(x => x.Item1).Distinct().Count() < 12) return new List<Position>();
            ScannerDone[aDestIndex] = true;
            //string rotation = FindRotation(BeaconA, BeaconB, out Position NewOrigin);
            //if (rotation == string.Empty) {
            //    return new List<Position>();
            //}
            //ScannerDone[aDestIndex] = true;
            NewOrigin.Parent = Parent;
            NewOrigin.Rotation = rotation;
            ScannerPosition.Add(NewOrigin);
            NewOrigin.SourceIndex = aSourceIndex;
            for (int i = 0; i < BeaconLists.Count; i++) {
                if (ScannerDone[i]) continue;
                Beacons.AddRange(FindOverlap(aDestIndex, i, NewOrigin));
            }
            //Console.WriteLine($"Source: {aSourceIndex} - Dest: {aDestIndex} - Rotation: {rotation} - Origin: {NewOrigin.X} {NewOrigin.Y} {NewOrigin.Z}");
            List<Position> TranslatedBeacons = new List<Position>();
            if (aSourceIndex == 0) {
                int a = 0;
            }
            foreach (Position pos in Beacons) {
                TranslatedBeacons.Add(Translate(NewOrigin, pos, rotation));
            }
            return TranslatedBeacons;
        }
        private static Position DetermineNewOrigin2(string aRotation, Position aOldOrigin, Position aPoint) {
            //string bla = Rotations[aSourceIndex];
            //int a = 0;
            Position RotatedPoint = Rotate(aPoint, aRotation);
            int x = +aOldOrigin.X + RotatedPoint.X;
            int y = +aOldOrigin.Y + RotatedPoint.Y;
            int z = +aOldOrigin.Z + RotatedPoint.Z;
            //int x = +aOldOrigin.X - (aRotation[0] == '0' ? aPoint.X : aRotation[0] == '3' ? -aPoint.X : aRotation[0] == '1' ? aPoint.Y : aRotation[0] == '4' ? -aPoint.Y : aRotation[0] == '2' ? aPoint.Z : -aPoint.Z);
            //int y = +aOldOrigin.Y - (aRotation[1] == '0' ? aPoint.X : aRotation[1] == '3' ? -aPoint.X : aRotation[1] == '1' ? aPoint.Y : aRotation[1] == '4' ? -aPoint.Y : aRotation[1] == '2' ? aPoint.Z : -aPoint.Z);
            //int z = +aOldOrigin.Z - (aRotation[2] == '0' ? aPoint.X : aRotation[2] == '3' ? -aPoint.X : aRotation[2] == '1' ? aPoint.Y : aRotation[2] == '4' ? -aPoint.Y : aRotation[2] == '2' ? aPoint.Z : -aPoint.Z);
            //int originx = ScannerPosition[ScannerDone.IndexOf(aSourceIndex)].Item1 + (bla[0] == '0' ? x : bla[0] == '3' ? -x : bla[0] == '1' ? y : bla[0] == '4' ? -y : bla[0] == '2' ? z : -z);
            //int originy = ScannerPosition[ScannerDone.IndexOf(aSourceIndex)].Item2 + (bla[1] == '0' ? x : bla[1] == '3' ? -x : bla[1] == '1' ? y : bla[1] == '4' ? -y : bla[1] == '2' ? z : -z);
            //int originz = ScannerPosition[ScannerDone.IndexOf(aSourceIndex)].Item3 + (bla[2] == '0' ? x : bla[2] == '3' ? -x : bla[2] == '1' ? y : bla[2] == '4' ? -y : bla[2] == '2' ? z : -z);
            //return new Tuple<int, int, int>(originx, originy, originz);
            return new Position() { X = x, Y = y, Z = z };
        }
        private static Position Rotate(Position aPoint, string aRotation) {
            int x = (aRotation[0] == '0' ? aPoint.X : aRotation[0] == '3' ? -aPoint.X : aRotation[0] == '1' ? aPoint.Y : aRotation[0] == '4' ? -aPoint.Y : aRotation[0] == '2' ? aPoint.Z : -aPoint.Z);
            int y = (aRotation[1] == '0' ? aPoint.X : aRotation[1] == '3' ? -aPoint.X : aRotation[1] == '1' ? aPoint.Y : aRotation[1] == '4' ? -aPoint.Y : aRotation[1] == '2' ? aPoint.Z : -aPoint.Z);
            int z = (aRotation[2] == '0' ? aPoint.X : aRotation[2] == '3' ? -aPoint.X : aRotation[2] == '1' ? aPoint.Y : aRotation[2] == '4' ? -aPoint.Y : aRotation[2] == '2' ? aPoint.Z : -aPoint.Z);
            return new Position() { X = x, Y = y, Z = z, Parent = aPoint.Parent };
        }
        private static Position DetermineNewOrigin(string aRotation, Position aOldOrigin, Position aPoint) {
            //string bla = Rotations[aSourceIndex];
            //int a = 0;
            Position RotatedPoint = Rotate(aPoint, aRotation);
            int x = +aOldOrigin.X - RotatedPoint.X;
            int y = +aOldOrigin.Y - RotatedPoint.Y;
            int z = +aOldOrigin.Z - RotatedPoint.Z;
            //int x = +aOldOrigin.X - (aRotation[0] == '0' ? aPoint.X : aRotation[0] == '3' ? -aPoint.X : aRotation[0] == '1' ? aPoint.Y : aRotation[0] == '4' ? -aPoint.Y : aRotation[0] == '2' ? aPoint.Z : -aPoint.Z);
            //int y = +aOldOrigin.Y - (aRotation[1] == '0' ? aPoint.X : aRotation[1] == '3' ? -aPoint.X : aRotation[1] == '1' ? aPoint.Y : aRotation[1] == '4' ? -aPoint.Y : aRotation[1] == '2' ? aPoint.Z : -aPoint.Z);
            //int z = +aOldOrigin.Z - (aRotation[2] == '0' ? aPoint.X : aRotation[2] == '3' ? -aPoint.X : aRotation[2] == '1' ? aPoint.Y : aRotation[2] == '4' ? -aPoint.Y : aRotation[2] == '2' ? aPoint.Z : -aPoint.Z);
            //int originx = ScannerPosition[ScannerDone.IndexOf(aSourceIndex)].Item1 + (bla[0] == '0' ? x : bla[0] == '3' ? -x : bla[0] == '1' ? y : bla[0] == '4' ? -y : bla[0] == '2' ? z : -z);
            //int originy = ScannerPosition[ScannerDone.IndexOf(aSourceIndex)].Item2 + (bla[1] == '0' ? x : bla[1] == '3' ? -x : bla[1] == '1' ? y : bla[1] == '4' ? -y : bla[1] == '2' ? z : -z);
            //int originz = ScannerPosition[ScannerDone.IndexOf(aSourceIndex)].Item3 + (bla[2] == '0' ? x : bla[2] == '3' ? -x : bla[2] == '1' ? y : bla[2] == '4' ? -y : bla[2] == '2' ? z : -z);
            //return new Tuple<int, int, int>(originx, originy, originz);
            return new Position() { X = x, Y = y, Z = z };
        }
        private static Position Translate(Position aScannerOrigin, Position pos, string rotation) {
            int x = aScannerOrigin.X + (rotation[0] == '0' ? pos.X : rotation[0] == '3' ? -pos.X : rotation[0] == '1' ? pos.Y : rotation[0] == '4' ? -pos.Y : rotation[0] == '2' ? pos.Z : -pos.Z);
            int y = aScannerOrigin.Y + (rotation[1] == '0' ? pos.X : rotation[1] == '3' ? -pos.X : rotation[1] == '1' ? pos.Y : rotation[1] == '4' ? -pos.Y : rotation[1] == '2' ? pos.Z : -pos.Z);
            int z = aScannerOrigin.Z + (rotation[2] == '0' ? pos.X : rotation[2] == '3' ? -pos.X : rotation[2] == '1' ? pos.Y : rotation[2] == '4' ? -pos.Y : rotation[2] == '2' ? pos.Z : -pos.Z);
            return new Position() { X = x, Y = y, Z = z };
        }
        private static List<Tuple<int, int, int, int>> SearchMatchingPoints(List<Position> Ascanners, List<Position> Bscanners) {
            List<Tuple<int, int, int, int>> matchingpoints = new List<Tuple<int, int, int, int>>();

            for (int an = 0; an < Ascanners.Count; an++) {
                for (int am = 0; am < Ascanners.Count; am++) {
                    for (int bn = 0; bn < Bscanners.Count; bn++) {
                        for (int bm = 0; bm < Bscanners.Count; bm++) {
                            if (Ascanners[an].Distance[am] == Bscanners[bn].Distance[bm] && Ascanners[an].Distance[am] != 0 && Bscanners[bn].Distance[bm] != 0) {
                                if (Math.Abs(Ascanners[an].X - Ascanners[am].X) == Math.Abs(Bscanners[bn].X - Bscanners[bm].X) || Math.Abs(Ascanners[an].X - Ascanners[am].X) == Math.Abs(Bscanners[bn].Y - Bscanners[bm].Y) || Math.Abs(Ascanners[an].X - Ascanners[am].X) == Math.Abs(Bscanners[bn].Z - Bscanners[bm].Z)) {
                                    if (!matchingpoints.Contains(new Tuple<int, int, int, int>(an, am, bn, bm))) {
                                        matchingpoints.Add(new Tuple<int, int, int, int>(an, am, bn, bm));
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return matchingpoints;
        }
        private static string FindRotation(List<Position> Ascanners, List<Position> Bscanners, out Position NewOrigin) {
            string rotation = string.Empty;
            NewOrigin = new Position();
            for (int i = 0; i < 48; i++) {
                switch (i) {
                    case 0:
                        rotation = "012";
                        break;
                    //case 1:
                    //    rotation = "015";
                    //    break;
                    //case 2:
                    //    rotation = "042";
                    //    break;
                    case 3:
                        rotation = "045";
                        break;
                    //case 4:
                    //    rotation = "312";
                    //    break;
                    case 5:
                        rotation = "315";
                        break;
                    case 6:
                        rotation = "342";
                        break;
                    //case 7:
                    //    rotation = "345";
                    //    break;
                    //case 8:
                    //    rotation = "102";
                    //    break;
                    case 9:
                        rotation = "105";
                        break;
                    case 10:
                        rotation = "132";
                        break;
                    //case 11:
                    //    rotation = "135";
                    //    break;
                    case 12:
                        rotation = "402";
                        break;
                    //case 13:
                    //    rotation = "405";
                    //    break;
                    //case 14:
                    //    rotation = "432";
                    //    break;
                    case 15:
                        rotation = "435";
                        break;
                    case 16:
                        rotation = "120";
                        break;
                    //case 17:
                    //    rotation = "150";
                    //    break;
                    //case 18:
                    //    rotation = "123";
                    //    break;
                    case 19:
                        rotation = "153";
                        break;
                    //case 20:
                    //    rotation = "420";
                    //    break;
                    case 21:
                        rotation = "450";
                        break;
                    case 22:
                        rotation = "423";
                        break;
                    //case 23:
                    //    rotation = "453";
                    //    break;
                    //case 24:
                    //    rotation = "021";
                    //    break;
                    case 25:
                        rotation = "051";
                        break;
                    case 26:
                        rotation = "024";
                        break;
                    //case 27:
                    //    rotation = "054";
                    //    break;
                    case 28:
                        rotation = "321";
                        break;
                    //case 29:
                    //    rotation = "351";
                    //    break;
                    //case 30:
                    //    rotation = "324";
                    //    break;
                    case 31:
                        rotation = "354";
                        break;
                    //case 32:
                    //    rotation = "210";
                    //    break;
                    case 33:
                        rotation = "213";
                        break;
                    case 34:
                        rotation = "240";
                        break;
                    //case 35:
                    //    rotation = "243";
                    //    break;
                    case 36:
                        rotation = "510";
                        break;
                    //case 37:
                    //    rotation = "513";
                    //    break;
                    //case 38:
                    //    rotation = "540";
                    //    break;
                    case 39:
                        rotation = "543";
                        break;
                    case 40:
                        rotation = "201";
                        break;
                    //case 41:
                    //    rotation = "231";
                    //    break;
                    //case 42:
                    //    rotation = "204";
                    //    break;
                    case 43:
                        rotation = "234";
                        break;
                    //case 44:
                    //    rotation = "501";
                    //    break;
                    case 45:
                        rotation = "531";
                        break;
                    case 46:
                        rotation = "504";
                        break;
                    //case 47:
                    //    rotation = "534";
                    //    break;
                    default:
                        continue;

                }
                for (int n = 0; n < Ascanners.Count; n++) {
                    for (int y = 0; y < Bscanners.Count; y++) {
                        NewOrigin = DetermineNewOrigin(rotation, Ascanners[n], Bscanners[y]);
                        List<Position> rotatedbeacons = new List<Position>();
                        foreach (Position beac in Bscanners) {
                            Position rotated = Rotate(beac, rotation);
                            rotatedbeacons.Add(new Position() { X = NewOrigin.X + rotated.X, Y = NewOrigin.Y + rotated.Y, Z = NewOrigin.Z + rotated.Z });
                        }
                        if (rotatedbeacons.Count - rotatedbeacons.Except(Ascanners).Count() > 11) {
                            return rotation;
                        }

                    }
                }
            }
            return string.Empty;
        }
        private static List<Tuple<int, int, int, int>> SearchMatchingPoints2(List<Position> Ascanners, List<Position> Bscanners) {
            List<Tuple<int, int, int, int>> matchingpoints = new List<Tuple<int, int, int, int>>();

            for (int an = 0; an < Ascanners.Count; an++) {
                for (int am = 0; am < Ascanners.Count; am++) {
                    for (int bn = 0; bn < Bscanners.Count; bn++) {
                        for (int bm = 0; bm < Bscanners.Count; bm++) {
                            if (an == am) continue;
                            if (bn == bm) continue;
                            if (Math.Abs(Ascanners[an].X - Ascanners[am].X) == Math.Abs(Bscanners[bn].X - Bscanners[bm].X) || Math.Abs(Ascanners[an].X - Ascanners[am].X) == Math.Abs(Bscanners[bn].Y - Bscanners[bm].Y) || Math.Abs(Ascanners[an].X - Ascanners[am].X) == Math.Abs(Bscanners[bn].Z - Bscanners[bm].Z))
                                if (Math.Abs(Ascanners[an].Y - Ascanners[am].Y) == Math.Abs(Bscanners[bn].X - Bscanners[bm].X) || Math.Abs(Ascanners[an].Y - Ascanners[am].Y) == Math.Abs(Bscanners[bn].Y - Bscanners[bm].Y) || Math.Abs(Ascanners[an].Y - Ascanners[am].Y) == Math.Abs(Bscanners[bn].Z - Bscanners[bm].Z))
                                    if (Math.Abs(Ascanners[an].Z - Ascanners[am].Z) == Math.Abs(Bscanners[bn].X - Bscanners[bm].X) || Math.Abs(Ascanners[an].Z - Ascanners[am].Z) == Math.Abs(Bscanners[bn].Y - Bscanners[bm].Y) || Math.Abs(Ascanners[an].Z - Ascanners[am].Z) == Math.Abs(Bscanners[bn].Z - Bscanners[bm].Z))
                                        if (!matchingpoints.Contains(new Tuple<int, int, int, int>(an, am, bn, bm))) {
                                            matchingpoints.Add(new Tuple<int, int, int, int>(an, am, bn, bm));
                                        }
                        }
                    }
                }
            }
            return matchingpoints;
        }
        private static string DetermineRotation(List<Position> Apositions, List<Position> Bpositions, Tuple<int, int, int, int> aMatching, out bool flip) {
            flip = false;
            int xDisA = Apositions[aMatching.Item1].X - Apositions[aMatching.Item2].X;
            int yDisA = Apositions[aMatching.Item1].Y - Apositions[aMatching.Item2].Y;
            int zDisA = Apositions[aMatching.Item1].Z - Apositions[aMatching.Item2].Z;
            int xDisB = Bpositions[aMatching.Item3].X - Bpositions[aMatching.Item4].X;
            int yDisB = Bpositions[aMatching.Item3].Y - Bpositions[aMatching.Item4].Y;
            int zDisB = Bpositions[aMatching.Item3].Z - Bpositions[aMatching.Item4].Z;
            string rotation = string.Empty;
            if (Math.Abs(xDisA) == Math.Abs(xDisB)) {
                if (xDisA == -xDisB) {
                    rotation += "3";
                } else {
                    rotation += "0";
                }
            } else if (Math.Abs(xDisA) == Math.Abs(yDisB)) {
                if (xDisA == -yDisB) {
                    rotation += "4";
                } else {
                    rotation += "1";
                }
            } else if (Math.Abs(xDisA) == Math.Abs(zDisB)) {
                if (xDisA == -zDisB) {
                    rotation += "5";
                } else {
                    rotation += "2";
                }
            }
            if (Math.Abs(yDisA) == Math.Abs(yDisB)) {
                if (yDisA == -yDisB) {
                    rotation += "4";
                } else {
                    rotation += "1";
                }
            } else if (Math.Abs(yDisA) == Math.Abs(xDisB)) {
                if (yDisA == -xDisB) {
                    rotation += "3";
                } else {
                    rotation += "0";
                }
            } else if (Math.Abs(yDisA) == Math.Abs(zDisB)) {
                if (yDisA == -zDisB) {
                    rotation += "5";
                } else {
                    rotation += "2";
                }
            }
            if (Math.Abs(zDisA) == Math.Abs(zDisB)) {
                if (zDisA == -zDisB) {
                    rotation += "5";
                } else {
                    rotation += "2";
                }
            } else if (Math.Abs(zDisA) == Math.Abs(xDisB)) {
                if (zDisA == -xDisB) {
                    rotation += "3";
                } else {
                    rotation += "0";
                }
            } else if (Math.Abs(zDisA) == Math.Abs(yDisB)) {
                if (zDisA == -yDisB) {
                    rotation += "4";
                } else {
                    rotation += "1";
                }
            }
            switch (rotation) {
                case "231":
                    rotation = "504";
                    break;
                case "204":
                    rotation = "531";
                    break;
                case "501":
                    rotation = "234";
                    break;
                case "534":
                    rotation = "201";
                    break;
                case "210":
                    rotation = "543";
                    break;
                case "243":
                    rotation = "510";
                    break;
                case "513":
                    rotation = "240";
                    break;
                case "345":
                    rotation = "012";
                    break;
                case "312":
                    rotation = "045";
                    break;
                case "042":
                    rotation = "315";
                    break;
                case "015":
                    rotation = "342";
                    break;
                case "432":
                    rotation = "105";
                    break;
                case "405":
                    rotation = "132";
                    break;
                case "135":
                    rotation = "402";
                    break;
                case "102":
                    rotation = "435";
                    break;
                case "453":
                    rotation = "120";
                    break;
                case "420":
                    rotation = "153";
                    break;
                case "123":
                    rotation = "450";
                    break;
                case "250":
                    rotation = "423";
                    break;
                case "324":
                    rotation = "051";
                    break;
                case "351":
                    rotation = "024";
                    break;
                case "054":
                    rotation = "321";
                    break;
                case "021":
                    rotation = "354";
                    break;
                case "540":
                    rotation = "213";
                    break;
                default:
                    flip = true;
                    break;

            }
            return rotation;
        }
    }
}
