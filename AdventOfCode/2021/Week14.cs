using SEGCC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021 {
    internal class Week14 : DayN {
        string mStartString = string.Empty;
        Dictionary<string, string> mPolymer = new Dictionary<string, string>();
        Dictionary<string, double> mPolymerQuantity = new Dictionary<string, double>();
        Dictionary<string, double> mAppearance = new Dictionary<string, double>();
        public override string Part1() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week14.txt");
            mStartString = lInput[0];
            for (int i = 2; i < lInput.Length; i++) {
                string[] lSplit = lInput[i].Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                mPolymer.Add(lSplit[0].Trim(), lSplit[1].Trim());
            }
            return $"{GetPolymerQuantity(mStartString, 10)}";
        }

        public override string Part2() {
            return $"{GetPolymerQuantity(mStartString, 40)}";
        }

        private double GetPolymerQuantity(string aStartString, int aSteps) {
            InitAppearance();
            mPolymerQuantity = new Dictionary<string, double>();
            for (int i = 0; i < aStartString.Length - 1; i++) {
                try {
                    mPolymerQuantity.Add(aStartString.Substring(i, 2), 1);
                } catch (Exception) {
                    mPolymerQuantity[aStartString.Substring(i, 2)]++;
                }
                mAppearance[aStartString[i].ToString()]++;
            }
            mAppearance[aStartString[aStartString.Length - 1].ToString()]++;
            for (int i = 0; i < aSteps; i++) {
                Dictionary<string, double> lTemp = new Dictionary<string, double>();
                foreach(var item in mPolymerQuantity) {
                    string lAdjacent = mPolymer[item.Key];
                    mAppearance[lAdjacent]+=item.Value;
                    try {
                        lTemp[$"{item.Key[0]}{lAdjacent}"]+= item.Value;
                    } catch(Exception) {
                        lTemp.Add($"{item.Key[0]}{lAdjacent}", item.Value);
                    }
                    try {
                        lTemp[$"{lAdjacent}{item.Key[1]}"] += item.Value;
                    } catch (Exception) {
                        lTemp.Add($"{lAdjacent}{item.Key[1]}", item.Value);
                    }
                }
                mPolymerQuantity = lTemp;
            }
            RemoveZeroAppearance();
            return mAppearance.Values.Max() - mAppearance.Values.Min();
        }

        private void InitAppearance() {
            mAppearance.Clear();
            mAppearance.Add("A", 0);
            mAppearance.Add("B", 0);
            mAppearance.Add("C", 0);
            mAppearance.Add("D", 0);
            mAppearance.Add("E", 0);
            mAppearance.Add("F", 0);
            mAppearance.Add("G", 0);
            mAppearance.Add("H", 0);
            mAppearance.Add("I", 0);
            mAppearance.Add("J", 0);
            mAppearance.Add("K", 0);
            mAppearance.Add("L", 0);
            mAppearance.Add("M", 0);
            mAppearance.Add("N", 0);
            mAppearance.Add("O", 0);
            mAppearance.Add("P", 0);
            mAppearance.Add("Q", 0);
            mAppearance.Add("R", 0);
            mAppearance.Add("S", 0);
            mAppearance.Add("T", 0);
            mAppearance.Add("U", 0);
            mAppearance.Add("V", 0);
            mAppearance.Add("W", 0);
            mAppearance.Add("X", 0);
            mAppearance.Add("Y", 0);
            mAppearance.Add("Z", 0);
        }

        private void RemoveZeroAppearance() {
            foreach (var item in mAppearance.Where(kvp => kvp.Value == 0).ToList()) {
                mAppearance.Remove(item.Key);
            }
        }
    }
}