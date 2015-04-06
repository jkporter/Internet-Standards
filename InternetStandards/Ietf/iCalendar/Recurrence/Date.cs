using NodaTime;

namespace InternetStandards.Ietf.iCalendar.Recurrence
{
    public class Date : IDate
    {
        private LocalDate _localDate;
        public Date(LocalDate localDate)
        {
            _localDate = localDate;
        }
        public int Year
        {
            get { return _localDate.Year; }
        }

        public int Month
        {
            get { return _localDate.Month; }
        }

        public int Day
        {
            get { return _localDate.Day; }
        }

        public int DayOfYear
        {
            get { return _localDate.DayOfYear; }
        }

        public IDate PlusYears(int years)
        {
            return new Date(_localDate.PlusYears(years));
        }

        public IDate PlusMonths(int months)
        {
            return new Date(_localDate.PlusMonths(months));
        }

        public IDate PlusWeeks(int weeks)
        {
            return new Date(_localDate.PlusWeeks(weeks));
        }

        public IDate PlusDays(int days)
        {
            return new Date(_localDate.PlusDays(days));
        }

        public LocalDate ToLocalDate()
        {
            return _localDate;
        }
    }
}