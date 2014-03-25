namespace Common.Extensions
{
    using System;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static DateTime? ParseToNullableDateTime(this string dateTimeStringToParse)
        {
            DateTime parsed;
            var success = DateTime.TryParse(dateTimeStringToParse, out parsed);

            if (success)
            {
                return parsed;
            }

            return null;
        }

        public static int? ParseToNullableInt(this string intString)
        {
            int parsed;
            var success = int.TryParse(intString, out parsed);

            if (success)
            {
                return parsed;
            }

            return null;
        }

        public static string SplitByCamelCase(this string stringToSplit)
        {
            var result = Regex.Replace(
                stringToSplit, 
                @"(?<a>(?<!^)((?:[A-Z][a-z])|(?:(?<!^[A-Z]+)[A-Z0-9]+(?:(?=[A-Z][a-z])|$))|(?:[0-9]+)))", 
                @" ${a}");
            return result;
        }
    }
}