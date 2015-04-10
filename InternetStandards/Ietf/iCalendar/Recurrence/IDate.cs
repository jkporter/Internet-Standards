using NodaTime;

namespace InternetStandards.Ietf.iCalendar.Recurrence
{
    public interface IDate<out T> : IDate where T : IDate
    {
        new T PlusYears(int years);

        new T PlusMonths(int months);

        new T PlusWeeks(int weeks);

        new T PlusDays(int days);
    }

    public interface IDate
    {
        int Year { get; }

        int Month { get; }

        int Day { get; }

        int DayOfYear { get; }

        IsoDayOfWeek DayOfWeek { get; }

        IDate PlusYears(int years);

        IDate PlusMonths(int months);

        IDate PlusWeeks(int weeks);

        IDate PlusDays(int days);

        LocalDate ToLocalDate();
    }
}