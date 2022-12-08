using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public abstract class DayN_2022 {
        #region Fields
        private const string RELATIVE_PATH = @"..\..\InputData\";
        private List<string> _InputFiles = new List<string>();
        #endregion

        #region Properties
        protected abstract string PuzzleName { get; }
        public string[] RawData { get; private set; }
        #endregion

        #region Functions
        protected void AddInputData(string InputFile) => _InputFiles.Add(InputFile);
        protected string[] ReadFile(string FilePath, bool RemoveEmptyLines = false) {
            return File.ReadAllText(RELATIVE_PATH + FilePath).Replace("\r", string.Empty).Split(new char[] { '\n' }, RemoveEmptyLines ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }
        public virtual void SolveAllPuzzle() {
            Console.WriteLine(PuzzleName);
            foreach  (string _InputFile in _InputFiles) {
                Console.WriteLine(_InputFile);
                Stopwatch lWatch = Stopwatch.StartNew();
                RawData = ReadFile(_InputFile);
                Console.WriteLine($"\t{lWatch.Elapsed.TotalSeconds.ToString("0.0000000", CultureInfo.InvariantCulture)}s Initialization\r\n");
                lWatch.Restart();
                string _result = SolvePartOne();
                Console.WriteLine($"\t{lWatch.Elapsed.TotalSeconds.ToString("0.0000000", CultureInfo.InvariantCulture)}s Part 1 ==> {_result}\r\n");
                lWatch.Restart();
                _result = SolvePartTwo();
                Console.WriteLine($"\t{lWatch.Elapsed.TotalSeconds.ToString("0.0000000", CultureInfo.InvariantCulture)}s Part 2 ==> {_result}\r\n");
            }
        }
        public virtual void SolvePuzzle() {
            if (_InputFiles.Count == 0) return;
            Console.WriteLine(PuzzleName);
            Console.WriteLine(_InputFiles[0]);
            Stopwatch lWatch = Stopwatch.StartNew();
            RawData = ReadFile(_InputFiles[0]);
            Console.WriteLine($"\t{lWatch.Elapsed.TotalSeconds.ToString("0.0000000", CultureInfo.InvariantCulture)}s Initialization\r\n");
            lWatch.Restart();
            string _result = SolvePartOne();
            Console.WriteLine($"\t{lWatch.Elapsed.TotalSeconds.ToString("0.0000000", CultureInfo.InvariantCulture)}s Part 1 ==> {_result}\r\n");
            lWatch.Restart();
            _result = SolvePartTwo();
            Console.WriteLine($"\t{lWatch.Elapsed.TotalSeconds.ToString("0.0000000", CultureInfo.InvariantCulture)}s Part 2 ==> {_result}\r\n");
        }
        public abstract string SolvePartOne();
        public abstract string SolvePartTwo();
        #endregion
    }
    /// <summary>
    /// Base class for SEGCC Puzzles Solutions
    /// <remarks>
    /// <para/>
    /// Inerhit via "class YourClassName : WeekN {...}"
    /// <para/>
    /// Your class must override Part1() and Part2()
    /// <para/>
    /// like this: "public override string Part1() {...}"
    /// </remarks>
    /// </summary>
    public abstract class DayN
    {
        const string _relativePath = @"..\..\..\..\InputData\";

        string newline = Environment.NewLine; // == "\r\n" on Windows; "\n" on Linux, MacOS
        /// <summary>
        /// Use for your Part1 or Part2 debug output, if needed.
        /// Visualization output, likewise, should be conditional on this value.
        /// </summary>
        public bool Debug { get; set; } = true;
        /// <summary>
        /// This used as header for output.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Execute Part1 and Part2, print out results with profile data, pause for user to gaze
        /// </summary>
        /// 
        protected string[] ReadFile(string FilePath, bool RemoveEmptyLines = false) {
            return File.ReadAllText(_relativePath + FilePath).Replace("\r", string.Empty).Split(new char[] { '\n' }, RemoveEmptyLines ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }
        public virtual void Run()
        {
            Stopwatch outer_stopwatch = Stopwatch.StartNew();
            // Part 1
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Restart();
            string part1 = Part1();
            double part1ms = stopwatch.Elapsed.TotalMilliseconds;
            // Part 2
            stopwatch.Restart();
            string part2 = Part2();
            double part2ms = stopwatch.Elapsed.TotalMilliseconds;
            // Console output
            bool part1IsNull = part1.Length == 0;
            bool part2IsNull = part2.Length == 0;
            Console.WriteLine();
            Console.WriteLine(Title);
            if (part1IsNull && part2IsNull) Console.WriteLine($"{newline}No solutions found. Implement puzzle code in Part1() and Part2()");
            else
            {
                if (Debug) Console.WriteLine("(Warning: Debug == true.  Set to false for best case profile data.)");
                if (part1IsNull)
                {
                    if (Debug) Console.WriteLine("Part1 not implemented.  Skipping.");
                } else
                {
                    Console.WriteLine($"{newline}Part 1 solution is {part1}");
                    Console.WriteLine($"Part 1 executed in {part1ms.ToString("0.00", CultureInfo.InvariantCulture)}ms");
                }
                if (part2IsNull)
                {
                    if (Debug) Console.WriteLine("Part2 not implemented.  Skipping.");
                } else
                {
                    Console.WriteLine($"{newline}Part 2 solution is {part2}");
                    Console.WriteLine($"Part 2 executed in {part2ms.ToString("0.00", CultureInfo.InvariantCulture)}ms");
                }
            }
            double entireRunms = outer_stopwatch.Elapsed.TotalMilliseconds;
            Console.WriteLine($"{newline}Entire run executed in {entireRunms.ToString("0.00", CultureInfo.InvariantCulture)}ms");

            //Console.WriteLine($"{newline}Hit any key to close this window...");
            //Console.ReadKey(true);
        }
        /// <summary>
        /// Use class data (extracted from input) to solve Part 1 of the puzzle.
        /// </summary>
        /// <returns>Part 1 solution as string</returns>
        public virtual string Part1()
        {
            return "";
        }
        /// <summary>
        /// Use class data (extracted from input or calculated in Part 1) to solve Part 2 of the puzzle.
        /// </summary>
        /// <returns>Part 2 solution as string</returns>
        public virtual string Part2()
        {
            return "";
        }
    }
}
