using System;
using System.IO;

namespace InternetStandards.Utilities
{
    public class SubStream : Stream
    {
        private readonly long startPosition;
        private long length;

        public SubStream(Stream parentStream, long length)
        {
            ParentStream = parentStream;
            startPosition = ParentStream.Position;
            this.length = length;
        }

        public override bool CanRead => ParentStream.CanRead;

        public override bool CanSeek => ParentStream.CanSeek;

        public override bool CanWrite => ParentStream.CanWrite;

        public override void Flush()
        {
            ParentStream.Flush();
        }

        public override long Length => length;

        public Stream ParentStream
        {
            get;
        }

        public override long Position
        {
            get => ParentStream.Position - startPosition;
            set => ParentStream.Position = startPosition + value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            count = Math.Min((int)Math.Max(Length - Position, 0), count);

            return ParentStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin is SeekOrigin.Begin or SeekOrigin.End)
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
