using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace InternetStandards.Ietf.Mail.Mime
{
    public class Utf8Quoted_PrintableInterpreter : Quoted_PrintableInterpreter
    { 
        public Utf8Quoted_PrintableInterpreter(string s):base(null)
        {
            //baseStream.
        }

        public override bool Read()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override byte[] Value
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override bool LineBreak
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override bool EquivalentToAscii
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
    }
}
