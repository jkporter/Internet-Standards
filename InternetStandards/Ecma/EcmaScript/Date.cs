using System;
using System.Collections.Generic;
using System.Text;

namespace InternetStandards.Ecma.EcmaScript
{
    public struct Date
    {
        private const int msPerDay = 86400000;

        private double Day(double t)
        {
            return Math.Floor(t) / msPerDay;
        }

        private double TimeWithinDay(double t)
        {
            return t % msPerDay;
        }

        private double DaysInYear(double y)
        {
            double leapYearTestValue = y % 4;
            if (y % 4 != 0 || (y % 100 == 0 & y % 400 != 0))
                return 365;

            if (y % 4 != 0 || (y % 100 == 0 & y % 400 != 0))
                return 366;

            return 0;
        }

        private double DayFromYear(double y)
        {
            return 365 * (y - 1970) + Math.Floor((y - 1969) / 4) - Math.Floor((y - 1901) / 100) + Math.Floor((y - 1601) / 400);
        }

        private double TimeFromYear(double y)
        {
            return msPerDay * DayFromYear(y);
        }

        private double YearFromTime(double t)
        {
            //TimeFromYear(y) <= t
            return 1;
        }

        private double InLeapYear(double t)
        {
            if (DaysInYear(YearFromTime(t)) == 365)
                return 0;
            if (DaysInYear(YearFromTime(t)) == 366)
                return 1;

            return -1;
        }

        private double MonthFromTime(double t)
        {
            double dayWithinYear = DayWithinYear(t);
            if (0 <= dayWithinYear & dayWithinYear < 31)
                return 0;
            if (31 <= dayWithinYear && dayWithinYear < 59 + InLeapYear(t))
                return 1;
            if (59 + InLeapYear(t) <= dayWithinYear && dayWithinYear < 90 + InLeapYear(t))
                return 2;
            if (90 + InLeapYear(t) <= dayWithinYear && dayWithinYear < 120 + InLeapYear(t))
                return 3;
            if (120 + InLeapYear(t) <= dayWithinYear && dayWithinYear < 151 + InLeapYear(t))
                return 4;
            if (151 + InLeapYear(t) <= dayWithinYear && dayWithinYear < 181 + InLeapYear(t))
                return 5;
            if (181 + InLeapYear(t) <= dayWithinYear && dayWithinYear < 212 + InLeapYear(t))
                return 6;
            if (212 + InLeapYear(t) <= dayWithinYear && dayWithinYear < 243 + InLeapYear(t))
                return 7;
            if (243 + InLeapYear(t) <= dayWithinYear && dayWithinYear < 273 + InLeapYear(t))
                return 8;
            if (273 + InLeapYear(t) <= dayWithinYear && dayWithinYear < 304 + InLeapYear(t))
                return 9;
            if (304 + InLeapYear(t) <= dayWithinYear && dayWithinYear < 334 + InLeapYear(t))
                return 10;
            if (334 + InLeapYear(t) <= dayWithinYear && dayWithinYear < 365 + InLeapYear(t))
                return 11;

            return -1;
        }

        private double DayWithinYear(double t)
        {
            return Day(t) - DayFromYear(YearFromTime(t));
        }

        private double Weekday(double t)
        {
            return (Day(t) + 4) % 7;
        }

        private double LocalTza()
        {
            throw new NotImplementedException();
        }

        private double DaylightSavingTA(double t)
        {
            throw new NotImplementedException();
        }
    }
}
