using System;
namespace oneapp.Utilities
{
    public static class SystemHelper
    {
        public static DateTimeOffset GetCurrentDate()
        { return DateTimeOffset.UtcNow; }

        public static Guid GetCurrentUser()
        { return new Guid("099cdfc5-82d2-4ddb-b7e5-84aef3f86c80"); }

        public static string ConvertDateTimeToString(DateTimeOffset dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string ConvertDateToString(DateTimeOffset dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
        public static string CalculateTimeDifference(DateTimeOffset startDateTime, DateTimeOffset endDateTime)
        {
            TimeSpan timeDiff = endDateTime - startDateTime;

            int years = timeDiff.Days / 365;
            int months = (timeDiff.Days % 365) / 30;
            int days = timeDiff.Days % 30;
            int hours = timeDiff.Hours;
            int minutes = timeDiff.Minutes;

            if (years != 0)
            {
                return string.Join("", years, "y");
            }
            if (months != 0)
            {
                return string.Join("", months, "m");
            }
            if (days != 0)
            {
                return string.Join("", days, "d");
            }
            if (hours != 0)
            {
                return string.Join("", hours, "h");
            }
            else
            {
                return string.Join("", minutes, "m");
            }
        }
    }
}

