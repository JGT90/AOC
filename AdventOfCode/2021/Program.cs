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
                        Day01 week01 = new Day01();
                        week01.Run();
                        break;
                    case "02":
                    case "2":
                        Day02 week02 = new Day02();
                        week02.Run();
                        break;
                    case "03":
                    case "3":
                        Day03 week03 = new Day03();
                        week03.Run();
                        break;
                    case "04":
                    case "4":
                        Day04 week04 = new Day04();
                        week04.Run();
                        break;
                    case "05":
                    case "5":
                        Day05 week05 = new Day05();
                        week05.Run();
                        break;
                    case "06":
                    case "6":
                        Day06 week06 = new Day06();
                        week06.Run();
                        break;
                    case "07":
                    case "7":
                        Day07 week07 = new Day07();
                        week07.Run();
                        break;
                    case "08":
                    case "8":
                        Day08 week08 = new Day08();
                        week08.Run();
                        break;
                    case "09":
                    case "9":
                        Day09 week09 = new Day09();
                        week09.Run();
                        break;
                    case "10":
                        Day10 week10 = new Day10();
                        week10.Run();
                        break;
                    case "11":
                        Day11 week11 = new Day11();
                        week11.Run();
                        break;
                    case "12":
                        Day12 week12 = new Day12();
                        week12.Run();
                        break;
                    case "13":
                        Day13 week13 = new Day13();
                        week13.Run();
                        break;
                    case "14":
                        Day14 week14 = new Day14();
                        week14.Run();
                        break;
                    case "15":
                        Day15 week15 = new Day15();
                        week15.Run();
                        break;
                    case "16":
                        Day16 week16 = new Day16();
                        week16.Run();
                        break;
                    case "17":
                        Day17 week17 = new Day17();
                        week17.Run();
                        break;
                    case "18":
                        Day18 week18 = new Day18();
                        week18.Run();
                        break;
                    case "19":
                        Day19 week19 = new Day19();
                        week19.Run();
                        break;
                    case "20":
                        Day20 week20 = new Day20();
                        week20.Run();
                        break;
                    case "21":
                        Day21 week21 = new Day21();
                        week21.Run();
                        break;
                    case "22":
                        Day22 week22 = new Day22();
                        week22.Run();
                        break;
                    case "23":
                        Day23 week23 = new Day23();
                        week23.Run();
                        break;
                    case "24":
                        Day24 week24 = new Day24();
                        week24.Run();
                        break;
                    case "25":
                        Day25 week25 = new Day25();
                        week25.Run();
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
