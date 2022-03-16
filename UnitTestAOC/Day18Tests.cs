using AdventOfCode2021.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestAOC {
    [TestClass]
    public class Day18Tests {
        [TestMethod]
        public void Test01() {
            List<string> Summands = new List<string>();
            Summands.Add("[1,1]");
            Summands.Add("[2,2]");
            Summands.Add("[3,3]");
            Summands.Add("[4,4]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[[[1,1],[2,2]],[3,3]],[4,4]]");
        }
        [TestMethod]
        public void Test02() {
            List<string> Summands = new List<string>();
            Summands.Add("[1,1]");
            Summands.Add("[2,2]");
            Summands.Add("[3,3]");
            Summands.Add("[4,4]");
            Summands.Add("[5,5]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[[[3,0],[5,3]],[4,4]],[5,5]]");
        }
        [TestMethod]
        public void Test03() {
            List<string> Summands = new List<string>();
            Summands.Add("[1,1]");
            Summands.Add("[2,2]");
            Summands.Add("[3,3]");
            Summands.Add("[4,4]");
            Summands.Add("[5,5]");
            Summands.Add("[6,6]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[[[5,0],[7,4]],[5,5]],[6,6]]");
        }
        [TestMethod]
        public void Test04() {
            List<string> Summands = new List<string>();
            Summands.Add("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]");
            Summands.Add("[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]");
            Summands.Add("[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]");
            Summands.Add("[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]");
            Summands.Add("[7,[5,[[3,8],[1,4]]]]");
            Summands.Add("[[2,[2,2]],[8,[8,1]]]");
            Summands.Add("[2,9]");
            Summands.Add("[1,[[[9,3],9],[[9,0],[0,7]]]]");
            Summands.Add("[[[5,[7,4]],7],1]");
            Summands.Add("[[[[4,2],2],6],[8,7]]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]");
        }
        [TestMethod]
        public void Test04A() {
            List<string> Summands = new List<string>();
            Summands.Add("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]");
            Summands.Add("[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]");
        }
        [TestMethod]
        public void Test04B() {
            List<string> Summands = new List<string>();
            Summands.Add("[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]");
            Summands.Add("[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]");
        }
        [TestMethod]
        public void Test04C() {
            List<string> Summands = new List<string>();
            Summands.Add("[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]");
            Summands.Add("[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]]");
        }
        [TestMethod]
        public void Test04F() {
            List<string> Summands = new List<string>();
            Summands.Add("[[[[6,6],[6,6]],[[6,0],[6,7]]],[[[7,7],[8,9]],[8,[8,1]]]]");
            Summands.Add("[2,9]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[[[6,6],[7,7]],[[0,7],[7,7]]],[[[5,5],[5,6]],9]]");
        }
        [TestMethod]
        public void Test05() {
            List<string> Summands = new List<string>();
            Summands.Add("[[[[4,3],4],4],[7,[[8,4],9]]]");
            Summands.Add("[1,1]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]");
        }
        [TestMethod]
        public void Test06() {
            List<string>Summands = new List<string>();
            Summands.Add("[[[[[9,8],1],2],3],4]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[[[0,9],2],3],4]");
        }
        [TestMethod]
        public void Test07() {
            List<string>Summands = new List<string>();
            Summands.Add("[7,[6,[5,[4,[3,2]]]]]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[7,[6,[5,[7,0]]]]");
        }
        [TestMethod]
        public void Test08() {
            List<string> Summands = new List<string>();
            Summands.Add("[[6,[5,[4,[3,2]]]],1]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[6,[5,[7,0]]],3]");
        }
        [TestMethod]
        public void Test09() {
            List<string> Summands = new List<string>();
            Summands.Add("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[3,[2,[8,0]]],[9,[5,[7,0]]]]");
        }
        [TestMethod]
        public void Test10() {
            List<string> Summands = new List<string>();
            Summands.Add("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
            string actual = Day18.SnailfishAddition(Summands);
            Assert.AreEqual<string>(actual, "[[3,[2,[8,0]]],[9,[5,[7,0]]]]");
        }

        [TestMethod]
        public void Magnitude01() {
            int actual = Day18.GetMagnitude("[[1,2],[[3,4],5]]");
            Assert.AreEqual<int>(actual, 143);
        }
        [TestMethod]
        public void Magnitude02() {
            int actual = Day18.GetMagnitude("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]");
            Assert.AreEqual<int>(actual, 1384);
        }
        [TestMethod]
        public void Magnitude03() {
            int actual = Day18.GetMagnitude("[[[[1,1],[2,2]],[3,3]],[4,4]]");
            Assert.AreEqual<int>(actual, 445);
        }
        [TestMethod]
        public void Magnitude04() {
            int actual = Day18.GetMagnitude("[[[[3,0],[5,3]],[4,4]],[5,5]]");
            Assert.AreEqual<int>(actual, 791);
        }
        [TestMethod]
        public void Magnitude05() {
            int actual = Day18.GetMagnitude("[[[[5,0],[7,4]],[5,5]],[6,6]]");
            Assert.AreEqual<int>(actual, 1137);
        }
        [TestMethod]
        public void Magnitude06() {
            int actual = Day18.GetMagnitude("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]");
            Assert.AreEqual<int>(actual, 3488);
        }
        [TestMethod]
        public void LargestTest01() {
            List<string> Summands = new List<string>();
            Summands.Add("[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]");
            Summands.Add("[[[5,[2,8]],4],[5,[[9,9],0]]]");
            Summands.Add("[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]");
            Summands.Add("[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]");
            Summands.Add("[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]");
            Summands.Add("[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]");
            Summands.Add("[[[[5,4],[7,7]],8],[[8,3],8]]");
            Summands.Add("[[9,3],[[9,9],[6,[4,9]]]]");
            Summands.Add("[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]");
            Summands.Add("[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]");
            int actual = Day18.LargestMagnitude(Summands);
            Assert.AreEqual<int>(actual, 3993);
        }
    }
}
 