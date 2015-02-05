using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf.iCalendar.DataTypes
{
    public struct Time
    {
        private int _hour;
        public int Hour
        {
            get { return _hour; }
            set
            {
                if (value < 0 || value > 23)
                    throw new ArgumentOutOfRangeException();
                _hour = value;
            }
        }

        private int _minute;
        public int Minute
        {
            get { return _minute; }
            set
            {
                if(value < 0 || value > 59)
                    throw new ArgumentOutOfRangeException();

                _minute = value;
            }
        }

        private int _second;
        public int Second
        {
            get { return _second; }
            set
            {
                if (value < 0 || value > 60)
                    throw new ArgumentOutOfRangeException();
                _second = value;
            }
        }

        public bool Utc { get; set; }

        public override string ToString()
        {
            return Hour.ToString("00") + Minute.ToString("00") + Second.ToString("00") + (Utc ? "Z" : string.Empty);
        }

        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(Hour, Minute, Second);
        }
    }
}
