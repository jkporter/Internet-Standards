using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace InternetStandards.Ietf
{
    public class UriFormatException : UriReferenceFormatException
    {
        public UriFormatException()
            : base()
        {
        }

        public UriFormatException(string textString)
            : base(textString)
        {
        }

        public UriFormatException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
