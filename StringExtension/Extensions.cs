﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExtension
{
    public static class Extensions
    {
        public static bool IsBinary(this string s)
        {
            bool x;
            return bool.TryParse(s,out x);
        }

        public static bool IsNum(this string s)
        {
            int x;
            return int.TryParse(s, out x);
        }

        public static bool IsDec(this string s)
        {
            double x;
            return double.TryParse(s, out x);
        }

        public static bool ContainsFromList(this string s, List<string> source)
        {
            return source.Any(s.Contains);
        }

        /// <summary>
        /// Checks the order of two substrings in a string
        /// </summary>
        /// <param name="s">String to be checked</param>
        /// <param name="toCheck"></param>
        /// <param name="toCheck2"></param>
        /// <returns>True if a comes before b or b is not included in the string</returns>
        public static bool CheckOrder(this string s, string toCheck, string toCheck2)
        {
            var a = s.IndexOf(toCheck);
            var b = s.IndexOf(toCheck2);

            if (a == -1)
            {
                return false;
            }

            if (b == -1)
            {
                return true;
            }

            if (a < b)
            {
                return true;
            }

            if (a > b)
            {
                return false;
            }
            return false;
        }
    }
}
