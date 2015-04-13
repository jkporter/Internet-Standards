using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace InternetStandards.Ietf.iCalendar.Recurrence
{
    public interface IDateTime : IDate<IDateTime>
    {
        int Hour { get; }

        int Minute { get; }

        int Second { get; }

        IDateTime PlusHours(int hours);

        IDateTime PlusMinutes(int minutes);

        IDateTime PlusSeconds(int seconds);

        LocalDateTime ToLocalDateTime();
    }
}
