using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022 {
    class Program {
        static void Main(string[] args) {
            string lExit = string.Empty;
            do {
                Console.WriteLine($"Hello to my implementation of AdventOfCode 2022");
                Console.WriteLine();
                Console.WriteLine($"Have a look and decide which you want to see");
                Console.WriteLine($"\t01 = Day01 - Calorie Counting");
                Console.WriteLine($"\t02 = Day02 - Rock Paper Scissors");
                Console.WriteLine($"\t03 = Day03 - Rucksack Reorganizatio");
                Console.WriteLine($"\t04 = Day04 - Camp Cleanup");
                Console.WriteLine($"\t05 = Day05 - Supply Stacks");
                Console.WriteLine($"\t06 = Day06 - Tuning Trouble");
                Console.WriteLine($"\t07 = Day07 - No Space Left On Device");
                Console.WriteLine($"\t08 = Day08 - Treetop Tree House");
                //Console.WriteLine($"\t09 = Day09 - Smoke Basin");
                //Console.WriteLine($"\t10 = Day10 - Syntax Scoring");
                //Console.WriteLine($"\t11 = Day11 - Dumbo Octopus");
                //Console.WriteLine($"\t12 = Day12 - Passage Pathing");
                //Console.WriteLine($"\t13 = Day13 - Transparent Origami");
                //Console.WriteLine($"\t14 = Day14 - Extended Polymerization");
                //Console.WriteLine($"\t15 = Day15 - Chiton");
                //Console.WriteLine($"\t16 = Day16 - Packet Decoder");
                //Console.WriteLine($"\t17 = Day17 - Trick Shot");
                //Console.WriteLine($"\t18 = Day18 - Snailfish");
                //Console.WriteLine($"\t19 = Day19 - Beacon Scanner");
                //Console.WriteLine($"\t20 = Day20 - Trench Map");
                //Console.WriteLine($"\t21 = Day21 - Dirac Dice");
                //Console.WriteLine($"\t22 = Day22 - Reactor Reboot");
                //Console.WriteLine($"\t23 = Day23 - Amphipod");
                //Console.WriteLine($"\t24 = Day24 - Arithmetic Logic Unit");
                //Console.WriteLine($"\t25 = Day25 - Sea Cucumber");
                Console.WriteLine();
                Console.Write($"Please select the day you want to solve: ");
                string lDay = Console.ReadLine();
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                switch (lDay) {
                    case "1":
                    case "01":
                        #region Day01
                        Day01 day01 = new Day01();
                        Console.WriteLine($"Day1_1 Result is {day01.DoPartA()}");
                        Console.WriteLine($"Day1_2 Result is {day01.DoPartB()}");
                        #endregion
                        break;
                    case "2":
                    case "02":
                        #region Day02
                        Day02 day02 = new Day02();
                        Console.WriteLine($"Day2_1 Result is {day02.DoPartA()}");
                        Console.WriteLine($"Day2_2 Result is {day02.DoPartB()}");
                        #endregion
                        break;
                    case "3":
                    case "03":
                        #region Day03
                        Day03 day03 = new Day03();
                        Console.WriteLine($"Day3_1 Result is {day03.DoPartA()}");
                        Console.WriteLine($"Day3_2 Result is {day03.DoPartB()}");
                        #endregion
                        break;
                    case "4":
                    case "04":
                        #region Day04
                        Day04 day04 = new Day04();
                        Console.WriteLine($"Day4_1 Result is {day04.DoPartA()}");
                        Console.WriteLine($"Day4_2 Result is {day04.DoPartB()}");
                        #endregion
                        break;
                    case "5":
                    case "05":
                        #region Day05
                        Day05 day05 = new Day05();
                        Console.WriteLine($"Day5_1 Result is {day05.DoPartA()}");
                        Console.WriteLine($"Day5_2 Result is {day05.DoPartB()}");
                        #endregion
                        break;
                    case "6":
                    case "06":
                        #region Day06
                        Day06 day06 = new Day06();
                        Console.WriteLine($"Day06_1 Result is {day06.DoPartA()}");
                        Console.WriteLine($"Day06_2 Result is {day06.DoPartB()}");
                        #endregion
                        break;
                    case "7":
                    case "07":
                        #region Day07
                        Day07 day07 = new Day07();
                        Console.WriteLine($"Day7_1 Result is {day07.DoPartA()}");
                        Console.WriteLine($"Day7_2 Result is {day07.DoPartB()}");
                        #endregion
                        break;
                    case "8":
                    case "08":
                        #region Day08
                        Day08 day08 = new Day08();
                        Console.WriteLine($"Day8_1 Result is {day08.DoPartA()}");
                        Console.WriteLine($"Day8_2 Result is {day08.DoPartB()}");
                        #endregion
                        break;
                        //case "9":
                        //case "09":
                        //    #region Day09
                        //    Day09 day09 = new Day09();
                        //    day09.ReadIn();
                        //    Console.WriteLine($"Day9_1 Result is {day09.DoPartA()}");
                        //    Console.WriteLine($"Day9_2 Result is {day09.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "10":
                        //    #region Day10
                        //    Day10 day10 = new Day10();
                        //    day10.ReadIn();
                        //    Console.WriteLine($"Day10_1 Result is {day10.DoPartA()}");
                        //    Console.WriteLine($"Day10_2 Result is {day10.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "11":
                        //    #region Day11
                        //    Day11 day11 = new Day11();
                        //    day11.ReadIn();
                        //    Console.WriteLine($"Day11_1 Result is {day11.DoPartA()}");
                        //    Console.WriteLine($"Day11_2 Result is {day11.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "12":
                        //    #region Day12
                        //    Day12 day12 = new Day12();
                        //    day12.ReadIn();
                        //    Console.WriteLine($"Day12_1 Result is {day12.DoPartA()}");
                        //    Console.WriteLine($"Day12_2 Result is {day12.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "13":
                        //    #region Day13
                        //    Day13 day13 = new Day13();
                        //    day13.ReadIn();
                        //    Console.WriteLine($"Day13_1 Result is {day13.DoPartA()}");
                        //    Console.WriteLine($"Day13_2 Result is {day13.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "14":
                        //    #region Day14
                        //    Day14 day14 = new Day14();
                        //    day14.ReadIn();
                        //    Console.WriteLine($"Day14_1 Result is {day14.DoPartA()}");
                        //    Console.WriteLine($"Day14_2 Result is {day14.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "15":
                        //    #region Day15
                        //    Day15 day15 = new Day15();
                        //    day15.ReadIn();
                        //    Console.WriteLine($"Day15_1 Result is {day15.DoPartA()}");
                        //    Console.WriteLine($"Day15_2 Result is {day15.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "16":
                        //    #region Day16
                        //    Day16 day16 = new Day16();
                        //    day16.ReadIn();
                        //    Console.WriteLine($"Day16_1 Result is {day16.DoPartA()}");
                        //    Console.WriteLine($"Day16_2 Result is {day16.DoPartB()}");
                        //    break;
                        //case "17":
                        //    #region Day17
                        //    Day17 day17 = new Day17();
                        //    day17.ReadIn();
                        //    Console.WriteLine($"Day17_1 Result is {day17.DoPartA()}");
                        //    Console.WriteLine($"Day17_2 Result is {day17.DoPartB()}");
                        //    #endregion
                        //    #endregion
                        //    break;
                        //case "18":
                        //    #region Day18
                        //    Day18 day18 = new Day18();
                        //    day18.ReadIn();
                        //    Console.WriteLine($"Day18_1 Result is {day18.DoPartA()}");
                        //    Console.WriteLine($"Day1_2 Result is {day18.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "19":
                        //    #region Day19
                        //    Day19 day19 = new Day19();
                        //    day19.ReadIn();
                        //    Console.WriteLine($"Day19_1 Result is {day19.DoPartA()}");
                        //    Console.WriteLine($"Day19_2 Result is {day19.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "20":
                        //    #region Day20
                        //    Day20 day20 = new Day20();
                        //    day20.ReadIn();
                        //    Console.WriteLine($"Day20_1 Result is {day20.DoPartA()}");
                        //    day20.ReadIn();
                        //    Console.WriteLine($"Day20_2 Result is {day20.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "21":
                        //    #region Day21
                        //    Day21 day21 = new Day21();
                        //    Console.WriteLine($"Day21_1 Result is {day21.DoPartA()}");
                        //    Console.WriteLine($"Day21_2 Result is {day21.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "22":
                        //    #region Day22
                        //    Day22 day22 = new Day22();
                        //    day22.ReadIn();
                        //    Console.WriteLine($"Day22_1 Result is {day22.DoPartA()}");
                        //    day22.ReadIn();
                        //    Console.WriteLine($"Day22_2 Result is {day22.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "23":
                        //    #region Day23
                        //    Day23 day23 = new Day23();
                        //    Console.WriteLine($"Day23_1 Result is {day23.DoPartA()}");
                        //    Console.WriteLine($"Day23_2 Result is {day23.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "24":
                        //    #region Day24
                        //    Day24 day24 = new Day24();
                        //    day24.ReadIn();
                        //    Console.WriteLine($"Day24_1 Result is {day24.DoPartA()}");
                        //    Console.WriteLine($"Day24_2 Result is {day24.DoPartB()}");
                        //    #endregion
                        //    break;
                        //case "25":
                        //    #region Day25
                        //    Day25 day25 = new Day25();
                        //    day25.ReadIn();
                        //    Console.WriteLine($"Day25_1 Result is {day25.DoPartA()}");
                        //    #endregion
                        //    break;
                }
                stopwatch.Stop();
                Console.WriteLine($"Day{lDay} took {stopwatch.ElapsedMilliseconds}ms to solve.");
                //Transfer.DoTransfer(@"C:\Users\jgt\source\repos\AdventOfCode\AdventOfCode\InputFiles\apu.txt");
                Console.WriteLine();
                Console.WriteLine("write exit to stop the application");
                lExit = Console.ReadLine();
            } while (lExit != "exit");
        }
    }
}
