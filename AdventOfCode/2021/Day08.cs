using SEGCC;
using System;

namespace AOC2021 {
    internal class Day08 : DayN {
        static string[] mSignalPattern;
        static string[] mOutputValues;

        public override string Part1() {
            string[] lLines = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week08.txt");
            mSignalPattern = new string[lLines.Length];
            mOutputValues = new string[lLines.Length];
            for (int i = 0; i < lLines.Length; i++) {
                string[] lSplit = lLines[i].Split('|');
                mSignalPattern[i] = lSplit[0];
                mOutputValues[i] = lSplit[1];
            }
            int lUniqueNumberOfSegments = 0;
            for (int i = 0; i < mOutputValues.Length; i++) {
                string[] lNumbers = mOutputValues[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < lNumbers.Length; j++) {
                    if (lNumbers[j].Length == 2) lUniqueNumberOfSegments++;
                    else if (lNumbers[j].Length == 3) lUniqueNumberOfSegments++;
                    else if (lNumbers[j].Length == 4) lUniqueNumberOfSegments++;
                    else if (lNumbers[j].Length == 7) lUniqueNumberOfSegments++;
                }
            }
            //Console.Write("Week08 - PartA: ");
            //Console.WriteLine(lUniqueNumberOfSegments);
            return lUniqueNumberOfSegments.ToString();
        }

        public override string Part2() {
            double lOutputValueSum = 0;
            for (int i = 0; i < mSignalPattern.Length; i++) {
                string lOneString = string.Empty;
                string lFourString = string.Empty;
                string lSevenString = string.Empty;
                string[] lSignalNumbers = mSignalPattern[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < lSignalNumbers.Length; j++) {
                    if (lSignalNumbers[j].Length == 2) {
                        lOneString = lSignalNumbers[j];
                    } else if (lSignalNumbers[j].Length == 3) {
                        lSevenString = lSignalNumbers[j];
                    } else if (lSignalNumbers[j].Length == 4) {
                        lFourString = lSignalNumbers[j];
                    }
                }
                string[] lNumbers = mOutputValues[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                double lOutputValue = 0;
                for (int j = 0; j < lNumbers.Length; j++) {
                    if (lNumbers[j].Length == 2) {
                        lOutputValue += 1 * Math.Pow(10, 3 - j);
                    } else if (lNumbers[j].Length == 3) {
                        lOutputValue += 7 * Math.Pow(10, 3 - j);
                    } else if (lNumbers[j].Length == 4) {
                        lOutputValue += 4 * Math.Pow(10, 3 - j);
                    } else if (lNumbers[j].Length == 5) { // Check if it is 2, 5 or 3
                        if (lOneString != string.Empty && lNumbers[j].Contains(lOneString[0].ToString()) && lNumbers[j].Contains(lOneString[1].ToString())) lOutputValue += 3 * Math.Pow(10, 3 - j);
                        else if (lOneString != String.Empty && lFourString != String.Empty) {
                            string lMidTopLeft = RemoveCharactersFromString(lFourString, new char[] { lOneString[0], lOneString[1] });
                            if (lNumbers[j].Contains(lMidTopLeft[0].ToString()) && lNumbers[j].Contains(lMidTopLeft[1].ToString())) lOutputValue += 5 * Math.Pow(10, 3 - j);
                            else lOutputValue += 2 * Math.Pow(10, 3 - j);
                        }
                    } else if (lNumbers[j].Length == 6) { // Check if it is 0, 6 or 9 
                        if (lFourString != String.Empty && lNumbers[j].Contains(lFourString[0].ToString()) && lNumbers[j].Contains(lFourString[1].ToString()) && lNumbers[j].Contains(lFourString[2].ToString()) && lNumbers[j].Contains(lFourString[3].ToString())) lOutputValue += 9 * Math.Pow(10, 3 - j);
                        else if (lOneString != string.Empty && lNumbers[j].Contains(lOneString[0].ToString()) && lNumbers[j].Contains(lOneString[1].ToString())) lOutputValue += 0 * Math.Pow(10, 3 - j);
                        else lOutputValue += 6 * Math.Pow(10, 3 - j);
                    } else if (lNumbers[j].Length == 7) lOutputValue += 8 * Math.Pow(10, 3 - j);
                }
                lOutputValueSum += lOutputValue;
            }
            //Console.Write("Week08 - PartB: ");
            //Console.WriteLine(lOutputValueSum);
            return lOutputValueSum.ToString();
        }

        private string RemoveCharactersFromString(string aString, char[] aCharacters) {
            for (int i = 0; i < aCharacters.Length; i++) {
                aString = aString.Remove(aString.IndexOf(aCharacters[i]), 1);
            }
            return aString;
        }
    }
}