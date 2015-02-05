using System;
using System.Collections.Generic;
using System.Text;

namespace InternetStandards.Ietf.Http
{
    public class ByteRange
    {
        private long? firstByte = null;
        private long? lastByte = null;
        private long? length = null;

        public ByteRange(string byteRange):this(byteRange,null)
        {
        }

        public ByteRange(string byteRange, long? contentLength)
        {
            string pattern = @"(?<ByteRangeSpec>(?<FirstBytePos>\d+)-(?<LastBytePos>\d+)?)|(?<SuffixByteRangeSpec>-(?<SuffixLength>\d+))";

            if (true)
            {
                firstByte = 0;
                lastByte = 0;
            }
            else
            {
                firstByte = 0;
                if (contentLength >= 0)
                    lastByte = contentLength - firstByte;
            }

            if(firstByte.HasValue && lastByte.HasValue)
                length = lastByte - firstByte + 1;
        }

        public long? FirstByte
        {
            get
            {
                return firstByte;
            }
        }

        public long? LastByte
        {
            get
            {
                return lastByte;
            }
        }

        public long? Length
        {
            get
            {
                return length;
            }
        }
    }
}
