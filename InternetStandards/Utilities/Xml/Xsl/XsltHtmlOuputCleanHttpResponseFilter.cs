using System;
using System.Collections.Generic;
using System.Text;
using InternetStandards.Web;
using System.IO;

namespace InternetStandards.Utilities.Xml.Xsl
{
    public class XsltHtmlOuputCleanHttpResponseFilter : BaseFilter
    {
        private int position = 0;
        public XsltHtmlOuputCleanHttpResponseFilter(Stream baseFilter)
            : base(baseFilter)
        {
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            const int htmlTagStart = 0x5A;
            if (position <= htmlTagStart && (position + count - 1) >= htmlTagStart)
            {
                int bytesBefore = htmlTagStart - position;
                BaseStream.Write(buffer, offset, bytesBefore);
                BaseStream.Write(new byte[] { 0x0D, 0x0A }, 0, 2);

                position += bytesBefore;
                offset += bytesBefore;
                count -= bytesBefore;
            }

            const int metaStart = 0x68;
            if (position < metaStart)
            {
                int bytesBefore = metaStart - position;
                if (bytesBefore > count)
                    bytesBefore = count;

                BaseStream.Write(buffer, offset, bytesBefore);

                position += bytesBefore;
                offset += bytesBefore;
                count -= bytesBefore;
            }

            const int afterMeta = 0xB0;
            if (position < afterMeta)
            {
                int bytesBefore = afterMeta - position;
                if (bytesBefore > count)
                    bytesBefore = count;

                position += bytesBefore;
                offset += bytesBefore;
                count -= bytesBefore;
            }

            if (position >= afterMeta)
                BaseStream.Write(buffer, offset, count);
        }
    }
}
