using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace InternetStandards
{
    public abstract class BaseFilter : Stream
    {
        private Stream baseStream;

        public BaseFilter(Stream baseStream)
        {
            this.baseStream = baseStream;
        }

        public Stream BaseStream
        {
            get
            {
                return baseStream;
            }
        }

        public override bool CanRead
        {
            get
            {
                return baseStream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return baseStream.CanSeek;
            }
        }

        public override bool CanTimeout
        {
            get
            {
                return baseStream.CanTimeout;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return baseStream.CanWrite;
            }
        }

        public override long Length
        {
            get
            {
                return baseStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return baseStream.Position;
            }
            set
            {
                baseStream.Position = value;
            }
        }

        public override int ReadTimeout
        {
            get
            {
                return base.ReadTimeout;
            }
            set
            {
                base.ReadTimeout = value;
            }
        }

        public override int WriteTimeout
        {
            get
            {
                return baseStream.WriteTimeout;
            }
            set
            {
                baseStream.WriteTimeout = value;
            }
        }

        public override System.IAsyncResult BeginRead(byte[] buffer, int offset, int count, System.AsyncCallback callback, object state)
        {
            return baseStream.BeginRead(buffer, offset, count, callback, state);
        }

        public override System.Runtime.Remoting.ObjRef CreateObjRef(System.Type requestedType)
        {
            return baseStream.CreateObjRef(requestedType);
        }

        protected override void Dispose(bool disposing)
        {
            baseStream.Close();
            base.Dispose(disposing);
        }

        public override int EndRead(System.IAsyncResult asyncResult)
        {
            return baseStream.EndRead(asyncResult);
        }

        public override void Flush()
        {
            baseStream.Flush();
        }

        /* public override object InitializeLifetimeService()
        {
            return baseStream.InitializeLifetimeService();
        } */

        public override int Read(byte[] buffer, int offset, int count)
        {
            return baseStream.Read(buffer, offset, count);
        }

        public override int ReadByte()
        {
            return baseStream.ReadByte();
        }

        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            return baseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            baseStream.SetLength(value);
        }

        public override string ToString()
        {
            return baseStream.ToString();
        }

        public abstract override void Write(byte[] buffer, int offset, int count);
    }
}
