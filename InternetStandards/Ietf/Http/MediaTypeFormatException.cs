using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace InternetStandards.Ietf.Http
{
    public class MediaTypeFormatException:FormatException
    {
        public MediaTypeFormatException():base()
        {
        }

        public MediaTypeFormatException(string message)
            : base(message)
        {
        }
        
        public MediaTypeFormatException(string message, Exception exception)
            : base(message, exception)
        {
        }
        
        public MediaTypeFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
