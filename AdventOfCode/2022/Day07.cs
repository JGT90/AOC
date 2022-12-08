using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2022 {
    class Day07 {
        CustomDirectory MainDirectory;
        public string DoPartA() {
            string lPath = @"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode2022\Input\Day07.txt";
            MainDirectory = new CustomDirectory();
            string[] lLines = File.ReadAllLines(lPath);
            var lActualDir = MainDirectory;
            try {
                for (int i = 1; i < lLines.Length; i++) {
                    if (lLines[i] == "$ ls") {
                        i++;
                        while(!lLines[i].StartsWith("$")) {

                            if (lLines[i].Contains("dir")) {
                                if (lActualDir.SubDirectory == null) lActualDir.SubDirectory = new List<CustomDirectory>();
                                string[] lSplit = lLines[i].Split(' ');
                                lActualDir.SubDirectory.Add(new CustomDirectory() { Name = lSplit[1], Parent = lActualDir });
                            } else if (!lLines[i].StartsWith("$")) {
                                string[] lSPlit = lLines[i].Split(' ');
                                if (lActualDir.Files == null) lActualDir.Files = new List<CustomFile>();
                                lActualDir.Files.Add(new CustomFile() { Name = lSPlit[1], Size = double.Parse(lSPlit[0]) });
                            }
                            i++;
                        } 
                    } 
                    if (lLines[i].Contains("$ cd ..")) {
                        lActualDir = lActualDir.Parent;
                    } else if (lLines[i].Contains("$ cd")) {
                        string[] lSplit = lLines[i].Split(' ');
                        lActualDir = lActualDir.SubDirectory.Where(x => x.Name == lSplit[2]).FirstOrDefault();
                    }
                }

            } catch(Exception) { }
            Recursive(MainDirectory);
            double b = AddDirectorySize(MainDirectory, 100000);
            return b.ToString();
        }
        private double AddDirectorySize(CustomDirectory lDirectory, double aLimit) {
            double lDirectorySize = 0;
            if (lDirectory.SubDirectory == null) return lDirectorySize;
            foreach(CustomDirectory lDir in lDirectory.SubDirectory) {
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
                foreach(CustomFile lFile in lDirectory.Files) {
                    lDirectorySize += lFile.Size;
                }
                lDirectory.Size = lDirectorySize;
                return lDirectory.Size;
            }
            lDirectorySize = 0;
            foreach(CustomDirectory lDir in lDirectory.SubDirectory) {
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

        public string DoPartB() {
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
            foreach(double lSpace in lSpaceFreed) {
                if ((lTotalDiskSpace - MainDirectory.Size + lSpace) > lFreeupSpace) {
                    if (lMin > lSpace) lMin = lSpace;
                } 
            }
            return lMin.ToString();
        }
    }

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
}
