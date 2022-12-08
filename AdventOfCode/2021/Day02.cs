namespace AdventOfCode {
    internal class Day02 : DayN {
        const string FORWARD = "forward";
        const string DOWN = "down";
        const string UP = "up";
        string[] mLines;
        public override string Part1() {
            int lHorizontal = 0;
            int lVertical = 0;
            mLines = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week02.txt");
            for (int i = 0; i < mLines.Length; i++) {
                if (mLines[i].Contains(FORWARD)) lHorizontal += int.Parse(mLines[i].Remove(0, FORWARD.Length + 1));
                else if (mLines[i].Contains(DOWN)) lVertical += int.Parse(mLines[i].Remove(0, DOWN.Length + 1));
                else if (mLines[i].Contains(UP)) lVertical -= int.Parse(mLines[i].Remove(0, UP.Length + 1));
            }
            //Console.Write("Week02 - PartA: ");
            //Console.WriteLine(lHorizontal * lVertical);
            return (lHorizontal * lVertical).ToString();
        }

        public override string Part2() {
            int lHorizontal = 0;
            int lVertical = 0;
            int lAim = 0;
            mLines = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week02.txt");
            for (int i = 0; i < mLines.Length; i++) {
                if (mLines[i].Contains(FORWARD)) {
                    lHorizontal += int.Parse(mLines[i].Remove(0, FORWARD.Length + 1));
                    lVertical += lAim * int.Parse(mLines[i].Remove(0, FORWARD.Length + 1));
                } else if (mLines[i].Contains(DOWN)) lAim += int.Parse(mLines[i].Remove(0, DOWN.Length + 1));
                else if (mLines[i].Contains(UP)) lAim -= int.Parse(mLines[i].Remove(0, UP.Length + 1));
            }
            //Console.Write("Week02 - PartB: ");
            //Console.WriteLine(lHorizontal * lVertical);
            return (lHorizontal * lVertical).ToString();
        }
    }
}
