using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace InternetStandards.Ietf.iCalendar.Recurrence
{
    public class DateTime : IDateTime
    {
        private LocalDateTime _localDateTime;
        public DateTime(LocalDateTime localDateTime)
        {
            _localDateTime = localDateTime;
        }

        public int Hour
        {
            get { return _localDateTime.Hour; }
        }

        public int Minute
        {
            get { return _localDateTime.Minute; }
        }

        public int Second
        {
            get { return _localDateTime.Second; }
        }

        public IDateTime PlusHours(int hours)
        {
            return new DateTime(_localDateTime.PlusHours(hours));
        }

        public IDateTime PlusMinutes(int minutes)
        {
            return new DateTime(_localDateTime.PlusMonths(minutes));
        }

        public IDateTime PlusSeconds(int seconds)
        {
            return new DateTime(_localDateTime.PlusSeconds(seconds));
        }

        public LocalDateTime ToLocalDateTime()
        {
            return _localDateTime;
        }

        public IDateTime PlusYears(int years)
        {
            return new DateTime(_localDateTime.PlusYears(years));
        }

        public IDateTime PlusMonths(int months)
        {
            return new DateTime(_localDateTime.PlusMonths(months));
        }

        public IDateTime PlusWeeks(int weeks)
        {
            return new DateTime(_localDateTime.PlusWeeks(weeks));
        }

        public IDateTime PlusDays(int days)
        {
            return new DateTime(_localDateTime.PlusDays(days));
        }

        public LocalDate ToLocalDate()
        {
            return _localDateTime.Date;
        }

        public int Year
        {
            get { return _localDateTime.Year; }
        }

        public int Month
        {
            get { return _localDateTime.Month; }
        }

        public int Day
        {
            get { return _localDateTime.Day; }
        }

        public int DayOfYear
        {
            get { return _localDateTime.DayOfYear; }
        }

        IDate IDate.PlusYears(int years)
        {
            return PlusYears(years);
        }

        IDate IDate.PlusMonths(int months)
        {
            return PlusMonths(months);
        }

        IDate IDate.PlusWeeks(int weeks)
        {
            return PlusWeeks(weeks);
        }

        IDate IDate.PlusDays(int days)
        {
            return PlusDays(days);
        }
    }

}
