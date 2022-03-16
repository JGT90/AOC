using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Functions {
    public class Day04 : AdventOfCode.AdventOfCode {
        string mString = "iwrupvqb";
        public override string DoPartA() {
            for (int i = 0; i < 1000000; i++) {
                if (CreateHash($"{mString}{i}").StartsWith("00000")) return $"{i}";
            }
            return "";
        }

        public override string DoPartB() {
            for (int i = 0; i < 10000000; i++) {
                if (CreateHash($"{mString}{i}").StartsWith("000000")) return $"{i}";
            }
            return "";
        }

        private string CreateHash(string source) {
            using (var md5Hash = MD5.Create()) {
                // Byte array representation of source string
                var sourceBytes = Encoding.UTF8.GetBytes(source);

                // Generate hash value(Byte Array) for input data
                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert hash byte array to string
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                // Output the MD5 hash
                //Console.WriteLine("The MD5 hash of " + source + " is: " + hash);
                return hash;
            }
        }

        public override void ReadIn() {
            throw new NotImplementedException();
        }
    }
}
