using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public class Day16 : AdventOfCode.AdventOfCode {
        const string mPath = @"..\..\InputFiles\Input12_16_1.txt";
        string mHexString;
        public override void ReadIn() {

            if (!File.Exists(Path.GetFullPath(mPath))) {
                Console.WriteLine($"File does not exist, {mPath}");
                return;
            }
            mHexString = string.Empty;
            foreach (string lLine in System.IO.File.ReadLines(mPath)) {
                mHexString = String.Join(String.Empty, lLine);
            }
        }
        public override string DoPartA() {
            // 1. step decode from hex to bin
            string BinaryString = String.Join(String.Empty, mHexString.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                ));
            // 2. step
            BinaryString = DecodePacket(BinaryString, out Packet packet);

            return $"{SumVersion(packet)}";
        }
        public override string DoPartB() {
            // 1. step decode from hex to bin
            string BinaryString = String.Join(String.Empty, mHexString.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                ));
            // 2. step
            BinaryString = DecodePacket(BinaryString, out Packet packet);

            //double bla =  SumIdDependent(packet);
            //double bla = blubb(packet);
            return $"{packet.dValue}";
        }
        
        private static double CalculateSum(Packet aPacket) {
            double sum = 0;
            switch (aPacket.TypeId) {
                case 0:
                    foreach (Packet packet in aPacket.SubPacket) {
                        if (packet.Value == string.Empty) continue;
                        sum += packet.dValue;
                    }
                    break;
                case 1:
                    sum = 1;
                    foreach (Packet packet in aPacket.SubPacket) {
                        if (packet.Value == string.Empty) continue;
                        sum *= packet.dValue;
                    }
                    break;
                case 2:
                    sum = aPacket.SubPacket.Min(p => p.dValue);
                    break;
                case 3:
                    sum = aPacket.SubPacket.Max(p => p.dValue);
                    break;
                case 4:
                    sum = Convert.ToInt64(aPacket.Value, 2);
                    break;
                case 5:
                    if (aPacket.SubPacket[0].dValue > aPacket.SubPacket[1].dValue) {
                        sum = 1;
                    } else {
                        sum = 0;
                    }
                    break;
                case 6:
                    if (aPacket.SubPacket[0].dValue < aPacket.SubPacket[1].dValue) {
                        sum = 1;
                    } else {
                        sum = 0;
                    }
                    break;
                case 7:
                    if (aPacket.SubPacket[0].dValue == aPacket.SubPacket[1].dValue) {
                        sum = 1;
                    } else {
                        sum = 0;
                    }
                    break;
                default:
                    break;

            }
            return sum;
        }
        //private static double SumIdDependent(Packet aPacket) {
        //    double sum = 0;
        //    aPacket.dValue = sum;
        //    if (aPacket == null) return sum;
        //    sum = CalculateSum(aPacket);
        //    aPacket.dValue = sum;
        //    if (aPacket.SubPacket == null) return sum;
        //    foreach (Packet packet in aPacket.SubPacket) {
        //        sum += SumIdDependent(packet);
        //    }
        //    //aPacket.dValue = sum;
        //    return sum;
        //}
        private static double SumVersion(Packet aPacket) {
            if (aPacket == null) return 0;
            if (aPacket.SubPacket == null) return aPacket.Version;
            double sum = aPacket.Version;
            foreach (Packet packet in aPacket.SubPacket) {
                sum += SumVersion(packet);
            }
            return sum;
        }
        //static int count = 0;
        private static string DecodePacket(string aBinaryString, out Packet lPacket) {
            if (aBinaryString.Length < 8) {
                lPacket = null;
                return "";
            }
            //count++;
            // Get version
            string lVersion = aBinaryString.Substring(0, 3);
            aBinaryString = aBinaryString.Remove(0, 3);
            lPacket = new Packet(Convert.ToInt32(lVersion, 2));
            // Get type id
            string lType = aBinaryString.Substring(0, 3);
            aBinaryString = aBinaryString.Remove(0, 3);
            lPacket.TypeId = Convert.ToInt32(lType, 2);
            if (lPacket.TypeId == 4) {
                // literal value
                char lPrefix;
                do {
                    lPrefix = aBinaryString[0];
                    lPacket.Value += aBinaryString.Substring(1, 4);
                    aBinaryString = aBinaryString.Remove(0, 5);
                } while (lPrefix == '1');
            } else {
                // operator
                lPacket.LengthTypeId = int.Parse(aBinaryString.Substring(0, 1));
                aBinaryString = aBinaryString.Remove(0, 1);
                if (lPacket.LengthTypeId == 1) {
                    // 11-bit number
                    string lNumberPackets = aBinaryString.Substring(0, 11);
                    aBinaryString = aBinaryString.Remove(0, 11);
                    lPacket.SubPacket = new List<Packet>();
                    for (int i = 0; i < Convert.ToInt32(lNumberPackets, 2); i++) {
                        aBinaryString = DecodePacket(aBinaryString, out Packet Subpacket);
                        if (Subpacket != null) lPacket.SubPacket.Add(Subpacket);
                    }
                } else if (lPacket.LengthTypeId == 0) {
                    // 15-bit number
                    string lNumberOfBits = aBinaryString.Substring(0, 15);
                    aBinaryString = aBinaryString.Remove(0, 15);
                    int NumberOfBits = Convert.ToInt32(lNumberOfBits, 2);
                    string lTemp = aBinaryString;
                    lPacket.SubPacket = new List<Packet>();
                    do {
                        lTemp = DecodePacket(lTemp, out Packet Subpacket);
                        if (Subpacket != null) lPacket.SubPacket.Add(Subpacket);
                        if (lTemp == string.Empty) break;
                    } while (aBinaryString.LastIndexOf(lTemp) != NumberOfBits);
                    aBinaryString = lTemp;
                } else {
                    int a = 0;
                }
                //for (int i = 0; i < lPacket.SubPacket.Count; i++) {
                //    aBinaryString = DecodePacket(aBinaryString, out Packet Subpacket);
                //    lPacket.SubPacket[i] = Subpacket;
                //}
            }
            lPacket.dValue = CalculateSum(lPacket);
            return aBinaryString;
        }

        public class Packet {
            public int Version { get; set; }
            public int TypeId { get; set; }
            public string Value { get; set; }
            public double dValue { get; set; }
            public int LengthTypeId { get; set; }
            public List<Packet> SubPacket { get; set; }
            public Packet(int aVersion) {
                Version = aVersion;
            }
            public Packet() { }

        }
    }
}
