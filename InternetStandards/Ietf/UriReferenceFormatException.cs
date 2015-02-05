using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace InternetStandards.Ietf
{
    public class UriReferenceFormatException:FormatException
    {
        public UriReferenceFormatException()
            : base()
        {
        }

        public UriReferenceFormatException(string textString)
            : base(textString)
        {
        }

        public UriReferenceFormatException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}