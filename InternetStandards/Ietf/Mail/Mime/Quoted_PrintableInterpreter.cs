using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace InternetStandards.Ietf.Mail.Mime
{
    public abstract class Quoted_PrintableInterpreter
    {
        protected Stream baseStream = null;

        public Quoted_PrintableInterpreter(string s, Encoding e)
        {
            baseStream = new MemoryStream(e.GetBytes(s), false);
        }

        public Quoted_PrintableInterpreter(Stream baseStream)
        {
            this.baseStream = baseStream;
        }

        public abstract bool Read();

        public abstract byte[] Value { get;}

        public virtual void Close()
        {
            baseStream.Close();
        }

        public abstract bool LineBreak { get; }

        public abstract bool EquivalentToAscii { get; }
    }
}
