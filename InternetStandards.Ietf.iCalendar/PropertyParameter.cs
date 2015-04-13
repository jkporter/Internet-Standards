using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf.iCalendar
{
    public class PropertyParameter
    {
        public PropertyParameter(string name, PropertyParameterValue value)
        {
            
        }

        public PropertyParameter(string name, PropertyParameterValue[] values)
        {
            
        }

        public string Name { get; private set; }

        public string Values { get; set; }

        public override string ToString()
        {
            return Name + string.Join(",", Values);
        }

        public static PropertyParameterValue DefaultValue
        {
            get { return null; }
        }
    }
}
