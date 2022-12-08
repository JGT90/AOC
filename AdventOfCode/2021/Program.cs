using System;
using System.Diagnostics;

namespace AOC2021 {
    internal class Program {
        static void Main(string[] args) {
            bool lDone = false;
            while (!lDone) {
                Console.WriteLine("Which week do you want to solve?");
                Console.WriteLine("\t- Week 1:  Sonar Sweep");
                Console.WriteLine("\t- Week 2:  Dive!");
                Console.WriteLine("\t- Week 3:  Binary Diagnostic");
                Console.WriteLine("\t- Week 4:  Giant Squid");
                Console.WriteLine("\t- Week 5:  Hydrothermal Venture");
                Console.WriteLine("\t- Week 6:  Lanternfish");
                Console.WriteLine("\t- Week 7:  The Treachery of Whales");
                Console.WriteLine("\t- Week 8:  Seven Segment Search");
                Console.WriteLine("\t- Week 9:  Smoke Basin");
                Console.WriteLine("\t- Week 10: Syntax Scoring");
                Console.WriteLine("\t- Week 11: Dumbo Octopus");
                Console.WriteLine("\t- Week 12: Passage Pathing");
                Console.WriteLine("\t- Week 13: Transparent Origami");
                Console.WriteLine("\t- Week 14: Extended Polymerization");
                Console.WriteLine("\t- Week 15: Chiton");
                Console.WriteLine("\t- Week 16: Packet Decoder");
                Console.WriteLine("\t- Week 17: Trick Shot");
                Console.WriteLine("\t- Week 18: Snailfish");
                Console.WriteLine("\t- Week 19: Beacon Scanner");
                Console.WriteLine("\t- Week 20: Trench Map");
                Console.WriteLine("\t- Week 21: Dirac Dice");
                Console.WriteLine("\t- Week 22: Reactor Reboot");
                Console.WriteLine("\t- Week 23: Amphipod");
                Console.WriteLine("\t- Week 24: Arithmetic Logic Unit");
                Console.WriteLine("\t- Week 25: Sea Cucumber");
                string lInput = Console.ReadLine();
                Stopwatch lStopwatch = System.Diagnostics.Stopwatch.StartNew();
                switch (lInput) {
                    case "01":
                    case "1":
                        Week01 week01 = new Week01();
                        week01.Run();
                        //week01.Part1();
                        //week01.Part2();
                        break;
                    case "02":
                    case "2":
                        Week02 week02 = new Week02();
                        week02.Run();
                        //week02.Part1();
                        //week02.Part2();
                        break;
                    case "03":
                    case "3":
                        Week03 week03 = new Week03();
                        week03.Run();
                        //week03.Part1();
                        //week03.Part2();
                        break;
                    case "04":
                    case "4":
                        Week04 week04 = new Week04();
                        week04.Run();
                        //week04.Part1();
                        //week04.Part2();
                        break;
                    case "05":
                    case "5":
                        Week05 week05 = new Week05();
                        week05.Run();
                        //week05.Part1();
                        //week05.Part2();
                        break;
                    case "06":
                    case "6":
                        Week06 week06 = new Week06();
                        week06.Run();
                        //week06.Part1();
                        //week06.Part2();
                        break;
                    case "07":
                    case "7":
                        Week07 week07 = new Week07();
                        week07.Run();
                        //week07.Part1();
                        //week07.Part2();
                        break;
                    case "08":
                    case "8":
                        Week08 week08 = new Week08();
                        week08.Run();
                        //week08.Part1();
                        //week08.Part2();
                        break;
                    case "09":
                    case "9":
                        Week09 week09 = new Week09();
                        week09.Run();
                        //week09.Part1();
                        //week09.Part2();
                        break;
                    case "10":
                        Week10 week10 = new Week10();
                        week10.Run();
                        //week10.Part1();
                        //week10.Part2();
                        break;
                    case "11":
                        Week11 week11 = new Week11();
                        week11.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "12":
                        Week12 week12 = new Week12();
                        week12.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "13":
                        Week13 week13 = new Week13();
                        week13.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "14":
                        Week14 week14 = new Week14();
                        week14.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "15":
                        Week15 week15 = new Week15();
                        week15.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "16":
                        Week16 week16 = new Week16();
                        week16.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "17":
                        Week17 week17 = new Week17();
                        week17.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "18":
                        Week18 week18 = new Week18();
                        week18.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "19":
                        Week19 week19 = new Week19();
                        week19.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "20":
                        Week20 week20 = new Week20();
                        week20.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "21":
                        Week21 week21 = new Week21();
                        week21.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "22":
                        Week22 week22 = new Week22();
                        week22.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "23":
                        Week23 week23 = new Week23();
                        week23.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "24":
                        Week24 week24 = new Week24();
                        week24.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    case "25":
                        Week25 week25 = new Week25();
                        week25.Run();
                        //week11.Part1();
                        //week11.Part2();
                        break;
                    default:
                        lDone = true;
                        break;
                }
                lStopwatch.Stop();
                Console.WriteLine($"Week {lInput} took {lStopwatch.ElapsedMilliseconds}ms to solve");
            }
        }
    }
}
