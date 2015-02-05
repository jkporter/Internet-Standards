using System;
using System.Collections.Generic;
using System.Text;

namespace InternetStandards.Ietf.Http
{
    public class MediaRangeFormatException:FormatException
    {
        public MediaRangeFormatException(string mediaRange):base("The media range is invalid")
        {
        }
    }
}
