using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Functions {
    public static class Transfer {
        
        public static void DoTransfer(string aPath) {
            List<string> lList = ReadIn(aPath);
            StringBuilder sb = new StringBuilder();
            foreach(string line in lList) {
                if (line.StartsWith("//") || string.IsNullOrEmpty(line)) continue;
                sb.Append($@"<< {Convert.ToChar(34)}\{Convert.ToChar(34)}");
                string[] split = line.Split(new string[] { " ", ";" }, StringSplitOptions.RemoveEmptyEntries);
                bool IsString = false;
                bool next = false;
                switch(split[0]) {
                    case "int":
                    case "float":
                    case "double":
                    case "SHORT":
                    case "short":
                    case "BOOL":
                    case "UINT":
                        sb.Append(split[1]);
                        sb.Append($@"\{Convert.ToChar(34)}:{Convert.ToChar(34)}");
                        break;
                    case "unsigned":
                        next = true;
                        sb.Append(split[2]);
                        sb.Append($@"\{Convert.ToChar(34)}:{Convert.ToChar(34)}");
                        break;
                    default:
                        IsString = true;
                        sb.Append(split[1]);
                        sb.Append($@"\{Convert.ToChar(34)}:\{Convert.ToChar(34)}{Convert.ToChar(34)}");
                        break;

                }
                if (IsString) sb.AppendLine($@" << aAPU.{split[1]} << {Convert.ToChar(34)}\{ Convert.ToChar(34)},{Convert.ToChar(34)}							// {line}");
                else if (next) sb.AppendLine($@" << aAPU.{split[2]} << {Convert.ToChar(34)},{Convert.ToChar(34)}							// {line}");
                else sb.AppendLine($@" << aAPU.{split[1]} << {Convert.ToChar(34)},{Convert.ToChar(34)}							// {line}");
            }
            File.WriteAllText(aPath, sb.ToString());
        }
        private static List<string> ReadIn(string aPath) {
            List<string> lResult = new List<string>();
            if (!File.Exists(Path.GetFullPath(aPath))) {
                Console.WriteLine($"File does not exist, {aPath}");
                return lResult;
            }
            foreach (string lLine in System.IO.File.ReadLines(aPath)) {
                lResult.Add(lLine);
            }
            Console.WriteLine($"{lResult.Count} values found in file, {aPath}");
            return lResult;
        }
    }
}
