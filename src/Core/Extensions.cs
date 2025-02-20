using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IdentityProvider.Core
{
    public static class Extensions
    {
        public static void RemoveRange<T>(this List<T> source, IEnumerable<T> rangeToRemove)
        {
            var toRemove = rangeToRemove as T[] ?? rangeToRemove.ToArray();
            if (!toRemove.Any())
                return;

            foreach (var item in toRemove) source.Remove(item);
        }

        public static string ToTwoChar(this string num)
        {
            return num.Length switch
            {
                1 => @"0" + num,
                _ => num
            };
        }

        public static string ToThreeChar(this string num)
        {
            return num.Length switch
            {
                1 => @"00" + num,
                2 => @"0" + num,
                _ => num
            };
        }

        public static string ToFourChar(this string num)
        {
            return num.Length switch
            {
                1 => @"000" + num,
                2 => @"00" + num,
                3 => @"0" + num,
                _ => num
            };
        }

        public static string ToFiveChar(this string num)
        {
            return num.Length switch
            {
                1 => @"0000" + num,
                2 => @"000" + num,
                3 => @"00" + num,
                4 => @"0" + num,
                _ => num
            };
        }

        public static string ToSixChar(this string num)
        {
            return num.Length switch
            {
                1 => @"00000" + num,
                2 => @"0000" + num,
                3 => @"000" + num,
                4 => @"00" + num,
                5 => @"0" + num,
                _ => num
            };
        }

        public static string ToSevenChar(this string num)
        {
            return num.Length switch
            {
                1 => @"000000" + num,
                2 => @"00000" + num,
                3 => @"0000" + num,
                4 => @"000" + num,
                5 => @"00" + num,
                6 => @"0" + num,
                _ => num
            };
        }

        public static string ToEightChar(this string num)
        {
            return num.Length switch
            {
                1 => @"0000000" + num,
                2 => @"000000" + num,
                3 => @"00000" + num,
                4 => @"0000" + num,
                5 => @"000" + num,
                6 => @"00" + num,
                7 => @"0" + num,
                _ => num
            };
        }

        public static string ToNineChar(this string num)
        {
            return num.Length switch
            {
                1 => @"00000000" + num,
                2 => @"0000000" + num,
                3 => @"000000" + num,
                4 => @"00000" + num,
                5 => @"0000" + num,
                6 => @"000" + num,
                7 => @"00" + num,
                8 => @"0" + num,
                _ => num
            };
        }

        public static string ToTenChar(this string num)
        {
            return num.Length switch
            {
                1 => @"000000000" + num,
                2 => @"00000000" + num,
                3 => @"0000000" + num,
                4 => @"000000" + num,
                5 => @"00000" + num,
                6 => @"0000" + num,
                7 => @"000" + num,
                8 => @"00" + num,
                9 => @"0" + num,
                _ => num
            };
        }

        public static string ToCharLength(this string num, string character, int length)
        {
            var stringLength = num.Length;
            if (stringLength >= length)
                return num;

            var remainingLength = length - num.Length;
            var rtn = num;
            for (var i = 0; i < remainingLength; i++) rtn += $"{character}";

            return rtn;
        }

        public static string ToTenSpaces(this string num)
        {
            return num.Length switch
            {
                1 => @"         " + num,
                2 => @"        " + num,
                3 => @"       " + num,
                4 => @"      " + num,
                5 => @"     " + num,
                6 => @"    " + num,
                7 => @"   " + num,
                8 => @"  " + num,
                9 => @" " + num,
                _ => num
            };
        }

        public static string ToTitleCase(this string s)
        {
            if (s == null) return string.Empty;

            var words = s.Split(' ');
            for (var i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;

                var firstChar = char.ToUpper(words[i][0]);
                var rest = "";
                if (words[i].Length > 1)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            return string.Join(" ", words).Trim();
        }

        public static double ToNumValue(this object obj)
        {
            try
            {
                double.TryParse(obj.ToString(), out var i);
                return i;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int ToIntegerValue(this object obj)
        {
            try
            {
                int.TryParse(obj.ToString(), out var i);
                return i;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double ToDecimalValue(this string value)
        {
            try
            {
                var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                value = value.Replace(".", separator).Replace(",", separator);
                return Convert.ToDouble(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double ToRoundedUpValue(this double amount, int rounding)
        {
            if (amount % rounding == 0)
                return amount;

            var result = (int)Math.Round((amount + 0.5 * rounding) / rounding);
            if (amount > 0 && result == 0) result = 1;

            return result * rounding;
        }

        public static double ToRoundedDownValue(this double amount, int rounding)
        {
            var result = (int)Math.Round((amount - 0.5 * rounding) / rounding);
            if (amount > 0 && result == 0) result = 1;
            return result * rounding;
        }

        public static double ToRoundedValue(this double amount, int rounding)
        {
            var result = (int)Math.Round(amount / rounding);
            if (amount > 0 && result == 0) result = 1;
            return result * rounding;
        }

        public static DateTime ToDateTime(this string stringDate) => Convert.ToDateTime(stringDate);

        public static DateTime ToMonthEndDate(this DateTime date)
        {
            var year = date.Year;
            var month = date.Month;
            return new DateTime(year, month, DateTime.DaysInMonth(year, month)).ToDateWithoutTime();
        }

        public static DateTime ToMonthStartDate(this DateTime date)
        {
            var year = date.Year;
            var month = date.Month;
            return new DateTime(year, month, 1).ToDateWithoutTime();
        }

        public static DateTime ToDateWithoutTime(this DateTime date) =>
            new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

        public static string ToDatabaseDateString(this DateTime dateTime) =>
            $"CAST(N'{dateTime.Year.ToString().ToFourChar()}-{dateTime.Month.ToString().ToTwoChar()}-{dateTime.Day.ToString().ToTwoChar()} 00:00:00.000' AS DateTime)";

        public static string ToMaxLength(this string num, int len)
        {
            if (string.IsNullOrEmpty(num)) return string.Empty;
            //return num.Trim().Length <= len ? num.Trim() : num.Trim().Substring(0, len);
            return num.Trim().Length <= len ? num.Trim() : num.Trim()[..len];
        }

        public static string ToDayMonthYearFormat(this DateTime num) => num.Date == DateTime.MinValue
            ? string.Empty
            : $"{num.Day.ToString().ToTwoChar()}/{num.Month.ToString().ToTwoChar()}/{num.Year.ToString().ToFourChar()}";

        public static string ClearSeparatorCharacter(this string dirtyString, string separator) =>
            string.IsNullOrEmpty(dirtyString) ? string.Empty : dirtyString.ToNormalizeNameString().Replace(separator, string.Empty);

        public static string ToNormalizeNameString(this string name, bool upperCase = true)
        {
            var normalizedName = string.Empty;

            if (string.IsNullOrEmpty(name)) return normalizedName;

            var source = "ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïðñòóôõöùúûüýÿ".ToCharArray();
            var target = "AAAAAACEEEEIIIINOOOOOUUUUYaaaaaaceeeeiiiionooooouuuuyy".ToCharArray();

            normalizedName = name.Replace("œ", "oe").Replace("Œ", "OE").Replace("æ", "ae").Replace("Æ", "AE").Replace("°", "o")
                .Replace("\r\n", "").Replace("\"", "").Replace("  ", " ");

            for (var i = 0; i < source.Length - 1; i++)
            {
                normalizedName = normalizedName.Replace(source[i], target[i]);
            }

            if (upperCase) normalizedName = normalizedName.ToUpper();

            return normalizedName;
        }
    }
}
