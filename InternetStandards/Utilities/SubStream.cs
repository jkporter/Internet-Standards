using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace InternetStandards.Utilities
{
    public class SubStream:Stream
    {
        readonly long startPosition = -1;
        long length = -1;

        public SubStream(Stream parentStream, long length)
        {
            ParentStream = parentStream;
            startPosition = ParentStream.Position;
            this.length = length;
        }

        public override bool CanRead
        {
            get { return ParentStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return ParentStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return ParentStream.CanWrite; }
        }

        public override void Flush()
        {
            ParentStream.Flush();
        }

        public override long Length
        {
            get { return length; }
        }

        public Stream ParentStream
        {
            get;
            private set;
        }

        public override long Position
        {
            get
            {
                return ParentStream.Position - startPosition;
            }
            set
            {
                ParentStream.Position = startPosition + value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            count = Math.Min((int)Math.Max(Length - Position, (long)0), count);

            return ParentStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Begin || origin == SeekOrigin.End)
                offset += startPosition;

            if (origin == SeekOrigin.End)
            {
                offset += Length;
                origin = SeekOrigin.Begin;
            }

            ParentStream.Seek(offset, origin);

            return Position;
        }

        public override void SetLength(long value)
        {
            length = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            ParentStream.Write(buffer, offset, count);
        }   
    }
}
