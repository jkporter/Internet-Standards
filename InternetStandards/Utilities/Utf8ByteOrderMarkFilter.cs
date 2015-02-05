using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InternetStandards.Web;
using System.IO;

namespace InternetStandards.Utilities
{
    public class Utf8ByteOrderMarkFilter : BaseFilter
    {
        readonly byte[] bom;
        readonly List<byte> bomBuffer;

        public Utf8ByteOrderMarkFilter(Stream baseFilter)
            : base(baseFilter)
        {
            bom = new byte[] { 0xEF, 0xBB, 0xBF };
            bomBuffer = new List<byte>(bom.Length);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException();

            if (offset < 0 || count < 0)
                throw new ArgumentOutOfRangeException();

            if (offset + count > buffer.Length)
                throw new ArgumentException();

            while (bomBuffer.Count < bomBuffer.Capacity && count > 0)
            {
                bomBuffer.Add(buffer[offset]);
                if (bomBuffer.Count == bom.Length && !IsBom(bomBuffer.ToArray()))
                    base.BaseStream.Write(bomBuffer.ToArray(), 0, bomBuffer.Count);

                offset++;
                count--;
            }

            if (offset + count <= buffer.Length)
                base.BaseStream.Write(buffer, offset, count);
        }

        public void Dispose()
        {
            if (bomBuffer.Count < bomBuffer.Capacity)
                base.BaseStream.Write(bomBuffer.ToArray(), 0, bomBuffer.Count);

            base.Dispose();
        }

        private bool IsBom(byte[] input)
        {
            bool lengthMatch = input.Length == bom.Length;
            if (lengthMatch)
                for (int i = 0; i < bom.Length; i++)
                    if (input[i] != bom[i])
                        return false;

            return lengthMatch;
        }
    }
}
