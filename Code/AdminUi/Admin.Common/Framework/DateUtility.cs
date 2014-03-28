namespace Common.Framework
{
    using System;

    public static class DateUtility
    {
        public static string DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.fffffffZ";

        static DateUtility()
        {
            MinDate = new DateTime(1753, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            MaxDate = new DateTime(9999, 12, 31, 0, 0, 0, DateTimeKind.Utc);
        }

        public static DateTime MaxDate { get; private set; }

        public static DateTime MinDate { get; private set; }

        public static DateTime Round(DateTime value)
        {
            if (value < MinDate)
            {
                return MinDate;
            }

            if (value > MaxDate)
            {
                return MaxDate;
            }

            // Strip the milliseconds - 10000 ticks = 1ms
            const long Stripms = 10000000;
            long ticks = value.Ticks / Stripms;
            var newValue = new DateTime(ticks * Stripms, value.Kind);

            return newValue;
        }
    }
}