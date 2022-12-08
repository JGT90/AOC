using AdventOfCode.Year2021;
using AdventOfCode.Year2022;
using System;

namespace AdventOfCode {
    class AdventOfCode {
        static void Main(string[] args) {
            Console.WriteLine($"From which year is the puzzle you would like to solve?");
            Console.WriteLine("\t- Year 2021");
            Console.WriteLine("\t- Year 2022");
            Console.WriteLine();
            string _Year = Console.ReadLine();
            switch (_Year) {
                case "2021":
                    AOC2021.Puzzle();
                    break;
                case "2022":
                    AOC2022.Puzzle();
                    break;
            }
            Console.WriteLine($"enter any key close");
            Console.ReadLine();
        }
    }
}
