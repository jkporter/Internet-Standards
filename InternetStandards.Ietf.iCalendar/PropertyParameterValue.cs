using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf.iCalendar
{
    public class PropertyParameterValue
    {
        public PropertyParameterValue(string value, bool? quotedString)
        {
            
        }

        public PropertyParameterValue(string value):this(value, null)
        {
        }
    }
}
