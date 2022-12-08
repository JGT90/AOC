using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2022 {
    class Day07 : DayN_2022 {

        #region Fields
        private CustomDirectory MainDirectory;
        #endregion

        #region Constructor
        public Day07() {
            AddInputData(@"2022/Day07-JGT90.txt");
        }
        #endregion

        #region Properties
        protected override string PuzzleName => "No Space Left On Device";
        #endregion

        #region Methods
        private double AddDirectorySize(CustomDirectory lDirectory, double aLimit) {
            double lDirectorySize = 0;
            if (lDirectory.SubDirectory == null) return lDirectorySize;
            foreach (CustomDirectory lDir in lDirectory.SubDirectory) {
                lDirectorySize += AddDirectorySize(lDir, aLimit);
                if (lDir.Size <= aLimit) {
                    lDirectorySize += lDir.Size;
                }
            }
            return lDirectorySize;
        }
        private double Recursive(CustomDirectory lDirectory) {
            double lDirectorySize = 0;
            if (lDirectory.SubDirectory == null) {
                foreach (CustomFile lFile in lDirectory.Files) {
                    lDirectorySize += lFile.Size;
                }
                lDirectory.Size = lDirectorySize;
                return lDirectory.Size;
            }
            lDirectorySize = 0;
            foreach (CustomDirectory lDir in lDirectory.SubDirectory) {
                lDirectorySize += Recursive(lDir);
            }
            if (lDirectory.Files != null) {
                foreach (CustomFile lFile in lDirectory.Files) {
                    lDirectorySize += lFile.Size;
                }
            }
            lDirectory.Size = lDirectorySize;
            return lDirectory.Size;
        }
        #endregion

        #region Functions
        public override string SolvePartOne() {
            MainDirectory = new CustomDirectory();
            var lActualDir = MainDirectory;
            try {
                for (int i = 1; i < RawData.Length; i++) {
                    if (RawData[i] == "$ ls") {
                        i++;
                        while (!RawData[i].StartsWith("$")) {

                            if (RawData[i].Contains("dir")) {
                                if (lActualDir.SubDirectory == null) lActualDir.SubDirectory = new List<CustomDirectory>();
                                string[] lSplit = RawData[i].Split(' ');
                                lActualDir.SubDirectory.Add(new CustomDirectory() { Name = lSplit[1], Parent = lActualDir });
                            } else if (!RawData[i].StartsWith("$")) {
                                string[] lSPlit = RawData[i].Split(' ');
                                if (lActualDir.Files == null) lActualDir.Files = new List<CustomFile>();
                                lActualDir.Files.Add(new CustomFile() { Name = lSPlit[1], Size = double.Parse(lSPlit[0]) });
                            }
                            i++;
                        }
                    }
                    if (RawData[i].Contains("$ cd ..")) {
                        lActualDir = lActualDir.Parent;
                    } else if (RawData[i].Contains("$ cd")) {
                        string[] lSplit = RawData[i].Split(' ');
                        lActualDir = lActualDir.SubDirectory.Where(x => x.Name == lSplit[2]).FirstOrDefault();
                    }
                }

            } catch (Exception) { }
            Recursive(MainDirectory);
            double b = AddDirectorySize(MainDirectory, 100000);
            return b.ToString();
        }

        public override string SolvePartTwo() {
            double lTotalDiskSpace = 70000000;
            double lFreeupSpace = 30000000;
            List<double> lSpaceFreed = new List<double>();
            double RecursiveB(CustomDirectory lDirectory, ref List<double> lList) {
                if (lDirectory.SubDirectory == null) return lDirectory.Size;
                foreach (CustomDirectory lDir in lDirectory.SubDirectory) {
                    lList.Add(RecursiveB(lDir, ref lList));
                }
                return lDirectory.Size;
            }
            lSpaceFreed.Add(RecursiveB(MainDirectory, ref lSpaceFreed));
            double lMin = double.MaxValue;
            foreach (double lSpace in lSpaceFreed) {
                if ((lTotalDiskSpace - MainDirectory.Size + lSpace) > lFreeupSpace) {
                    if (lMin > lSpace) lMin = lSpace;
                }
            }
            return lMin.ToString();
        }
        #endregion

        #region Classes
        class CustomDirectory {
            public string Name { get; set; }
            public List<CustomDirectory> SubDirectory { get; set; }
            public List<CustomFile> Files { get; set; }
            public CustomDirectory Parent { get; set; }
            public double Size { get; set; }
        }
        class CustomFile {
            public string Name { get; set; }
            public double Size { get; set; }
        }
        #endregion
    }
}
