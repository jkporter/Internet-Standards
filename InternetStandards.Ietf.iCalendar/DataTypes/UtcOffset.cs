using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf.iCalendar.DataTypes
{
    public struct UtcOffset
    {
        private TimeSpan _offset;
        public UtcOffset(TimeSpan offset)
        {
            _offset = offset;
        }

        public override string ToString()
        {
            return _offset.Hours.ToString(CultureInfo.InvariantCulture) + _offset.Minutes.ToString(CultureInfo.InvariantCulture) + _offset.Seconds.ToString(CultureInfo.InvariantCulture);
        }
    }
}
