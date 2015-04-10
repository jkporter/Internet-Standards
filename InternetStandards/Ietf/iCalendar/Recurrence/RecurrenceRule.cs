using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace InternetStandards.Ietf.iCalendar.Recurrence
{
    public class RecurrenceRule<T> : RecurrenceRule
    {
        public RecurrenceRule(Frequency frequency) : base(frequency)
        {
        }

        public new T Until
        {
            get { return (T) base.Until; }
            set { base.Until = value; }
        }
    }
    public class RecurrenceRule
    {
        public RecurrenceRule(Frequency frequency)
        {
            Frequency = frequency;
        }
        public Frequency Frequency { get; set; }

        public object Until { get; set; }

        public int? Count { get; set; }

        public int? Interval { get; set; }

        public IEnumerable<int> BySecond { get; set; }

        public IEnumerable<int> ByMinute { get; set; }

        public IEnumerable<int> ByHour { get; set; }

        public IEnumerable<Tuple<int?, IsoDayOfWeek>> ByDay { get; set; }

        public IEnumerable<int> ByMonthDay{ get; set; }

        public IEnumerable<int> ByYearDay { get; set; }

        public IEnumerable<int> ByWeekNumber { get; set; }

        public IEnumerable<int> ByMonth { get; set; }

        public IEnumerable<int> BySetPosition { get; set; }

        public IsoDayOfWeek? WorkWeekStart { get; set; }
    }

    public enum Frequency
    {
        Secondly,
        Minutely,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }

    public enum Weekday
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
}
