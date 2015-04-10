using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;

namespace InternetStandards.Ietf.iCalendar.Recurrence
{

    public static class RecurrenceSetUtility
    {

        public static IEnumerable<LocalDate> GetOccurences(RecurrenceRule recurrenceRule, LocalDate dateStart)
        {
            return GetOccurences(recurrenceRule, new Date(dateStart), null).Select(o => o.ToLocalDate());
        }

        public static IEnumerable<LocalDate> GetOccurences(RecurrenceRule<LocalDate> recurrenceRule, LocalDate dateStart)
        {
            return GetOccurences(recurrenceRule, new Date(dateStart), null).Select(o => o.ToLocalDate());
        }

        public static IEnumerable<LocalDate> GetOccurences(RecurrenceRule<LocalDate?> recurrenceRule, LocalDate dateStart)
        {
            return GetOccurences(recurrenceRule, new Date(dateStart), null).Select(o => o.ToLocalDate());
        }

        public static IEnumerable<LocalDateTime> GetOccurences(RecurrenceRule recurrenceRule, LocalDateTime dateStart)
        {
            return GetOccurences(recurrenceRule, new DateTime(dateStart), null).Select(o => o.ToLocalDateTime());
        }

        public static IEnumerable<LocalDateTime> GetOccurences(RecurrenceRule<LocalDateTime> recurrenceRule, LocalDateTime dateStart)
        {
            return GetOccurences(recurrenceRule, new DateTime(dateStart), null).Select(o => o.ToLocalDateTime());
        }

        public static IEnumerable<LocalDateTime> GetOccurences(RecurrenceRule<LocalDateTime?> recurrenceRule, LocalDateTime dateStart)
        {
            return GetOccurences(recurrenceRule, new DateTime(dateStart), null).Select(o => o.ToLocalDateTime());
        }

        public static IEnumerable<ZonedDateTime> GetOccurences(RecurrenceRule recurrenceRule, LocalDateTime dateStart, DateTimeZone dateStartTimeZone)
        {
            return
                GetOccurences(recurrenceRule, new DateTime(dateStart), dateStartTimeZone)
                    .Select(o => o.ToLocalDateTime().InZoneLeniently(dateStartTimeZone));
        }

        public static IEnumerable<ZonedDateTime> GetOccurences(RecurrenceRule<LocalDateTime> recurrenceRule, LocalDateTime dateStart, DateTimeZone dateStartTimeZone)
        {
            return
                GetOccurences(recurrenceRule, new DateTime(dateStart), dateStartTimeZone)
                    .Select(o => o.ToLocalDateTime().InZoneLeniently(dateStartTimeZone));
        }

        public static IEnumerable<ZonedDateTime> GetOccurences(RecurrenceRule<LocalDateTime?> recurrenceRule, LocalDateTime dateStart, DateTimeZone dateStartTimeZone)
        {
            return
                GetOccurences(recurrenceRule, new DateTime(dateStart), dateStartTimeZone)
                    .Select(o => o.ToLocalDateTime().InZoneLeniently(dateStartTimeZone));
        }

        private static IEnumerable<T> GetOccurences<T>(RecurrenceRule recurrenceRule, T dateStart, DateTimeZone timeZone) where T : IDate
        {
            var instances =
                Intervals(recurrenceRule.Frequency, recurrenceRule.Interval, dateStart)
                    .Select(
                        interval =>
                            GetSetOfRecurrenceInstances(recurrenceRule, interval)
                                .BySetPosition(recurrenceRule.BySetPosition))
                    .SelectMany(set => set)
                    .Count(recurrenceRule.Count)
                    .Until(recurrenceRule, timeZone);

            return instances;
        }

        private static IEnumerable<T> Intervals<T>(Frequency recurrenceRuleType, int? interval, T dateStart) where T : IDate
        {
            T previousInterval;
            yield return previousInterval = dateStart;

            while (true)
            {
                try
                {
                    switch (recurrenceRuleType)
                    {
                        case Frequency.Yearly:
                            previousInterval = (T)previousInterval.PlusYears(interval.GetValueOrDefault(1));
                            break;
                        case Frequency.Monthly:
                            previousInterval = (T)previousInterval.PlusMonths(interval.GetValueOrDefault(1));
                            break;
                        case Frequency.Weekly:
                            previousInterval = (T)previousInterval.PlusWeeks(interval.GetValueOrDefault(1));
                            break;
                        case Frequency.Daily:
                            previousInterval = (T)previousInterval.PlusDays(interval.GetValueOrDefault(1));
                            break;
                        case Frequency.Hourly:
                            previousInterval = (T)((IDateTime)previousInterval).PlusHours(interval.GetValueOrDefault(1));
                            break;
                        case Frequency.Minutely:
                            previousInterval = (T)((IDateTime)previousInterval).PlusMinutes(interval.GetValueOrDefault(1));
                            break;
                        case Frequency.Secondly:
                            previousInterval = (T)((IDateTime)previousInterval).PlusSeconds(interval.GetValueOrDefault(1));
                            break;
                    }
                }
                catch (Exception)
                {
                    yield break;
                }

                yield return previousInterval;
            }
        }

        private static IEnumerable<T> GetSetOfRecurrenceInstances<T>(RecurrenceRule recurrenceRule, T instance) where T : IDate
        {
            var set = Enumerable.Repeat(instance, 1);
            if (recurrenceRule.ByMonth != null)
                set = set.ByMonth(recurrenceRule.ByMonth, recurrenceRule.Frequency);

            if (recurrenceRule.ByYearDay != null)
                set = set.ByYearDay(recurrenceRule.ByYearDay, recurrenceRule.Frequency);

            if (recurrenceRule.ByMonthDay != null)
                set = set.ByMonthDay(recurrenceRule.ByMonthDay, recurrenceRule.Frequency);

            if (recurrenceRule.ByDay != null)
                set = set.ByDay(recurrenceRule);

            if (instance is IDateTime && (recurrenceRule.ByHour != null || recurrenceRule.ByMinute != null || recurrenceRule.BySecond != null))
            {
                var set2 = set.Cast<IDateTime>();
                if (recurrenceRule.ByHour != null)
                    set2 = set2.ByHour(recurrenceRule.ByHour, recurrenceRule.Frequency);

                if (recurrenceRule.ByMinute != null)
                    set2 = set2.ByMinute(recurrenceRule.ByMinute, recurrenceRule.Frequency);

                if (recurrenceRule.BySecond != null)
                    set2 = set2.BySecond(recurrenceRule.BySecond, recurrenceRule.Frequency);

                set = set2.Cast<T>();
            }

            return set;
        }

        private static IEnumerable<T> ByMonth<T>(this IEnumerable<T> instances, IEnumerable<int> byMonthList, Frequency recurrenceRuleType) where T : IDate
        {
            switch (recurrenceRuleType)
            {
                case Frequency.Yearly:
                    return (from instance in instances
                            from monthNumber in byMonthList.OrderBy(monthNumber => monthNumber)
                            select (T)instance.PlusMonths(monthNumber - instance.Month)).Distinct();
                case Frequency.Monthly:
                case Frequency.Weekly:
                case Frequency.Daily:
                case Frequency.Hourly:
                case Frequency.Minutely:
                case Frequency.Secondly:
                    return instances.OrderedFilter(byMonthList.OrderBy(monthNumber => monthNumber),
                        instance => instance.Month);
            }

            return null;
        }

        /* private static IEnumerable<T> ByWeekNumber<T>(this IEnumerable<T> instances, IEnumerable<int> byWeekNumberList, Frequency recurrenceRuleType) where T : IDate
        {
            switch (recurrenceRuleType)
            {
                case Frequency.Yearly:
                    return (from instance in instances
                            from monthNumber in byMonthList.OrderBy(monthNumber => monthNumber)
                            select (T)instance.PlusMonths(monthNumber - instance.Month)).Distinct();
            }

            return null;
        } */

        private static IEnumerable<T> ByYearDay<T>(this IEnumerable<T> instances, IEnumerable<int> byYearDayList, Frequency recurrenceRuleType) where T : IDate
        {
            switch (recurrenceRuleType)
            {
                case Frequency.Yearly:
                    foreach (var instance in instances)
                        foreach (
                            var dayInYear in
                                byYearDayList.Select(yearDayNumber => GetDayOfYear(yearDayNumber, instance.Year))
                                    .Where(dayOfYear => dayOfYear.HasValue)
                                    .Select(dayOfYear => dayOfYear.Value)
                                    .OrderBy(dayOfYear => dayOfYear)
                                    .Distinct())
                        {
                            yield return (T)instance.PlusDays(instance.DayOfYear - dayInYear);
                        }
                    break;
                case Frequency.Hourly:
                case Frequency.Minutely:
                case Frequency.Secondly:
                    foreach (
                        var instance in
                            instances.Where(
                                instance =>
                                    byYearDayList.Select(yearDayNumber => GetDayOfYear(yearDayNumber, instance.Year))
                                        .Where(dayOfYear => dayOfYear.HasValue)
                                        .Select(dayOfYear => dayOfYear.Value)
                                        .Contains(instance.DayOfYear)))
                        yield return instance;
                    break;
            }
        }

        private static IEnumerable<T> ByMonthDay<T>(this  IEnumerable<T> instances, IEnumerable<int> byMonthDayList, Frequency recurrenceRuleType) where T : IDate
        {
            switch (recurrenceRuleType)
            {
                case Frequency.Yearly:
                case Frequency.Monthly:
                    return
                        instances.SelectMany(
                            instance =>
                                byMonthDayList.Select(monthDay => GetDayOfMonth(monthDay, instance.Year, instance.Month))
                                    .Where(dayOfMonth => dayOfMonth.HasValue)
                                    .Select(dayOfMonth => dayOfMonth.Value)
                                    .OrderBy(dayOfMonth => dayOfMonth)
                                    .Distinct(),
                            (instance, dayOfMonth) => (T)instance.PlusDays(instance.Day - dayOfMonth));
                case Frequency.Daily:
                case Frequency.Hourly:
                case Frequency.Minutely:
                case Frequency.Secondly:
                    return
                        instances.Where(
                            instance =>
                                byMonthDayList.Select(monthDay => GetDayOfMonth(monthDay, instance.Year, instance.Month))
                                    .Where(dayOfMonth => dayOfMonth.HasValue)
                                    .Select(dayOfMonth => dayOfMonth.Value)
                                    .Contains(instance.Day));
            }
            return null;
        }

        private static IEnumerable<T> ByDay<T>(this IEnumerable<T> instances, RecurrenceRule recurrenceRule) where T : IDate
        {
            if (recurrenceRule.ByDay == null)
                return instances;

            var limitByDay =
                instances.Where(
                    instance => recurrenceRule.ByDay.Select(weekday => weekday.Item2).Contains(instance.DayOfWeek));

            Func<bool, IEnumerable<T>> expandByDay =
                (offsetIsMonth) =>
                    instances.Select(
                        instance =>
                            recurrenceRule.ByDay.Select(
                                weekdayNumber =>
                                    GetDays(weekdayNumber, instance.Year, offsetIsMonth ? (int?) instance.Month : null))
                                .SelectMany(dayOfYear => dayOfYear)
                                .Select(dayOfYear => instance.PlusDays(dayOfYear - instance.DayOfYear)))
                        .SelectMany(instance => instance)
                        .Cast<T>();

            switch (recurrenceRule.Frequency)
            {
                case Frequency.Yearly:
                    return recurrenceRule.ByYearDay != null || recurrenceRule.ByMonthDay != null
                        ? limitByDay
                        : expandByDay(false);
                case Frequency.Monthly:
                    return recurrenceRule.ByMonthDay != null ? limitByDay : expandByDay(true);
                case Frequency.Weekly:
                    return from instance in instances
                           from weekday in
                               recurrenceRule.ByDay.Select(weekdayNumber => weekdayNumber.Item2)
                                   .Distinct()
                                   .OrderBy(
                                       weekday =>
                                           weekday <
                                           recurrenceRule.WorkWeekStart.GetValueOrDefault(IsoDayOfWeek.Monday))
                                   .ThenBy(weekday => weekday)
                           select WeekDayDifference(instance, weekday, recurrenceRule.WorkWeekStart);
                case Frequency.Hourly:
                case Frequency.Minutely:
                case Frequency.Secondly:
                    return limitByDay;
            }

            return null;
        }

        private static IEnumerable<IDateTime> ByHour(this IEnumerable<IDateTime> instances, IEnumerable<int> byHourList, Frequency recurrenceRuleType)
        {
            switch (recurrenceRuleType)
            {
                case Frequency.Yearly:
                case Frequency.Monthly:
                case Frequency.Weekly:
                case Frequency.Daily:
                    return (from instance in instances
                            from hour in byHourList.OrderBy(hour => hour)
                            select instance.PlusHours(hour - instance.Hour)).Distinct();
                case Frequency.Hourly:
                case Frequency.Minutely:
                case Frequency.Secondly:
                    return instances.Where(instance => byHourList.Contains((instance).Hour));
            }

            return null;
        }

        private static IEnumerable<IDateTime> ByMinute(this IEnumerable<IDateTime> instances, IEnumerable<int> byMinuteList, Frequency recurrenceRuleType)
        {
            switch (recurrenceRuleType)
            {
                case Frequency.Yearly:
                case Frequency.Monthly:
                case Frequency.Weekly:
                case Frequency.Daily:
                case Frequency.Hourly:
                    return (from instance in instances
                            from minute in byMinuteList.OrderBy(minute => minute)
                            select instance.PlusMinutes(minute - instance.Minute)).Distinct();
                case Frequency.Minutely:
                case Frequency.Secondly:
                    return instances.Where(instance => byMinuteList.Contains(instance.Hour));
            }

            return null;
        }

        private static IEnumerable<IDateTime> BySecond(this IEnumerable<IDateTime> instances, IEnumerable<int> bySecondList, Frequency recurrenceRuleType)
        {
            switch (recurrenceRuleType)
            {
                case Frequency.Yearly:
                case Frequency.Monthly:
                case Frequency.Weekly:
                case Frequency.Daily:
                case Frequency.Hourly:
                case Frequency.Minutely:
                    return (from instance in instances
                            from second in bySecondList.OrderBy(second => second).Distinct()
                            select instance.PlusSeconds(second - instance.Second));
                case Frequency.Secondly:
                    return instances.Where(instance => bySecondList.Contains(instance.Second));
            }

            return null;
        }

        private static IEnumerable<T> BySetPosition<T>(this IEnumerable<T> instances, IEnumerable<int> setPositionDayList)
        {
            if (setPositionDayList == null)
                return instances;

            return
                instances.Where(
                    (instance, index) =>
                        setPositionDayList.Select(setPositionDay => GetIndex(setPositionDay, instances.Count()))
                            .Contains(index));
        }

        private static IEnumerable<T> Count<T>(this IEnumerable<T> instances, int? count)
            where T : IDate
        {
            return count.HasValue ? instances.Take(count.Value) : instances;
        }

        private static IEnumerable<T> Until<T>(this IEnumerable<T> instances, RecurrenceRule rule, DateTimeZone timeZone) where T : IDate
        {
            var recurrenceRuleZonedDateTime = rule as RecurrenceRule<ZonedDateTime?>;
            var zonedDateTime = recurrenceRuleZonedDateTime != null
                ? recurrenceRuleZonedDateTime.Until
                : (rule is RecurrenceRule<ZonedDateTime>
                    ? (ZonedDateTime?)((RecurrenceRule<ZonedDateTime>)rule).Until
                    : null);
            if (zonedDateTime.HasValue)
                return
                    instances.TakeWhile(
                        instance =>
                            ((IDateTime)instance).ToLocalDateTime()
                                .InZoneLeniently(timeZone)
                                .CompareTo(zonedDateTime.Value) <= 0);

            var recurrenceRuleDateTime = rule as RecurrenceRule<LocalDateTime?>;
            var localDateTime = recurrenceRuleDateTime != null
                ? recurrenceRuleDateTime.Until
                : (rule is RecurrenceRule<LocalDateTime>
                    ? (LocalDateTime?)((RecurrenceRule<LocalDateTime>)rule).Until
                    : null);
            if (localDateTime.HasValue)
                return instances.TakeWhile(instance => ((IDateTime)instance).ToLocalDateTime().CompareTo(localDateTime.Value) <= 0);

            var recurrenceRuleDate = rule as RecurrenceRule<LocalDate?>;
            var localDate = recurrenceRuleDate != null
                ? recurrenceRuleDate.Until
                : (rule is RecurrenceRule<LocalDate> ? (LocalDate?)((RecurrenceRule<LocalDate>)rule).Until : null);
            if (localDate.HasValue)
                return instances.TakeWhile(instance => ((IDate)instance).ToLocalDate().CompareTo(localDate.Value) <= 0);

            return instances;
        }

        private static int GetDaysInYear(int year)
        {
            var maxMonth = CalendarSystem.Iso.GetMaxMonth(year);
            return (new LocalDate(year, maxMonth, CalendarSystem.Iso.GetDaysInMonth(year, maxMonth))).DayOfYear;
        }

        private static int? GetDayOfYear(int yearDay, int year)
        {
            var daysInYear = GetDaysInYear(year);
            var dayOfYear = yearDay > 0 ? yearDay : daysInYear + yearDay + 1;

            return dayOfYear >= 1 && dayOfYear <= daysInYear ? (int?)dayOfYear : null;
        }

        private static int? GetDayOfMonth(int monthDay, int year, int month)
        {
            var daysInMonth = CalendarSystem.Iso.GetDaysInMonth(year, month);
            var dayOfMonth = monthDay > 0 ? monthDay : daysInMonth + monthDay + 1;

            return dayOfMonth >= 1 && dayOfMonth <= daysInMonth ? (int?)dayOfMonth : null;
        }

        private static int GetIndex(int setPositionDay, int totalOccurrences)
        {
            return (setPositionDay > 0 ? setPositionDay : totalOccurrences + setPositionDay + 1) - 1;
        }

        private static IEnumerable<int> GetDays(Tuple<int?, IsoDayOfWeek> weekDayNumber, int year, int? month = null)
        {
            LocalDate day;
            var nthOccurence = 0;

            if (weekDayNumber.Item1 <= -1)
            {
                day = new LocalDate(year, month.GetValueOrDefault(CalendarSystem.Iso.GetMaxMonth(year)), CalendarSystem.Iso.GetDaysInMonth(year, month.GetValueOrDefault(CalendarSystem.Iso.GetMaxMonth(year)))).PlusDays(1).Previous(weekDayNumber.Item2);
                while (day.Year == year && (!month.HasValue || day.Month == month))
                {
                    if (--nthOccurence == weekDayNumber.Item1)
                        yield return day.DayOfYear;
                    day = day.Previous(weekDayNumber.Item2);
                }
                yield break;
            }

            day = new LocalDate(year, month.GetValueOrDefault(1), 1).PlusDays(-1).Next(weekDayNumber.Item2);
            while (day.Year == year && (!month.HasValue || day.Month == month))
            {
                if (!weekDayNumber.Item1.HasValue || ++nthOccurence == weekDayNumber.Item1)
                    yield return day.DayOfYear;
                day = day.Next(weekDayNumber.Item2);
            }
        }

        /* private static IEnumerable<T> GetWeeksInYear<T>(T instance, IsoDayOfWeek weekStart) where T : IDate
        {
            var firstDayOfFirstWeek = new LocalDate(instance.Year - 1, 12, 29);
            if (firstDayOfFirstWeek.IsoDayOfWeek != weekStart)
                firstDayOfFirstWeek = firstDayOfFirstWeek.Next(weekStart);

            return Intervals(Frequency.Weekly, 1, new Date(firstDayOfFirstWeek))
                    .TakeWhile(d => d.Day <= GetDaysInYear(instance.Year) - 3)
        } */

        public static IEnumerable<TSource> OrderedFilter<TSource, TSource2>(this IEnumerable<TSource> source, IEnumerable<TSource2> source2, Func<TSource, TSource2> keySelector)
        {
            var sourceEnumerator = source.GetEnumerator();
            var filterByEnumerator = source2.GetEnumerator();

            var @continue = sourceEnumerator.MoveNext();
            while (filterByEnumerator.MoveNext() && @continue)
                do
                {
                    if (keySelector(sourceEnumerator.Current).Equals(filterByEnumerator.Current))
                        yield return sourceEnumerator.Current;
                } while (@continue = sourceEnumerator.MoveNext());
        }

        public static IEnumerable<TSource> RemoveDuplicates<TSource>(this IEnumerable<TSource> source)
        {
            var first = true;
            var previous = default(TSource);

            foreach (var v in source)
            {
                if (first || !source.Equals(previous))
                    yield return v;

                previous = v;
                first = false;
            }
        }

        private static int WeekStartAdjustment(IsoDayOfWeek dayOfWeek, IsoDayOfWeek? workWeekStart)
        {
            var difference = workWeekStart.GetValueOrDefault(IsoDayOfWeek.Monday) - dayOfWeek;
            return workWeekStart <= dayOfWeek ? difference : difference - 7;
        }

        private static T WeekDayDifference<T>(T instance, IsoDayOfWeek targetDayOfWeek, IsoDayOfWeek? workWeekStart) where T : IDate
        {
            var weekStartDifference = WeekStartAdjustment(instance.DayOfWeek, workWeekStart);
            var difference = targetDayOfWeek - workWeekStart.GetValueOrDefault(IsoDayOfWeek.Monday);

            return
                (T)
                    instance.PlusDays(weekStartDifference +
                                      (workWeekStart.GetValueOrDefault(IsoDayOfWeek.Monday) > targetDayOfWeek
                                          ? 7 + difference
                                          : difference));
        }

    }
}
