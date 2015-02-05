using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Utilities.Collections.Generic
{
    public class NameValuePair<TValue>
    {
        public NameValuePair(string name, TValue value)
            : base()
        {
            Name = name;
            Value = value;
        }

        public string Name
        {
            get;
            private set;
        }

        public TValue Value
        {
            get;
            private set;
        }
    }
}
