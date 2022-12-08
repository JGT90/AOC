using SEGCC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Week16Lib_JGT {
    public class Week16 : IWeek16 {

        string mBinaryMessage;
        int mVersionCount = 0;
        Package mPackage;

        private Dictionary<char, string> HEX_TO_BIN = new Dictionary<char, string>() { { '0', "0000" }, { '1', "0001" }, { '2', "0010" }, { '3', "0011" }, { '4', "0100" }, { '5', "0101" }, { '6', "0110" }, { '7', "0111" }, { '8', "1000" }, { '9', "1001" }, { 'a', "1010" }, { 'b', "1011" }, { 'c', "1100" }, { 'd', "1101" }, { 'e', "1110" }, { 'f', "1111" } };
        private class Package {
            public Package() { }
            public Package(int aVersion) {
                Version = aVersion;
            }
            public Package(int aVersion, int aTypeID) : this(aVersion) {
                TypeID = aTypeID;
                if (aTypeID == 4) {
                    IsLiteral = true;
                } else {
                    SubPackage = new List<Package>();
                }
            }
            public double Value { get; set; }
            public bool IsLiteral { get; set; }
            public int Version { get; set; }
            public int TypeID { get; set; }
            public double Number { get; set; }
            public List<Package> SubPackage { get; set; }
            public double TotalLength { get; set; }
            public int NumberOfPackages { get; set; }
        }

        private enum PartOfPackage {
            Version,
            TypeId,
            Number,
            LastNumber,
            TotalLength,
            NumberOfSubPackages
        }
        public void ParseInputFile(string filename) {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week16.txt");
            mBinaryMessage = string.Join("", lInput[0].ToLower().Select(x => HEX_TO_BIN[x]));
        }
        public string Part1() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week16.txt");
            mBinaryMessage = string.Join("", lInput[0].ToLower().Select(x => HEX_TO_BIN[x]));
            mPackage = DecodeBinary(0, out int lIndexMoved);
            return $"{mVersionCount}";
        }

        public string Part2() {
            return $"{EvaluateValue(mPackage)}";
        }

        private Package DecodeBinary(int aStartIndex, out int aIndexMoved) {
            aIndexMoved = 0;
            PartOfPackage partOfPackage = PartOfPackage.Version;
            Package lPackage = new Package();
            for (int i = aStartIndex; i < mBinaryMessage.Length;) {
                switch (partOfPackage) {
                    case PartOfPackage.Version:
                        lPackage = new Package(Convert.ToInt32(mBinaryMessage.Substring(i, 3), 2));
                        partOfPackage = PartOfPackage.TypeId;
                        mVersionCount += lPackage.Version;
                        i += 3;
                        break;
                    case PartOfPackage.TypeId:
                        lPackage = new Package(lPackage.Version, Convert.ToInt32(mBinaryMessage.Substring(i, 3), 2));
                        i += 3;
                        if (!lPackage.IsLiteral) {
                            if (mBinaryMessage[i] == '0') partOfPackage = PartOfPackage.TotalLength;
                            else if (mBinaryMessage[i] == '1') partOfPackage = PartOfPackage.NumberOfSubPackages;
                            else throw new Exception("bla");
                            i++;
                        } else partOfPackage = PartOfPackage.Number;
                        break;
                    case PartOfPackage.Number:
                        bool lNumberEnded = false;
                        string lNumber = string.Empty;
                        do {
                            if (mBinaryMessage[i] == '0') lNumberEnded = true;
                            i++;
                            lNumber += mBinaryMessage.Substring(i, 4);
                            //lPackage.Number += Convert.ToInt32(mBinaryMessage.Substring(i, 4), 2);
                            i += 4;
                        } while (!lNumberEnded);
                        aIndexMoved = i - aStartIndex;
                        lPackage.Number = Convert.ToInt64(lNumber, 2);
                        return lPackage;
                    case PartOfPackage.LastNumber:
                        break;
                    case PartOfPackage.TotalLength:
                        lPackage.TotalLength = Convert.ToInt32(mBinaryMessage.Substring(i, 15), 2);
                        i += 15;
                        int lTemp = i;
                        do {
                            lPackage.SubPackage.Add(DecodeBinary(i, out aIndexMoved));
                            i += aIndexMoved;
                            //i++;
                        } while ((i - lTemp) != lPackage.TotalLength);
                        aIndexMoved = i - aStartIndex;
                        return lPackage;
                    //partOfPackage = PartOfPackage.Version;
                    //break;
                    case PartOfPackage.NumberOfSubPackages:
                        lPackage.NumberOfPackages = Convert.ToInt32(mBinaryMessage.Substring(i, 11), 2);
                        i += 11;
                        for (int j = 0; j < lPackage.NumberOfPackages; j++) {
                            lPackage.SubPackage.Add(DecodeBinary(i, out aIndexMoved));
                            i += aIndexMoved;
                        }
                        aIndexMoved = i - aStartIndex;
                        return lPackage;
                        //break;
                }
            }
            return lPackage;
        }

        private double EvaluateValue(Package aPackage) {
            switch (aPackage.TypeID) {
                case 0: // Sum
                    double sum = 0;
                    foreach (Package package in aPackage.SubPackage) {
                        sum += EvaluateValue(package);
                    }
                    return sum;
                case 1:
                    double product = 1;
                    foreach (Package package in aPackage.SubPackage) {
                        product *= EvaluateValue(package);
                    }
                    return product;
                case 2:
                    double minimum = double.MaxValue;
                    foreach (Package package in aPackage.SubPackage) {
                        package.Value = EvaluateValue(package);
                        if (minimum > package.Value) minimum = package.Value;
                    }
                    return minimum;
                case 3:
                    double maximum = 0;
                    foreach (Package package in aPackage.SubPackage) {
                        package.Value = EvaluateValue(package);
                        if (maximum < package.Value) maximum = package.Value;
                    }
                    return maximum;
                case 4:
                    aPackage.Value = aPackage.Number;
                    return aPackage.Number;
                case 5:
                    foreach (Package package in aPackage.SubPackage) {
                        package.Value = EvaluateValue(package);
                    }
                    if (aPackage.SubPackage[0].Value > aPackage.SubPackage[1].Value) return 1;
                    else return 0;
                case 6:
                    foreach (Package package in aPackage.SubPackage) {
                        package.Value = EvaluateValue(package);
                    }
                    if (aPackage.SubPackage[0].Value < aPackage.SubPackage[1].Value) return 1;
                    else return 0;
                case 7:
                    foreach (Package package in aPackage.SubPackage) {
                        package.Value = EvaluateValue(package);
                    }
                    if (aPackage.SubPackage[0].Value == aPackage.SubPackage[1].Value) return 1;
                    else return 0;
            }
            return 0;
        }
    }
}
